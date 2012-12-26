using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Z.Ex;
using System.Collections;

namespace Z.DA
{
    /// <summary>
    /// ����������, �Զ�����Insert, Delete, Update, IsExist����, �ṩ��Ա�Ļ�������
    /// </summary>
    public class AbstractTable : AbstractDA
    {
        #region ˽�б���

        private DatabaseCache InsertCache;
        private DatabaseCache UpdateCache;
        private DatabaseCache DeleteCache;
        private DatabaseCache IsExistCache;

        /// <summary>
        /// ������
        /// </summary>
        protected string TableName;

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="tableName">���ݿ������</param>
        public AbstractTable(string ProviderName, string ConnectionString, string tableName)
            : base(ProviderName, ConnectionString)
        {
            InitTable(tableName);
        }

        /// <summary>
        /// �򵥱������
        /// </summary>
        /// <param name="ConnectionName">��app.config�ж�������ݿ���������</param>
        /// <param name="tableName">������</param>
        public AbstractTable(string ConnectionName, string tableName)
            : base(ConnectionName)
        {
            InitTable(tableName);
        }

        private void InitTable(string tableName)
        {
            TableName = tableName;
            string CacheKey = MyConnectionString + ":TBL:" + TableName;

            if (!DatabaseCache.Instance.ContainsKey(CacheKey + ":Insert"))
            {
                lock (DatabaseCache.Instance)
                {
                    if (!DatabaseCache.Instance.ContainsKey(CacheKey + ":Insert"))
                    {
                        using (IDbConnection conn = CreateConnection())
                        {
                            using (IDbCommand command = CreateCommand(conn, MyTransaction))
                            {
                                IDbDataAdapter adapter = MyDatabase.CreateAdapter(command);

                                try
                                {
                                    DatabaseCache insertCache, updateCache, deleteCache, isExistCache;
                                    DataTable TableSchema;

                                    MyDatabase.ExtractTableParameters(TableName,
                                        adapter,
                                        out insertCache,
                                        out deleteCache,
                                        out updateCache,
                                        out isExistCache,
                                        out TableSchema);

                                    DatabaseCache.Instance.Add(CacheKey + ":Insert", insertCache);
                                    DatabaseCache.Instance.Add(CacheKey + ":Update", updateCache);
                                    DatabaseCache.Instance.Add(CacheKey + ":Delete", deleteCache);
                                    DatabaseCache.Instance.Add(CacheKey + ":IsExist", isExistCache);
                                }
                                finally
                                {
                                    if (adapter is IDisposable)
                                        (adapter as IDisposable).Dispose();
                                }

                            }
                        }
                    }
                }
            }

            InsertCache = DatabaseCache.Instance[CacheKey + ":Insert"];
            UpdateCache = DatabaseCache.Instance[CacheKey + ":Update"];
            DeleteCache = DatabaseCache.Instance[CacheKey + ":Delete"];
            IsExistCache = DatabaseCache.Instance[CacheKey + ":IsExist"];
        }

        #endregion

        #region Insert

        /// <summary>
        /// �����������а����������������򷵻�������ţ� ���򷵻�1
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public int Insert(params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException("Insert�������ܴ����ֵ");

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = InsertCache.SQL;

                    AddParametersClone2Cmd(cmd, InsertCache.Parameters, values);

                    if (InsertCache.IsHaveAutoIncrement)
                        return Convert.ToInt32(cmd.ExecuteScalar());

                    return cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, DeleteCache.SQL);
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

        #region Insert<T>

        /// <summary>
        /// ��������ΪT�Ķ��󵽱��У�������а����������������򷵻�������ţ� ���򷵻�1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Insert<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException("Insert�������ܴ����ֵ");

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = InsertCache.SQL;

                    AddParametersClone2Cmd(cmd, InsertCache.Parameters, null);

                    EntityTools.ChangeType(value, cmd.Parameters);

                    int iRet = 0;

                    if (InsertCache.IsHaveAutoIncrement)
                        iRet = Convert.ToInt32(cmd.ExecuteScalar());
                    else
                        iRet = cmd.ExecuteNonQuery();

                    EntityTools.ChangeType(cmd.Parameters, value);

                    return iRet;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, DeleteCache.SQL);
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

        #region Delete

