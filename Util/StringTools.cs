using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Z.Util
{
    /// <summary>
    /// �ṩ��ͨString��
    /// δ���ṩ�ĳ��ù���,���๦�ܿ��Բο�Microsoft.VisualBasic�����ռ��µ�Strings��
    /// </summary>
    public static class StringTools
    {
        #region ��ȡ�ַ�����ָ���������ֽڳ���

        /// <summary>
        /// ��ȡ�ַ�����ָ���������ֽڳ���
        /// ������Ҫ�������ݿ���ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoder"></param>
        /// <returns></returns>
        public static int ByteLength(string str, Encoding encoder)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte[] b = encoder.GetBytes(str);
                return b.Length;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region ����ַ�����һ���ַ��ָ��õ�������

        /// <summary>
        /// ����ַ�����һ���ַ��ָ��õ�������
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string[] Split(string str, string split)
        {
            return str.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion

        #region �õ�ָ�����ƺ�ֵ�ĸ�ֵ�ַ���

        /// <summary>
        /// �õ�ָ�����ƺ�ֵ�ĸ�ֵ�ַ���
        /// </summary>
        /// <param name="Name">��������</param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <author>����</author>
        public static string GetNameValue(string Name, object Value)
        {
            if (Value != null)
                return string.Format("<{0}>{1}</{0}>", Name, System.Web.HttpUtility.HtmlEncode(Value.ToString()));
            else
                return string.Format("<{0}/>", Name);
        }

        #endregion

        #region ���ַ�������JavaScript��escape�����ķ�ʽ���ַ������б���

        /// <summary>
        /// ���ַ�������JavaScript��escape�����ķ�ʽ���ַ������б��룬
        /// �Ա�����C#�б�����ַ�����JavaScript��Ҳ����ȷ����
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string JSEscape(string strIn)
        {
            string tmp = "";
            foreach (char c in strIn)
            {
                int i = Convert.ToInt32(c);
                if (i > 127)
                    tmp += "%u" + i.ToString("X4");
                else
                {
                    if (char.IsLetterOrDigit(c))
                        tmp += c;
                    else
                        tmp += "%" + i.ToString("X2");
                }
            }
            return tmp;
        }

        #endregion

        #region ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���

        /// <summary>
        /// ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString(DataSet ds, string TableName, string ColumnName, string Prefix, string Suffix, string Spliter)
        {
            if (!ds.Tables.Contains(TableName))
                return "";

            if (!ds.Tables[TableName].Columns.Contains(ColumnName))
                return "";

            return JoinString(ds.Tables[TableName], ColumnName, Prefix, Suffix, Spliter);
        }

        /// <summary>
        /// ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ColumnName"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString(DataTable dt, string ColumnName, string Prefix, string Suffix, string Spliter)
        {
            if (!dt.Columns.Contains(ColumnName))
                return "";

            return JoinString(dt.Rows, ColumnName, Prefix, Suffix, Spliter);

        }

        /// <summary>
        /// ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <param name="Columns"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <param name="IsAppendAutoIncrement">�Ƿ񸽼�����������</param>
        /// <returns></returns>
        public static string JoinString(DataColumnCollection Columns, string Prefix, string Suffix, string Spliter, bool IsAppendAutoIncrement)
        {
            if (Columns.Count <= 0)
                return string.Empty;

            StringBuilder strJoin = new StringBuilder();

            int i = 0;
            int j = Columns.Count - 1;
            for (; i < j; i++)
            {
                DataColumn c = Columns[i];

                if (c.AutoIncrement && IsAppendAutoIncrement == false)
                    continue;

                strJoin.Append(Prefix);
                strJoin.Append(c.ColumnName);
                strJoin.Append(Suffix);
                strJoin.Append(Spliter);
            }

            strJoin.Append(Prefix);
            strJoin.Append(Columns[i].ColumnName);
            strJoin.Append(Suffix);

            return strJoin.ToString();
        }

        /// <summary>
        /// ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <param name="Columns"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <param name="IsAppendAutoIncrement">�Ƿ񸽼�����������</param>
        /// <returns></returns>
        public static string JoinString(DataColumn[] Columns, string Prefix, string Suffix, string Spliter, bool IsAppendAutoIncrement)
        {
            if (Columns.Length <= 0)
                return string.Empty;

            StringBuilder strJoin = new StringBuilder();

            int i = 0;
            int j = Columns.Length - 1;
            for (; i < j; i++)
            {
                DataColumn c = Columns[i];

                if (c.AutoIncrement && IsAppendAutoIncrement == false)
                    continue;

                strJoin.Append(Prefix);
                strJoin.Append(c.ColumnName);
                strJoin.Append(Suffix);
                strJoin.Append(Spliter);
            }

            strJoin.Append(Prefix);
            strJoin.Append(Columns[i].ColumnName);
            strJoin.Append(Suffix);

            return strJoin.ToString();
        }

        /// <summary>
        /// ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="ColumnName"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString(DataRowCollection rs, string ColumnName, string Prefix, string Suffix, string Spliter)
        {
            if (rs.Count <= 0)
                return "";

            if (!rs[0].Table.Columns.Contains(ColumnName))
                return "";

            string strJoin = "";

            foreach (DataRow r in rs)
            {
                if (!r.IsNull(ColumnName))
                {
                    string strTemp = Prefix;

                    strTemp += r[ColumnName].ToString();

                    strTemp += Suffix;

                    strJoin += strTemp + Spliter;
                }

            }

            return strJoin;
        }

        /// <summary>
        /// ���� ָ���� ���� �� �ֶ��� �����е����ۼ�, ����ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="ColumnName"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString(DataRow[] rs, string ColumnName, string Prefix, string Suffix, string Spliter)
        {
            if (rs.Length <= 0)
                return "";

            if (!rs[0].Table.Columns.Contains(ColumnName))
                return "";

            string strJoin = "";

            foreach (DataRow r in rs)
            {
                if (!r.IsNull(ColumnName))
                {
                    string strTemp = Prefix;

                    strTemp += r[ColumnName].ToString();

                    strTemp += Suffix;

                    strJoin += strTemp + Spliter;
                }

            }

            if (strJoin.EndsWith(Spliter))
                strJoin = strJoin.Substring(0, strJoin.Length - Spliter.Length);

            return strJoin;
        }

        #endregion

        #region ���� ָ���� ���� ���������鰴��ָ���Ĺ��� �ۼ�Ϊ�ַ���

        /// <summary>
        /// ���� ָ���� ���� ���������鰴��ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString<T>(T[] ts, string Prefix, string Suffix, string Spliter)
        {
            StringBuilder sb = new StringBuilder();

            if (ts == null || ts.Length == 0)
                return string.Empty;

            for (int i = 0; i < ts.Length - 1; i++)
            {
                sb.Append(string.Format("{0}{1}{2}{3}",
                    Prefix, ts[i].ToString(), Suffix, Spliter));
            }

            sb.Append(string.Format("{0}{1}{2}",
                Prefix, ts[ts.Length - 1].ToString(), Suffix));

            return sb.ToString();
        }

        /// <summary>
        /// ���� ָ���� ���� ���������鰴��ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString<T>(List<T> ts, string Prefix, string Suffix, string Spliter)
        {
            StringBuilder sb = new StringBuilder();

            if (ts == null || ts.Count == 0)
                return string.Empty;

            for (int i = 0; i < ts.Count - 1; i++)
            {
                sb.Append(string.Format("{0}{1}{2}{3}",
                    Prefix, ts[i].ToString(), Suffix, Spliter));
            }

            sb.Append(string.Format("{0}{1}{2}",
                Prefix, ts[ts.Count - 1].ToString(), Suffix));

            return sb.ToString();
        }

        /// <summary>
        /// ���� ָ���� ���� ���������鰴��ָ���Ĺ��� �ۼ�Ϊ�ַ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="Prefix"></param>
        /// <param name="Suffix"></param>
        /// <param name="Spliter"></param>
        /// <returns></returns>
        public static string JoinString<T>(IEnumerable<T> ts, string Prefix, string Suffix, string Spliter)
        {
            StringBuilder sb = new StringBuilder();

            if (ts == null)
                return string.Empty;

            foreach (T t in ts)
            {
                sb.Append(string.Format("{0}{1}{2}{3}",
                 Prefix, t.ToString(), Suffix, Spliter));
            }

            if (sb.ToString().EndsWith(Suffix + Spliter))
            {
                string s = Suffix + Spliter;

                sb.Remove(sb.Length - s.Length, s.Length);
            }

            return sb.ToString();
        }

        #endregion

        #region �ж��ַ����Ƿ�Ϊ����

        /// <summary>
        /// �ж��ַ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNumber(string source)
        {
            Regex reg = new Regex(@"^\d+$", RegexOptions.IgnoreCase);
            return reg.IsMatch(source);
        }

        #endregion

        #region �ж��ַ����Ƿ�Ϊ��С��������

        /// <summary>
        /// �ж��ַ����Ƿ�Ϊ��С��������
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDecimal(string source)
        {
            if (source == "")
                return false;

            Regex reg = new Regex(@"^\d*(\.\d*)?$", RegexOptions.IgnoreCase);
            return reg.IsMatch(source);
        }


        #endregion

        #region �ж��ַ����Ƿ����������ʽҪ�󣨲����ִ�Сд��

        /// <summary>
        /// �ж��ַ����Ƿ����������ʽҪ�󣨲����ִ�Сд��
        /// </summary>
        /// <param name="source"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsValide(string source, string regex)
        {
            if (source == "")
                return false;

            Regex reg = new Regex(regex, RegexOptions.IgnoreCase);
            return reg.IsMatch(source);
        }

        #endregion

        #region ��ʽ������ WebForm ������еķǷ��ַ�

        /// <summary>
        /// ��ʽ������ WebForm ������еķǷ��ַ�
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string InputText(string inputString)
        {
            StringBuilder retVal = new StringBuilder();
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");
            }
            return retVal.ToString().Trim();
        }

        #endregion

        #region �ָ��ַ����Ĵ�������������ӡ�����ɾ�� ��
        /// <summary>
        /// �� �ָ����ַ����м����µ���,����ӵ�����ظ�
        /// </summary>
        /// <param name="source">ԭ�ַ���</param>
        /// <param name="split">�ָ���</param>
        /// <param name="item">�¼ӵ���</param>
        /// <returns></returns>
        public static string AddSplitItem(string source, string split, string item)
        {
            if (source.IndexOf(item) >= 0)
                return source;
            if (source == "")
            {
                return item;
            }
            else
            {
                return source + split + item;
            }
        }

        /// <summary>
        /// �� �ָ����ַ�����ɾ����
        /// </summary>
        /// <param name="source">ԭ�ַ���</param>
        /// <param name="split">�ָ���</param>
        /// <param name="item">�¼ӵ���</param>
        /// <returns></returns>
        public static string RemoveSplitItem(string source, string split, string item)
        {
            if (source.IndexOf(item) == -1)
            {
                return source;
            }
            else
            {
                //�����β
                if (source.IndexOf(split) == 0)
                {
                    source = source.Remove(0, split.Length);
                }
                if (source.LastIndexOf(split) == source.Length - split.Length)
                    source = source.Remove(source.Length - split.Length, split.Length);

                source = source.Replace(item, "");
                source = source.Replace(split + split, split);

                return source;
            }
        }

        #endregion

        #region �޳� Like ģ����ѯ�еķǷ�Bug�ַ� (��Sql Server �汾 )
        /// <summary>
        /// �޳� Like ģ����ѯ�еķǷ�Bug�ַ� (��Sql Server �汾 )
        /// </summary>
        /// <param name="sqlStr">SQL ��ѯ�ַ���</param>
        /// <returns>���ع��˺�ĺϸ��ַ�</returns>
        public static string ToLikeSql(string sqlStr)
        {
            if (sqlStr == null) return "";
            StringBuilder b = new StringBuilder(sqlStr);
            b.Replace("'", "''");
            b.Replace("[", "[[]");
            b.Replace("%", "[%]");
            b.Replace("_", "[_]");
            return b.ToString();
        }
        #endregion

        #region ��ʽ��Sql����е�Where or /Where and ����

        /// <summary>
        /// ��ʽ��Sql������
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static string FormatSql(string strSql)
        {
            if (strSql.EndsWith("where"))
                strSql = strSql.Replace("where", "");
            if (strSql.IndexOf("where and") > 0)
                strSql = strSql.Replace("where and", "where");
            if (strSql.IndexOf("where or") > 0)
                strSql = strSql.Replace("where or", "where");
            return strSql;
        }

        #endregion

        #region ɾ����βָ��������

        /// <summary>
        /// ɾ����βָ�����ַ���
        /// </summary>
        /// <param name="sbIn"></param>
        /// <param name="end">Ҫɾ������β���ַ���</param>
        public static void TrimEnd(StringBuilder sbIn, string end)
        {
            if (sbIn.ToString().EndsWith(end))
                sbIn.Remove(sbIn.Length - end.Length, end.Length);
        }

        #endregion

        

        #region ��ָ���Ķ������л�Ϊ�����Ƶ�BASE64�����ַ���

        /// <summary>
        /// ��ָ���Ķ������л�Ϊ�����Ƶ�BASE64�����ַ���
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToObjectBase64String(object o)
        {
            string s = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(ms, o);
                s = Convert.ToBase64String(ms.ToArray());

                ms.Close();
            }

            return s;
        }

        /// <summary>
        /// ��ָ���Ķ���ת��ΪXml�ļ�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T FromObjectBase64String<T>(string s) where T : class
        {
            byte[] bytes = Convert.FromBase64String(s);
            object o = null;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                o = (T)bf.Deserialize(ms);
                ms.Close();
            }

            return o as T;
        }

        #endregion

        #region ȷ��ĳ�ַ����Ƿ����

        /// <summary>
        /// ȷ��ĳ�ַ����Ƿ����
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strFinds"></param>
        /// <returns></returns>
        public static string Conterns(string strA, string[] strFinds)
        {
            string strFind = string.Format(",{0},", strA);
            foreach (string s in strFinds)
            {
                if (strFind.IndexOf(',' + s + ',') >= 0)
                    return s;
            }

            return "";
        }

        #endregion

        #region ��DataSetת��ΪTXT��ʽ

        /// <summary>
        /// ��DataSetת��ΪTXT��ʽ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static StringBuilder ToStringBuilder(DataTable dt)
        {
            int RowCount = dt.Rows.Count;
            int ColCount = dt.Columns.Count;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < RowCount; i++)
            {
                DataRow r = dt.Rows[i];
                for (int j = 0; j < ColCount; j++)
                {
                    sb.AppendFormat("{0}\t", r[dt.Columns[j]]);
                }

                sb.AppendLine();
            }

            return sb;
        }

        #endregion

        /// <summary>
        /// �ж������ַ��У��Ƿ��������
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainChineseString(string s)
        {
            Regex rx = new Regex("^[\u4e00-\u9fa5]$");
            for (int i = 0; i < s.Length; i++)
            {
                if ( rx.IsMatch(s[i].ToString()))
                    return true ;
            }
            return false;
        }
    }
}
