using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Z.DA
{
    /// <summary>
    /// �洢���̵ķ���ֵ����
    /// </summary>
    [Serializable]
    public class StoreProcedureInfo
    {
        /// <summary>
        /// ��������ֵ�
        /// </summary>
        public Dictionary<string, object> OutputParameters = new Dictionary<string, object>();

        /// <summary>
        /// ����ֵ
        /// </summary>
        public int ReturnCode;

        /// <summary>
        /// �ڴ洢������Select�Ľ����
        /// </summary>
        public DataSet DataSet;
    }
    
    /// <summary>
    /// ���ڷ��͵Ĵ洢���̷���ֵ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class StoreProcedureList<T>
    {
        /// <summary>
        /// ��������ֵ�
        /// </summary>
        public Dictionary<string, object> OutputParameters = new Dictionary<string, object>();

        /// <summary>
        /// ����ֵ
        /// </summary>
        public int ReturnCode;

        /// <summary>
        /// �ڴ洢������Select�Ľ����
        /// </summary>
        public List<T> List = new List<T>();
    }
}
