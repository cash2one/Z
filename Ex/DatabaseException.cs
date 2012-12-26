using System;
using System.Collections.Generic;
using System.Text;
using Z.Util;

namespace Z.Ex
{
    /// <summary>
    /// ���ݿ�����쳣 
    /// </summary>
    public class DatabaseException : Exceptionbase
    {
        /// <summary>
        /// �����쳣��SQL���
        /// </summary>
        public string SQL;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="sql"></param>
        public DatabaseException(string msg, Exception ex, string sql)
            : base(msg, ex)
        {
            SQL = sql;
        }

        /// <summary>
        /// ��������䵽XML����
        /// </summary>
        /// <param name="sbXml"></param>
        public override void ToXmlElements(StringBuilder sbXml)
        {
            base.ToXmlElements(sbXml);

            sbXml.Append(StringTools.GetNameValue("SQL", SQL));

        }
    }
}
