using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Z.DA.DB;
using Z.Ex;

namespace Z.DA
{
    /// <summary>
    /// ���ݷ��ʻ��࣬ ͨ�������������������ʵ�ֶ����ݿ�ķ��ʣ� ͨ��<c>TransScope</c>ʵ������
    /// </summary>
    public class AbstractDA : IDisposable
    {
        /// <summary>
        /// Ĭ�ϵ���Сʱ��
        /// </summary>
        public static DateTime MinDateTime = new DateTime(1900, 1, 1);

        #region ˽�б���

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        internal protected string ProviderName;

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        internal protected string MyConnectionString;


        /// <summary>
        /// ���ݿ�����
        /// </summary>
        internal IDatabase MyDatabase;

        /// <summary>
        /// ���ӳ�ʱʱ��
        /// </summary>
        internal protected int ExecTimeout = 3000;

        /// <summary>
        /// �Ƿ������ӣ� �ڶ�������ʱͨ��IDispose�ӿڹر�
        /// </summary>
        internal protected bool KeepAlive = false;

        /// <summary>
        /// ���ݿ������Ƿ��ɵ�ǰʵ������
        /// </summary>
        internal bool IsConnectionOwner = false;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        internal IDbConnection MyConnection = null;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        internal IDbTransaction MyTransaction = null;

        #endregion

        #region ���캯��

        #region ���캯���� ͨ��App.Config�е�ConnectionSection�л�ȡ�����ַ������ṩ��, ����Connetion����

        /// <summary>
        /// ���캯���� ͨ��App.Config�е�ConnectionSection�л�ȡ�����ַ������ṩ��, ����Connetion����
        /// </summary>
        /// <param name="ConnectionName"></param>
        public AbstractDA(string ConnectionName)
        {
            ConnectionStringSettings Sec = ConfigurationManager.ConnectionStrings[ConnectionName];
            if (Sec == null)
                throw new ConfigurationErrorsException("�������ļ����Ҳ������ݿ�ڣ�" + ConnectionName);

            InitDatabase(Sec.ProviderName, Sec.ConnectionString);
        }

        #endregion

        #region ���캯���� ֱ�Ӵ��������ַ������ṩ��, ����Connetion����

        /// <summary>
        /// ���캯���� ֱ�Ӵ��������ַ������ṩ��, ����Connetion����
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        public AbstractDA(string ProviderName, string ConnectionString)
        {
            InitDatabase(ProviderName, ConnectionString);
        }

        #endregion

        #region ѡ�����ݿ�ʵ��

        /// <summary>
        /// ѡ�����ݿ�ʵ��
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="ConnectionString"></param>
        private void InitDatabase(string providerName, string ConnectionString)
        {
            switch (providerName.ToLower())
            {
                case "mysql":
                    MyDatabase = new MySQL();
                    break;
                case "sqlserver":
                case "sql":
                case "system.data.sqlclient":
                    MyDatabase = new SqlServer();
                    break;
                case "jetdb":
                case "":
                    MyDatabase = new JetDB();
                    break;
                default:
                    throw new Exception("���ݿ����Ͳ�֧��!");
            }

            MyConnectionString = ConnectionString;
            ProviderName = providerName;
        }

        #endregion

