using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Z.Ex;

namespace Z.DA
{
    /// <summary>
    /// ����֧����
    /// </summary>
    public class TransScope : IDisposable
    {
        #region �ڲ�����

        /// <summary>
        /// AbstractDA�б�����ȷ����ЩDA��Ҫ��������ִ��
        /// </summary>
        List<AbstractDA> AbstractDAList = new List<AbstractDA>();

        /// <summary>
        /// �����б� ����Э����ͬ���ݿ����ӵ�����
        /// </summary>
        Dictionary<string, IDbTransaction> TransactionList = new Dictionary<string, IDbTransaction>();

        /// <summary>
        /// ��Ҫ�رյ����ݿ������б�
        /// </summary>
        List<IDbConnection> NeedCloseConnection = new List<IDbConnection>();

        private bool IsDispose = false;

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯���� ����������Ҫִ�������AbstractDA��, Ĭ��������뼶����IsolationLevel.RepeatableRead
        /// </summary>
        /// <param name="list"></param>
        public TransScope(params AbstractDA[] list) : this(IsolationLevel.RepeatableRead, list) {}

        /// <summary>
        /// ���캯���� ����������Ҫִ�������AbstractDA��
        /// </summary>
        /// <param name="TransLevel">������뼶��</param>
        /// <param name="list"></param>
        public TransScope(IsolationLevel TransLevel, params AbstractDA[] list)
        {
            //Ѱ�ҵ�ǰ���õ����ӣ� ���뵽Transaction��
            foreach (AbstractDA da in list)
            {
                if (!TransactionList.ContainsKey(da.MyConnectionString))
                {
                    if (da.MyConnection != null && da.MyConnection.State == ConnectionState.Open)
                    {
                        IDbTransaction trans = da.MyConnection.BeginTransaction(TransLevel);

                        TransactionList.Add(da.MyConnectionString, trans);
                    }
                }
            }

            foreach (AbstractDA da in list)
            {
                //��ǰ�����ַ�����û�д�������
                if (!TransactionList.ContainsKey(da.MyConnectionString))
                {
                    IDbConnection connection = da.MyDatabase.CreateConnection(da.MyConnectionString);
                    connection.Open();

                    NeedCloseConnection.Add(connection);
                    TransactionList.Add(
                        da.MyConnectionString,
                        connection.BeginTransaction(TransLevel));
                }

                da.MyTransaction = TransactionList[da.MyConnectionString];

                AbstractDAList.Add(da);
            }
        }

        #endregion

        #region �ύ����

        /// <summary>
        /// �ύ����
        /// </summary>
        public void Commit()
        {
            if (!IsDispose)
            {
                try
                {
                    foreach (IDbTransaction trans in TransactionList.Values)
                    {
                        trans.Commit();
                        trans.Dispose();
                    }

                    foreach (IDbConnection conn in NeedCloseConnection)
                    {
                        conn.Close();
                        conn.Dispose();
                    }

                    foreach (AbstractDA da in AbstractDAList)
                    {
                        da.MyTransaction = null;
                    }

                    AbstractDAList.Clear();
                    TransactionList.Clear();
                }
                finally
                {
                    IsDispose = true;
                }
            }
            else
            {

            }
        }

        #endregion

        #region IDisposable�ӿ�

        /// <summary>
        /// ������������
        /// </summary>
        public void Dispose()
        {
            if (IsDispose == false)
            {
                //Ĭ�ϻع�
                foreach (IDbTransaction trans in TransactionList.Values)
                {
                    trans.Rollback();
                    trans.Dispose();
                }

                foreach (IDbConnection conn in NeedCloseConnection)
                {
                    conn.Close();
                    conn.Dispose();
                }

                foreach (AbstractDA da in AbstractDAList)
                {
                    da.MyTransaction = null;
                }

                IsDispose = true;
            }
        }

        #endregion
    }
}