        /// <summary>
        /// �ӱ��и�������ɾ��һ����¼�� values˳����������˳��һ��
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Delete(params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException("Delete�������ܴ����ֵ");

            if (values.Length != DeleteCache.CurrentTable.PrimaryKey.Length)
                throw new ArgumentNullException("Delete���������������������ȷ,�޷���ȷɾ��");

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = DeleteCache.SQL;

                    AddParametersClone2Cmd(cmd, DeleteCache.Parameters, values);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, DeleteCache.SQL);
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

        #region Update

        /// <summary>
        /// �ӱ��и�����������һ����¼�� values˳�����ͱ��ֶε�����˳��һ��
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Update(params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException("Update�������ܴ����ֵ");

            if (values.Length < DeleteCache.CurrentTable.PrimaryKey.Length)
                throw new ArgumentNullException("Update���������������������ȷ,�޷���ȷ����");


            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = UpdateCache.SQL;

                    AddParametersClone2Cmd(cmd, UpdateCache.Parameters, null);

                    DataRow r = UpdateCache.CurrentTable.NewRow();

                    int j = UpdateCache.CurrentTable.Columns.Count;
                    if(j > values.Length)
                        j = values.Length;

                    for (int i = 0; i < j; i++)
                    {
                        r[i] = values[i];
                    }

                    foreach (IDbDataParameter p in cmd.Parameters)
                    {
                        if (UpdateCache.CurrentTable.Columns.Contains(p.SourceColumn))
                        {
                            p.Value = r[p.SourceColumn];
                        }
                    }

                    return cmd.ExecuteNonQuery() > 0;

                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, UpdateCache.SQL);
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

        #region Update<T>

        /// <summary>
        /// �ӱ��и�����������һ����¼�� ����֪������ת��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Update<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException("Update�������ܴ����ֵ");

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = UpdateCache.SQL;

                    AddParametersClone2Cmd(cmd, UpdateCache.Parameters, null);

                    EntityTools.ChangeType(value, cmd.Parameters);

                    return cmd.ExecuteNonQuery() > 0;

                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, UpdateCache.SQL);
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

        #region IsExist

        /// <summary>
        /// �ӱ��и����������һ����¼�Ƿ���ڣ� values˳����������˳��һ��
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool IsExist(params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException("IsExist�������ܴ����ֵ");

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = IsExistCache.SQL;

                    AddParametersClone2Cmd(cmd, IsExistCache.Parameters, values);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, IsExistCache.SQL);
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

        #region IsExist<T>

        /// <summary>
        /// �ӱ��и����������һ����¼�Ƿ���ڣ� values˳����������˳��һ��
        /// </summary>
        /// <typeparam name="T">����T</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsExist<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException("IsExist�������ܴ����ֵ");

            IDbConnection connection = CreateConnection();

            try
            {
                using (IDbCommand cmd = CreateCommand(connection, MyTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = IsExistCache.SQL;

                    AddParametersClone2Cmd(cmd, IsExistCache.Parameters, null);

                    EntityTools.ChangeType(value, cmd.Parameters);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex, IsExistCache.SQL);
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
        
        #region ��ǰ�ı�ṹ���������κ�����

        /// <summary>
        /// ��ǰ�ı�ṹ���������κ�����
        /// </summary>
        protected DataTable CurrentTable
        {
            get
            {
                return InsertCache.CurrentTable.Clone();
            }
        }

        #endregion

        #region Save

        /// <summary>
        /// ����ʵ��, ���ʵ���Ѵ���, �����, �������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>1-����, 2-����</returns>
        public virtual int Save<T>(T t)
        {
            if (IsExist<T>(t))
            {
                Update<T>(t);
                return 1;

            }


            return Insert<T>(t);
        }

        #endregion

        #region TableSysn<T>

        /// <summary>
        /// ��������ΪT�Ķ��������ݿ����Ƿ����, ������������, �����������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">����ΪT�Ķ�������</param>
        /// <returns></returns>
        public int TableSync<T>(IEnumerable list)
        {
            int iCount = 0;

            if (InsertCache.IsHaveAutoIncrement)
                throw new DatabaseException("�޷�ͬ�����������ı�", null, string.Empty);

            foreach (T t in list)
            {
                if (IsExist<T>(t)) 
                    Update<T>(t);
                else 
                    Insert<T>(t);

                iCount++;
            }

            return iCount;
        }

        #endregion

        
    }
}