        internal AbstractDA() { }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ִ��һ��SQL���, ����DataSet
        /// </summary>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        internal protected virtual DataSet ExecuteDataSet(string SQL, params object[] values)
        {
            if ( values != null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ƥ��");

            IDbConnection connection = CreateConnection();
            IDataAdapter adapter = null;

            try
            {
                DataSet ds = new DataSet();

                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    BuildCommandText(values, SQL, cmd);

                    adapter = MyDatabase.CreateAdapter(cmd);
                    adapter.Fill(ds);
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
            finally
            {
                if (adapter != null && adapter is IDisposable)
                    (adapter as IDisposable).Dispose();

                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }

        }

        #endregion

        #region ExecuteNoneQuery

        /// <summary>
        /// ִ��һ��SQL���, ����Ӱ���������
        /// </summary>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        internal protected virtual int ExecuteNoneQuery(string SQL, params object[] values)
        {
            if (values != null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ƥ��");

            int iRet = -1;

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    BuildCommandText(values, SQL, cmd);
                    iRet = cmd.ExecuteNonQuery();
                }
                return iRet;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        #endregion

        #region ExecuteScalar<T>

        /// <summary>
        /// ִ��һ��SQL���, ���� T ���͵ĵ�ֵ
        /// </summary>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        internal protected virtual T ExecuteScalar<T>(string SQL, params object[] values)
        {
            if ( values != null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ֵ��������ƥ��");

            IDbConnection connection = CreateConnection();

            try
            {
                object o = null;

                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    BuildCommandText(values, SQL, cmd);

                    o = cmd.ExecuteScalar();
                }

                if (o != null && o != DBNull.Value)
                    return (T)Convert.ChangeType(o, typeof(T));

                return default(T);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        #endregion

        #region ExecuteEntity<T>

        /// <summary>
        /// ִ��һ��SQL���, ����һ�� T���͵�List����, �� Extracter �������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Extracter">T���������</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        internal protected virtual T ExecuteEntity<T>(Converter<IDataRecord, T> Extracter, string SQL, params object[] values)
        {
            if ( values != null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ֵ��������ƥ��");

            if (Extracter == null) throw new DatabaseException("Ϊ{0}����׼���Ľ�����ExtracterΪ�գ�", null, SQL);

            IDbConnection connection = CreateConnection();

            T t = default(T);

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    BuildCommandText(values, SQL, cmd);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            t = Extracter(reader);
                        }

                        reader.Close();
                    }
                }

                return t;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        #endregion

        #region ExecuteList

        /// <summary>
        /// ִ��һ��SQL���, ����һ�� T���͵�List����, �� Extracter �������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Extracter"></param>
        /// <param name="SQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        internal protected virtual IEnumerable<T> ExecuteEnumerator<T>(Converter<IDataRecord, T> Extracter, string SQL, params object[] values)
        {
            if (values != null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ֵ��������ƥ��");

            if (Extracter == null)
                throw new DatabaseException("Ϊ{0}����׼���Ľ�����ExtracterΪ�գ�", null, SQL);

            IDbConnection connection = CreateConnection();

            try
            {
                using (var cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    BuildCommandText(values, SQL, cmd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T t = Extracter(reader);

                            if (t != null)
                                yield return t;
                        }

                        reader.Close();
                    }
                }
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// ִ��һ��SQL���, ����һ�� T���͵�List����, �� Extracter �������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Extracter"></param>
        /// <param name="SQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        internal protected virtual List<T> ExecuteList<T>(Converter<IDataRecord, T> Extracter, string SQL, params object[] values)
        {
            if (values!= null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ֵ��������ƥ��");

            if (Extracter == null)
                throw new DatabaseException("Ϊ{0}����׼���Ľ�����ExtracterΪ�գ�", null, SQL);

            List<T> list = new List<T>();

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    BuildCommandText(values, SQL, cmd);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T t = Extracter(reader);

                            if (t != null)
                                list.Add(t);
                        }

                        reader.Close();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// ��ҳ�б���ʾ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Extracter"></param>
        /// <param name="SQL"></param>
        /// <param name="TotalCount"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        internal protected virtual List<T> ExecuteListPaged<T>(Converter<IDataRecord, T> Extracter, string SQL, out int TotalCount, params object[] values)
        {
            if (values != null
                && SQL.IndexOf("{" + values.Length + "}") >= 0) throw new ArgumentException("������ֵ��������ƥ��");

            if (Extracter == null)
                throw new DatabaseException("Ϊ{0}����׼���Ľ�����ExtracterΪ�գ�", null, SQL);

            TotalCount = -999;

            List<T> list = new List<T>();

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;

                    BuildCommandText(values, SQL, cmd);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (TotalCount == -999)
                            {
                                string fName = reader.GetName(reader.FieldCount - 1);

                                if (fName == "ZDA_TOTAL_COUNT")
                                {
                                    TotalCount = reader.GetInt32(reader.FieldCount - 1);
                                }
                            }
                            
                            T t = Extracter(reader);

                            if (t != null)
                                list.Add(t);
                        }

                        reader.Close();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        #endregion

        #region ExecuteProcedure

        /// <summary>
        /// ִ��һ���洢���̣� ���� T ���͵ĵ�ֵ
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns>T ���͵ĵ�ֵ</returns>
        internal protected virtual T ExecuteScalarProcedure<T>(string ProcedureName, params object[] values)
        {
            StoreProcedureInfo info = new StoreProcedureInfo();
            IDbDataParameter[] plist = GetProcedureParameters(ProcedureName, values);
            return ExecuteScalarProcedure<T>(ProcedureName, plist);           
        }

        /// <summary>
        /// ִ��һ���洢���̣� ���� T ���͵ĵ�ֵ
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="ParameterList">�����IDataParameter�����б�</param>
        /// <returns>T ���͵ĵ�ֵ</returns>
        internal protected virtual T ExecuteScalarProcedure<T>(string ProcedureName, params IDbDataParameter[] ParameterList)
        {           
            IDbConnection connection = CreateConnection();

            try
            {
                object o = null;
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (IDataParameter p in ParameterList)
                        cmd.Parameters.Add(p);
                    o = cmd.ExecuteScalar();
                }

                if (o != null && o != DBNull.Value)
                    return (T)Convert.ChangeType(o, typeof(T));
                return default(T);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, ProcedureName);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// ִ��һ���洢���̣� ������DataSet
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="ParameterList">�����IDataParameter�����б�</param>
        /// <returns>�洢���̷���ֵ</returns>
        protected int ExecuteProcedure(string ProcedureName, params IDbDataParameter[] ParameterList)
        {
            int iRet = -1;

            IDbConnection connection = CreateConnection();

            try
            {

                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (IDataParameter p in ParameterList)
                        cmd.Parameters.Add(p);

                    cmd.ExecuteNonQuery();

                    foreach (IDataParameter p in cmd.Parameters)
                        if (p.Direction == ParameterDirection.ReturnValue)
                            iRet = Convert.ToInt32(p.Value);
                }

                return iRet;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, ProcedureName);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// ִ��һ���洢���̣� ������DataSet
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns>�洢���̷��ض���</returns>
        internal protected StoreProcedureInfo ExecuteProcedure(string ProcedureName, params object[] values)
        {
            StoreProcedureInfo info = new StoreProcedureInfo();

            IDbDataParameter[] plist = GetProcedureParameters(ProcedureName, values);

            info.ReturnCode = ExecuteProcedure(ProcedureName, plist);

            foreach (IDataParameter p in plist)
            {
                if (p.Direction == ParameterDirection.InputOutput ||
                    p.Direction == ParameterDirection.Output)
                {
                    info.OutputParameters.Add(p.ParameterName.Replace(MyDatabase.ParameterPrefix,""), p.Value);
                }
            }

            return info;

        }


        #endregion

        #region ExecuteProcedureNoneQuery

        /// <summary>
        /// ִ�д洢���̣� ֻ���ش洢���̷���ֵ
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">����Ĳ����б�</param>
        /// <returns>�洢���̷���ֵ</returns>        
        internal protected int ExecuteProcedureNonQuery(string ProcedureName, params object[] values)
        {
            int iRet = -1;

            IDataParameter[] ParameterList = GetProcedureParameters(ProcedureName, values);

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (IDataParameter p in ParameterList)
                        cmd.Parameters.Add(p);

                    cmd.ExecuteNonQuery();

                    foreach (IDataParameter p in ParameterList)
                    {
                        if (p.Direction == ParameterDirection.ReturnValue)
                            iRet = Convert.ToInt32(p.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, ProcedureName);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }

            }

            return iRet;
        }

        #endregion

        #region ExecuteProcedureDataSet

        /// <summary>
        /// ִ�д洢���̣� ����DataSet
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="ds">���ص�DataSet</param>
        /// <param name="ParameterList">�����IDataParameter�����б�</param>
        /// <returns>�洢���̷���ֵ</returns>        
        protected int ExecuteProcedureDataSet(string ProcedureName, out DataSet ds, params IDataParameter[] ParameterList)
        {
            ds = new DataSet();
            int iRet = -1;
            IDbDataAdapter adapter = null;

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (IDataParameter p in ParameterList)
                        cmd.Parameters.Add(p);

                    adapter = MyDatabase.CreateAdapter(cmd);

                    adapter.Fill(ds);

                    foreach (IDataParameter p in ParameterList)
                    {
                        if (p.Direction == ParameterDirection.ReturnValue)
                            iRet = Convert.ToInt32(p.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, ProcedureName);
            }
            finally
            {
                if (adapter != null && adapter is IDisposable)
                    (adapter as IDisposable).Dispose();

                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }

            }

            return iRet;
        }

        /// <summary>
        /// ִ�д洢���̣� ����DataSet
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns>�洢���̷��ض���</returns>
        internal protected StoreProcedureInfo ExecuteProcedureDataSet(string ProcedureName, params object[] values)
        {
            StoreProcedureInfo info = new StoreProcedureInfo();

            IDataParameter[] plist = GetProcedureParameters(ProcedureName, values);

            info.ReturnCode = ExecuteProcedureDataSet(ProcedureName, out info.DataSet, plist);

            foreach (IDataParameter p in plist)
            {
                if (p.Direction == ParameterDirection.InputOutput ||
                    p.Direction == ParameterDirection.Output)
                {
                    info.OutputParameters.Add(p.ParameterName.Replace(MyDatabase.ParameterPrefix, ""), p.Value);
                }
            }

            return info;
        }

        #endregion

        #region ExecuteProcedureList<T>

        /// <summary>
        /// ִ�д洢���̣� ��������Ϊ T ��List�б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns></returns>
        public StoreProcedureList<T> ExecuteProcedureList<T>(string ProcedureName, params object[] values) where T: new()
        {
            return ExecuteProcedureList<T>(ProcedureName, EntityTools.ChangeType<T>, values);
        }
        
        /// <summary>
        /// ִ�д洢���̣� ��������Ϊ T ��List�б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="Extracter">���������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns></returns>
        public StoreProcedureList<T> ExecuteProcedureList<T>(string ProcedureName, Converter<IDataRecord, T> Extracter, params object[] values)
        {
            StoreProcedureList<T> info = new StoreProcedureList<T>();

            IDbDataParameter[] ParameterList = GetProcedureParameters(ProcedureName, values);

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    foreach (IDataParameter p in ParameterList)
                        cmd.Parameters.Add(p);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T t = Extracter(reader);

                            if (t != null)
                                info.List.Add(t);
                        }
                    }


                    foreach (IDataParameter p in ParameterList)
                    {
                        if (p.Direction == ParameterDirection.ReturnValue)
                            info.ReturnCode = Convert.ToInt32(p.Value);

                        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                        {
                            info.OutputParameters.Add(p.ParameterName.Substring(1), p.Value);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, ProcedureName);
            }
            finally
            {
                if (MyConnection != connection && MyTransaction == null)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }

            }

            return info;
        }

        #endregion

        #region GetProcedureParameters

        /// <summary>
        /// �����洢���̲���
        /// </summary>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="objs">�������ֵ�����ղ�����Input���ͣ�˳��ֵ��Parameter��</param>
        /// <returns>IDataParameter����</returns>
        protected IDbDataParameter[] GetProcedureParameters(string ProcedureName, params object[] objs)
        {
            string CacheKey = MyConnectionString + ":SP:" + ProcedureName;
            if (!DatabaseCache.Instance.ContainsKey(CacheKey))
            {
                lock (DatabaseCache.Instance)
                {
                    if (!DatabaseCache.Instance.ContainsKey(CacheKey))
                    {
                        IDbDataParameter[] ParaList = ExtractProcedureParameters(ProcedureName);

                        DatabaseCache MyCache = new DatabaseCache(ProcedureName, ParaList);

                        DatabaseCache.Instance.Add(CacheKey, MyCache);
                    }
                }
            }

            return CloneParameters(DatabaseCache.Instance[CacheKey].Parameters, objs);
        }

        #endregion

        #region GetDatabaseType

        /// <summary>
        /// ConnectionName
        /// </summary>
        /// <param name="ConnectionName"></param>
        /// <returns></returns>
        public static string GetDatabaseType(string ConnectionName)
        {
            ConnectionStringSettings Sec = ConfigurationManager.ConnectionStrings[ConnectionName];
            if (Sec == null)
                throw new ConfigurationErrorsException("�������ļ����Ҳ������ݿ�ڣ�" + ConnectionName);

            return Sec.ProviderName;
        }

        #endregion

        #region ImportData(����������ݵ����ݿ�)

        /// <summary>
        /// ImportData(����������ݵ����ݿ�)
        /// </summary>
        /// <param name="dt">��������ݷ���DataTable��, DataTable��ColumnsΪĬ����ӳ��, DataTable.TableNameΪĿ�����</param>
        /// <param name="TimeoutSecond">���볬ʱ</param>
        /// <returns></returns>
        public bool ImportData(DataTable dt, int TimeoutSecond)
        {
            if (MyConnection is MySqlConnection)
            {
                throw new NotImplementedException("���MySQL���������ݵ�����δʵ��");
                //using (MySqlBulkLoader bulkLoader = new MySqlBulkLoader(MyConnection as MySqlConnection))
                //{
                //    foreach (DataColumn c in CurrentTable.Columns)
                //    {
                //        bulkLoader.Columns.Add(c.ColumnName);
                //        //bulkLoader.
                //    }
                //}
            }
            else if (MyConnection is SqlConnection)
            {
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(MyConnection as SqlConnection))
                {
                    sqlBulk.BatchSize = dt.Rows.Count;
                    sqlBulk.BulkCopyTimeout = TimeoutSecond;
                    sqlBulk.DestinationTableName = dt.TableName;

                    foreach (DataColumn c in dt.Columns)
                    {
                        sqlBulk.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                    }
                    
                    sqlBulk.WriteToServer(dt);
                }
            }

            return true;
        }

        #endregion

        #region TruncateTable ��ձ�

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public bool TruncateTable(string TableName)
        {
            ExecuteNoneQuery("truncate table " + TableName);

            return true;
        }

        #endregion

        #region ���ط���

        /// <summary>
        /// ����һ��T���͵Ķ���, ʹ�����õ�EntityTools.ChangeType���������н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        protected virtual T ExecuteEntity<T>(string SQL, params object[] values) where T : new()
        {
            return ExecuteEntity<T>(EntityTools.ChangeType<T>, SQL, values);
        }

        /// <summary>
        /// ����һ�� T���͵�List����, ʹ�����õ�EntityTools.ChangeType���������н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        protected virtual List<T> ExecuteList<T>(string SQL, params object[] values) where T : new()
        {
            return ExecuteList<T>(EntityTools.ChangeType<T>, SQL, values);
        }

        /// <summary>
        /// ����һ�� T���͵�List����, ʹ�����õ�EntityTools.ChangeType���������н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        protected virtual IEnumerable<T> ExecuteEnumerator<T>(string SQL, params object[] values) where T : new()
        {
            return ExecuteEnumerator<T>(EntityTools.ChangeType<T>, SQL, values);
        }

        /// <summary>
        /// SQL 2005 ר�õķ�ҳ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalRecords"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        protected virtual List<T> ExecuteListPaged<T>(string SQL, int PageIndex, int PageSize, string OrderBy, out int TotalRecords, params object[] values) where T : new()
        {
            StringBuilder sb;
            CreatePagedSQL(SQL, PageIndex, PageSize, OrderBy, values, out sb);

            return ExecuteListPaged<T>(EntityTools.ChangeType<T>, sb.ToString(), out TotalRecords, values);
        }

        private static void CreatePagedSQL(string SQL, int PageIndex, int PageSize, string OrderBy, object[] values, out StringBuilder sb)
        {
            sb = new StringBuilder(SQL.Length + 100);

            int i = SQL.IndexOf(" from ", StringComparison.OrdinalIgnoreCase);

            if (i <= 0) throw new DatabaseException("��ҳSQL�﷨����", null, SQL);

            sb.AppendFormat(
                "with temp_t as ({0}, ROW_NUMBER() OVER (ORDER BY {2}) AS ZDA_ROW_NUMBER {1}) SELECT *, (select count(1) from temp_t) as ZDA_TOTAL_COUNT FROM temp_t WHERE ZDA_ROW_NUMBER between {3} and {4}",
                SQL.Substring(0, i), SQL.Substring(i), OrderBy, 
                PageSize * (PageIndex-1)+ 1,
                PageSize * PageIndex);

        }

        /// <summary>
        /// ����һ�� T���͵�List����, ʹ�����õ�EntityTools.ChangeType���������н���
        /// </summary>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        protected virtual List<string> ExecuteList(string SQL, params object[] values)
        {
            return ExecuteList<string>(EntityTools.ChangeType, SQL, values);
        }

        #endregion

        #region ˽�к���

        #region CreateConnection

        /// <summary>
        /// �麯��ʵ��
        /// </summary>
        /// <returns></returns>
        internal virtual IDbConnection CreateConnection()
        {
            //������ڵ�ǰ���ӣ� ��ʹ�õ�ǰ���ӣ� ��������ڣ� �򴴽�
            if (MyTransaction != null)
                return MyTransaction.Connection;

            if (MyConnection != null)
            {
                if (MyConnection.State != ConnectionState.Open)
                    MyConnection.Open();

                IsConnectionOwner = true;
                return MyConnection;
            }
            else
            {
                IDbConnection conn = MyDatabase.CreateConnection(MyConnectionString);
                conn.Open();

                if (KeepAlive)
                {
                    MyConnection = conn;
                    IsConnectionOwner = true;
                }

                return conn;
            }
        }

        #endregion

        #region CreateCommand

        /// <summary>
        /// ����һ��IDbCommand�� ����趨������ ���������IDbCommand��������
        /// </summary>
        /// <returns></returns>
        internal IDbCommand CreateCommand(IDbConnection connection, IDbTransaction transaction)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandTimeout = ExecTimeout;

            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }

