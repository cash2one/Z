using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Z.DA
{
    internal class DatabaseCache
    {
        /// <summary>
        /// ��̬��䣬 ���ڻ�������ִ�й������
        /// </summary>
        internal static Dictionary<string, DatabaseCache> Instance = new Dictionary<string, DatabaseCache>();

        /// <summary>
        /// �߳̾�̬���ӳ�
        /// </summary>
        [ThreadStatic]
        private static Dictionary<string, IDbConnection> connectionList;

        /// <summary>
        /// �߳̾�̬���ӳ�
        /// </summary>
        internal static Dictionary<string, IDbConnection> ThreadConnectionPool
        {
            get
            {
                if (connectionList == null)
                {
                    connectionList = new Dictionary<string, IDbConnection>();
                }

                return connectionList;
            }
        }

        public string SQL;
        public IDataParameter[] Parameters;
        public DataTable CurrentTable;
        public bool IsHaveAutoIncrement;

        public DatabaseCache(string text, IDataParameter[] ParameterList)
        {
            SQL = text;
            Parameters = ParameterList;
        }

        public DatabaseCache(string text, IDataParameterCollection ParameterList)
        {
            SQL = text;

            Parameters = new IDataParameter[ParameterList.Count];

            ParameterList.CopyTo(Parameters, 0);
        }
    }
}
