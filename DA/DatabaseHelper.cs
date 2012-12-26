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
    /// <summary>
    /// ���ݿ���ʹ��ߣ� ��SQLHelper�ķ��ʷ���һ��
    /// </summary>
    public static class DatabaseHelper
    {
        #region ExecuteDataSet

        /// <summary>
        /// ִ��SQL���, ����һ��DataSet����
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string ProviderName, string ConnectionString, string SQL, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteDataSet(SQL, values);
        }


        /// <summary>
        /// ִ��SQL���, ����һ��DataSet����
        /// </summary>
        /// <param name="config">���ݿ�����������</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(DatabaseConfiguration config, string SQL, params object[] values)
        {
            return new AbstractDA(config.ProviderName, config.ConnectionString).ExecuteDataSet(SQL, values);
        }

        #endregion

        #region ExecuteNoneQuery

        /// <summary>
        /// ����:ִ��һ��SQL���, ����Ӱ���������
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static int ExecuteNoneQuery(string ProviderName, string ConnectionString, string SQL, params object[] values)
        {
                return new AbstractDA(ProviderName, ConnectionString).ExecuteNoneQuery(SQL, values);
        }


        /// <summary>
        /// ����:ִ��һ��SQL���, ����Ӱ���������
        /// </summary>
        /// <param name="config">���ݿ�����������</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static int ExecuteNoneQuery(DatabaseConfiguration config, string SQL, params object[] values)
        {
            return new AbstractDA(config.ProviderName, config.ConnectionString).ExecuteNoneQuery(SQL, values);
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ���� T ���͵ĵ�ֵ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string ProviderName, string ConnectionString, string SQL, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteScalar<T>(SQL, values);
        }

        #endregion

        #region ExecuteEntity

        /// <summary>
        /// ExecuteEntity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="Extracter">���������</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static T ExecuteEntity<T>(string ProviderName, string ConnectionString, Converter<IDataRecord, T> Extracter, string SQL, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteEntity<T>(Extracter, SQL, values);
        }

        #endregion

        #region ExecuteList

        /// <summary>
        /// ����һ��T���͵�List����, �� Extracter �������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="Extracter">T���������</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static List<T> ExecuteList<T>(string ProviderName, string ConnectionString, Converter<IDataRecord, T> Extracter, string SQL, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteList<T>(Extracter, SQL, values);
        }

        #endregion

        #region ExecuteProcedureNonQuery

        /// <summary>
        /// ִ�д洢���̣� ֻ���ش洢���̷���ֵ
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns>�洢���̷���ֵ</returns>
        public static int ExecuteProcedureNonQuery(string ProviderName, string ConnectionString, string ProcedureName, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteProcedureNonQuery(ProcedureName, values);
        }

        #endregion

        #region ExecuteProcedure

        /// <summary>
        /// ִ�д洢���̣� ������DataSet
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static StoreProcedureInfo ExecuteProcedure(string ProviderName, string ConnectionString, string ProcedureName, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteProcedure(ProcedureName, values);
        }


        #endregion

        #region ExecuteProcedureDataSet

        /// <summary>
        /// ִ�д洢���̣� ������DataSet
        /// </summary>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static StoreProcedureInfo ExecuteProcedureDataSet(string ProviderName, string ConnectionString, string ProcedureName, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteProcedureDataSet(ProcedureName, values);
        }

        #endregion

        #region ExecuteProcedureList<T>

        /// <summary>
        /// ִ�д洢���̣� ��������Ϊ T ��List�б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns></returns>
        public static StoreProcedureList<T> ExecuteProcedureList<T>(string ProviderName, string ConnectionString, string ProcedureName, params object[] values) where T : new()
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteProcedureList<T>(ProcedureName, EntityTools.ChangeType<T>, values);
        }

        /// <summary>
        /// ִ�д洢���̣� ��������Ϊ T ��List�б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="ProcedureName">�洢��������</param>
        /// <param name="Extracter">���������</param>
        /// <param name="values">�����ֵ�б�, ���մ洢����in���Ͳ�����˳��ֵ������out����</param>
        /// <returns></returns>
        public static StoreProcedureList<T> ExecuteProcedureList<T>(string ProviderName, string ConnectionString, string ProcedureName, Converter<IDataRecord, T> Extracter, params object[] values)
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteProcedureList<T>(ProcedureName, Extracter, values);
        }

        #endregion


        #region ���ط���

        /// <summary>
        /// ����һ��T���͵Ķ���, ʹ�����õ�EntityTools.ChangeType������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static T ExecuteEntity<T>(string ProviderName, string ConnectionString, string SQL, params object[] values) where T : new()
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteEntity<T>(EntityTools.ChangeType<T>, SQL, values);
        }

        /// <summary>
        /// ����һ��T���͵�List����, ʹ�����õ�EntityTools.ChangeType������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ProviderName">mysql����sqlserver�� �����ִ�Сд</param>
        /// <param name="ConnectionString">���ݿ������ַ���</param>
        /// <param name="SQL">SQL���������, ��string.format��ʽһ��</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public static List<T> ExecuteList<T>(string ProviderName, string ConnectionString, string SQL, params object[] values) where T : new()
        {
            return new AbstractDA(ProviderName, ConnectionString).ExecuteList<T>(EntityTools.ChangeType<T>, SQL, values);
        }

        #endregion

        #region ��ʾ�����е�SQL���

        /// <summary>
        /// ��ʾ�����е�SQL���
        /// </summary>
        /// <returns></returns>
        public static string ShowCache()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string k in DatabaseCache.Instance.Keys)
            {
                sb.AppendFormat("[{0}]:{1}", k, DatabaseCache.Instance[k].SQL);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion

    }
}
