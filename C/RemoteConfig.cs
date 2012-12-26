using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;

namespace Z.C
{
    /// <summary>
    /// ���ڿ���Զ������
    /// </summary>
    public class RemoteConfig : MarshalByRefObject
    {
        #region ��ȡԶ��������Ϣ

        /// <summary>
        /// ��ȡԶ��������Ϣ
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pwd">��������</param>
        /// <returns></returns>
        public string GetConfig(string s, string pwd)
        {
            try
            {
                if (string.Compare(pwd, AppConfigHandler.RemotePassword) != 0)
                    return string.Empty;

                if (!AppConfigHandler.Dict.ContainsKey(s))
                    return string.Empty;

                return Z.Util.XmlTools.ToXmlInternal(AppConfigHandler.Dict[s], string.Empty, true, false, true);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region ����Զ��������Ϣ

        /// <summary>
        /// ����Զ��������Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Content"></param>
        /// <param name="ConfigFileName"></param>
        /// <param name="pwd">Զ����������</param>
        /// <returns></returns>
        public string SaveConfig(string key, string Content, string ConfigFileName, string pwd)
        {
            if (string.Compare(pwd, AppConfigHandler.RemotePassword) != 0)
                return string.Empty;

            try
            {
                if (!AppConfigHandler.Dict.ContainsKey(key))
                    return string.Empty;

                object o = AppConfigHandler.Dict[key];

                if (o == null)
                    return string.Empty;

                object Config = Z.Util.XmlTools.FromXml(Content, o.GetType());

                if (Config == null)
                    return string.Empty;

                AppConfigHandler.Dict[key] = Config;

                AppConfigHandler.SaveConfig(ConfigFileName, true, Config);

                return "�ɹ�!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region ����Զ�������ļ��Ƿ���ȷ

        /// <summary>
        /// ���Խ��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Content"></param>
        /// <param name="pwd">Զ����������</param>
        /// <returns></returns>
        public bool TestConfig(string key, string Content, string pwd)
        {
            try
            {
                if (!AppConfigHandler.Dict.ContainsKey(key))
                    return false;

                object o = AppConfigHandler.Dict[key];

                if (o == null)
                    return false;

                object Config = Z.Util.XmlTools.FromXml(Content, o.GetType());

                if (Config == null)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetServerVersions()
        {
            return CheckServerVersions().ToString();
        }

        internal static StringBuilder CheckServerVersions()
        {
            StringBuilder sbServerVersions = new StringBuilder();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            sbServerVersions.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbServerVersions.AppendLine("<Files>");

            foreach (Assembly assembly in assemblies)
            {
                if (assembly.GlobalAssemblyCache == false)
                {
                    sbServerVersions.AppendLine("<File ");

                    FileInfo assemblyFile = new FileInfo(assembly.Location);
                    AssemblyName an = assembly.GetName();

                    sbServerVersions.AppendFormat(" FileName=\"{0}\"", assemblyFile.Name);
                    sbServerVersions.AppendFormat(" Version=\"{0}\"", an.Version);
                    sbServerVersions.AppendFormat(" CreateTime=\"{0}\"", assemblyFile.CreationTime);
                    sbServerVersions.AppendFormat(" LastWriteTime=\"{0}\"", assemblyFile.LastWriteTime);
                    sbServerVersions.AppendLine(" />");
                }
            }
            sbServerVersions.AppendLine("</Files>");

            return sbServerVersions;
        }
    }
}
