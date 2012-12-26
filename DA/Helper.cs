using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Z.DA.DB;
using Z.Ex;
using System.Configuration;
using System.Collections;

namespace Z.DA
{
    class Helper
    {
        private IDbConnection MyConnection;
        private IDatabase MyDatabase;

        #region ���캯��

        #region ���캯���� ͨ��App.Config�е�ConnectionSection�л�ȡ�����ַ������ṩ��, ����Connetion����

        /// <summary>
        /// ���캯���� ͨ��App.Config�е�ConnectionSection�л�ȡ�����ַ������ṩ��, ����Connetion����
        /// </summary>
        /// <param name="ConnectionName"></param>
        public Helper(string ConnectionName)
        {
            ConnectionStringSettings Sec = ConfigurationManager.ConnectionStrings[ConnectionName];
            if (Sec == null)
                throw new ConfigurationErrorsException("�������ļ����Ҳ������ݿ�ڣ�" + ConnectionName);

            SelectDatabase(
                Sec.ProviderName,
                Sec.ConnectionString);
        }

        #endregion

        #region ���캯���� ֱ�Ӵ��������ַ������ṩ��, ����Connetion����

        /// <summary>
        /// ���캯���� ֱ�Ӵ��������ַ������ṩ��, ����Connetion����
        /// </summary>
        /// <param name="ProviderName"></param>
        /// <param name="ConnectionString"></param>
        public Helper(string ProviderName, string ConnectionString)
        {
            SelectDatabase(ProviderName, ConnectionString);
        }

        #endregion

        #region ����һ��Helper2ʵ��, �������е�Helper2, �����Connection

        /// <summary>
        /// ����һ��Helper2ʵ��, �������е�Helper2, �����Connection
        /// </summary>
        /// <param name="MyHelper"></param>
        public Helper(Helper MyHelper)
        {
            MyConnection = MyHelper.MyConnection;
            MyDatabase = MyHelper.MyDatabase;
        }

        #endregion

        #region ѡ�����ݿ�ʵ������������

        /// <summary>
        /// ѡ�����ݿ�ʵ������������
        /// </summary>
        /// <param name="ProviderName"></param>
        /// <param name="ConnectionString"></param>
        private void SelectDatabase(string ProviderName, string ConnectionString)
        {
            switch (ProviderName.ToLower())
            {
                case "mysql":
                    MyDatabase = new MySQL();
                    break;
                case "sqlserver":
                case "sql":
                    MyDatabase = new SqlServer();
                    break;
                default:
                    throw new Exception("���ݿ����Ͳ�֧��!");
            }

            MyConnection = MyDatabase.CreateConnection(ConnectionString);

            MyConnection.Open();
        }

        #endregion

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="SQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int ExecuteDataSet(IDbCommand cmd, IDataAdapter adapter, string SQL, DataSet ds, params IDataParameter[] ParaList)
        {
            try
            {
                BuildCommand(cmd, SQL, ParaList);

                return adapter.Fill(ds);
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
        }

        #endregion

        #region ExecuteNoneQuery

        /// <summary>
        /// ִ��һ��SQL��䣬 �޷���ֵ
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="SQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int ExecuteNoneQuery(IDbCommand cmd, string SQL, params IDataParameter[] ParaList)
        {
            try
            {
                BuildCommand(cmd, SQL, ParaList);
                
                return cmd.ExecuteNonQuery();
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }

        }

        #endregion

        #region ExecuteEntity<TValue>

        /// <summary>
        /// ����һ�� TValue���͵�ʵ�����, �� GetEntity �������
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="ConnectionString"></param>
        /// <param name="Extracter"></param>
        /// <param name="SQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public TValue ExecuteEntity<TValue>(IDbCommand cmd, Converter<IDataRecord, TValue> Extracter, string SQL, params IDataParameter[] ParaList) where TValue : new()
        {
            try
            {
                TValue t = default(TValue);

                BuildCommand(cmd, SQL, ParaList);
                
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                t = Extracter(reader);
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                

                Extracter = null;

                return t;
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
        }

        #endregion

        #region ExecuteList<TValue>

        /// <summary>
        /// ����һ�� TValue���͵�List����, �� GetEntity �������
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="ConnectionString"></param>
        /// <param name="Extracter"></param>
        /// <param name="SQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<TValue> ExecuteList<TValue>(IDbCommand cmd, Converter<IDataRecord, TValue> Extracter, string SQL, params IDataParameter[] ParaList)
        {
            try
            {
                List<TValue> list = new List<TValue>();

                BuildCommand(cmd, SQL, ParaList);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TValue t = Extracter(reader);

                        if (t != null)
                            list.Add(t);
                    }
                }

                return list;
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, SQL);
            }
        }

        #endregion

        #region ˽�з���, ���� IDbCommand

        /// <summary>
        /// �ڲ�����, ��SQL����н�������, ����cmd.Parameter��
        /// </summary>
        /// <param name="values"></param>
        /// <param name="SQL"></param>
        /// <param name="cmd"></param>
        private void BuildCommand(IDbCommand cmd, string SQL, params object[] values)
        {
            //����󴫵�����
            if (values.Length > 0 && values[0] is IList)
            {
                IList list = values[0] as IList;

                object[] objList = new object[list.Count];

                for (int i = 0; i < objList.Length; i++)
                {
                    objList[i] = list[i];
                }

                BuildCommand(cmd, SQL, objList);
                return;
            }

            if (SQL.IndexOf("{0}") < 0)
            {
                cmd.CommandText = SQL;
                return;
            }

            StringBuilder sbSQL = new StringBuilder(SQL);

            for (int i = 0, j = values.Length; i < j; i++)
            {
                IDbDataParameter p = cmd.CreateParameter();
                p.ParameterName = MyDatabase.FormaterParameterName(i);
                p.Value = values[i];

                if (p.Value == null)
                    p.Value = System.DBNull.Value;
                cmd.Parameters.Add(p);

                sbSQL.Replace("{" + i + "}", p.ParameterName);
            }

            cmd.CommandText = sbSQL.ToString();

            sbSQL = null;
        }

        #endregion

    }
}