            return cmd;
        }

        #endregion

        #region ExtractParameters

        /// <summary>
        /// �����洢���̵Ĳ���
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <returns></returns>
        internal IDbDataParameter[] ExtractProcedureParameters(string ProcedureName)
        {
            IDbDataParameter[] Parameters = null;

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = ProcedureName;

                    MyDatabase.ExtractStoreProcedureParameters(cmd);

                    Parameters = new IDbDataParameter[cmd.Parameters.Count];

                    cmd.Parameters.CopyTo(Parameters, 0);
                }
            }
            finally
            {
                if (MyConnection != connection)
                {
                    if (!KeepAlive)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }

            return Parameters;
        }

        #endregion

        #region BuildCommandText

        static Regex reg = new Regex(@"\{\d+\}");

        /// <summary>
        /// ��SQL����н�������, ����cmd.Parameter��
        /// </summary>
        /// <param name="values"></param>
        /// <param name="SQL"></param>
        /// <param name="cmd"></param>
        internal void BuildCommandText(IList values, string SQL, IDbCommand cmd)
        {
            if (values!= null && values.Count > 0 && values[0] is IList)
            {
                BuildCommandText(values[0] as IList, SQL, cmd);
                return;
            }

            string CacheKey = MyConnectionString + ":" + SQL;

            cmd.CommandType = CommandType.Text;

            //�����κβ������軺��
            if (reg.IsMatch(SQL) == false)
            {
                cmd.CommandText = SQL;
                return;
            }

            if (!DatabaseCache.Instance.ContainsKey(CacheKey))
            {
                lock (DatabaseCache.Instance)
                {
                    if (!DatabaseCache.Instance.ContainsKey(CacheKey))
                    {
                        StringBuilder sbSQL = new StringBuilder(SQL);

                        IDbDataParameter[] pList = new IDbDataParameter[values.Count];

                        for (int i = 0, j = values.Count; i < j; i++)
                        {
                            pList[i] = cmd.CreateParameter();
                            pList[i].ParameterName = MyDatabase.FormaterParameterName(i);
                            pList[i].Value = null;

                            sbSQL.Replace("{" + i + "}", MyDatabase.FormaterParameterName(i));
                        }

                        DatabaseCache.Instance.Add(
                            CacheKey,
                            new DatabaseCache(sbSQL.ToString(), pList));

                        sbSQL = null;
                    }
                }
            }

            DatabaseCache cache = DatabaseCache.Instance[CacheKey];

            cmd.CommandText = cache.SQL;

            AddParametersClone2Cmd(cmd, cache.Parameters, values);
        }

        #endregion

        #region AddParametersClone2Cmd

        /// <summary>
        /// �����е�IDataParameter���鸴�Ƶ�IDbCommand��
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="Parameters"></param>
        /// <param name="values"></param>
        internal void AddParametersClone2Cmd(IDbCommand cmd, IList Parameters, IList values)
        {
            int valueIndex = 0;

            for (int i = 0; i < Parameters.Count; i++)
            {
                IDbDataParameter p = (Parameters[i] as ICloneable).Clone() as IDbDataParameter;

                if (values != null && values.Count > valueIndex)
                {
                    if (values[valueIndex] == null)
                        p.Value = DBNull.Value;
                    else
                    {
                        if (values[valueIndex] is DateTime)
                        {
                            DateTime dt = (DateTime)values[valueIndex];

                            if (DateTime.Compare(dt, DateTime.MinValue) == 0)
                                p.Value = AbstractDA.MinDateTime;
                            else
                                p.Value = dt;
                        }
                        else
                            p.Value = values[valueIndex];
                    }
                    valueIndex++;
                }
                else
                    p.Value = DBNull.Value;

                cmd.Parameters.Add(p);
            }
        }

        #endregion

        #region CloneParameters

        /// <summary>
        /// ���ƴ����Parameter�б�, ����һ���µ�ʵ������
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="values">�������ֵ�б�values�� ����˳����и�ֵ</param>
        /// <returns></returns>
        internal IDbDataParameter[] CloneParameters(IList Parameters, IList values)
        {
            IDbDataParameter[] NewParameters = new IDbDataParameter[Parameters.Count];

            int j = 0;

            for (int i = 0; i < Parameters.Count; i++)
            {
                NewParameters[i] = (Parameters[i] as ICloneable).Clone() as IDbDataParameter;
                NewParameters[i].Value = DBNull.Value;

                if (NewParameters[i].Direction == ParameterDirection.Input && j < values.Count)
                {
                    if (values[j] != null)
                        NewParameters[i].Value = values[j];
                    j++;
                }
            }

            return NewParameters;
        }

        #endregion

        #endregion

        #region �������Ӻ�����

        /// <summary>
        /// ��ʽ����
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// ��ʽ����
        /// </summary>
        ~AbstractDA()
        {
            Dispose(false);
        }

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="IsDispose"></param>
        protected virtual void Dispose(bool IsDispose)
        {
            if (IsConnectionOwner)
            {
                if (MyConnection != null)
                {
                    if (MyConnection.State == ConnectionState.Open)
                    {
                        MyConnection.Close();
                        MyConnection.Dispose();
                    }
                }
            }
        }


        #endregion

        #region Internal ����

        #region ֱ�����ݿ����ӷ���

        /// <summary>
        /// ֱ�����ݿ����ӷ���
        /// </summary>
        internal IDbConnection Connection
        {
            get
            {
                return MyConnection;
            }
            set
            {
                if (MyConnection != null && MyConnection.State == ConnectionState.Open)
                {
                    throw new DatabaseException("��ǰʵ���������ݿ����ӱ���", null, string.Empty);
                }

                MyConnection = value;
                KeepAlive = true;
                IsConnectionOwner = false;
            }
        }

        #endregion

        #endregion

    }
}
