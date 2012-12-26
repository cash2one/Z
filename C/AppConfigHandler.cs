using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Xml;
using System.Xml.Serialization;
using Z.Log;
using Z.Util;
using System.Diagnostics;
using System.Text;

namespace Z.C
{
    /// <summary>
    /// ͨ�����ö�ȡ��
    /// </summary>
    public class AppConfigHandler : IConfigurationSectionHandler
    {
        #region ��̬����ʵ��

        private static readonly Logger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static Dictionary<string, object> Dict;

        static AppConfigHandler()
        {
            Dict = new Dictionary<string, object>();
            
        }

        #endregion

        #region ʵ�� IConfigurationSectionHandler �ӿڷ����� ��ֱ�ӵ���

        /// <summary>
        /// ʵ�� IConfigurationSectionHandler �ӿڷ����� ��ֱ�ӵ���
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        [Obsolete("AppConfigHandler.Createʵ�� IConfigurationSectionHandler �ӿڷ����� ��ֱ�ӵ���", true)]
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            //XPathNavigator nav = section.CreateNavigator();
            //string typename = (string)nav.Evaluate("string(@type)");
            //Type t = Type.GetType(typename);
            //XmlSerializer ser = XmlTools.GetXmlSerializer(t);
            //return ser.Deserialize(new XmlNodeReader(section));
            return section;
        }

        #endregion

        #region �����ļ���ȡ�ӿڣ�������ʵ�֣�

        /// <summary>
        /// ��.NET��׼�����ļ��н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetConfig<T>() where T : class, new()
        {
            return GetConfigInternal<T>(typeof(T).Name, 0, string.Empty, string.Empty, false);
        }

        /// <summary>
        /// ��ָ����XML�ļ��н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigFileName">�������������ļ�·��</param>
        /// <returns></returns>
        public static T GetConfig<T>(string ConfigFileName) where T : class, new()
        {
            return GetConfigInternal<T>(typeof(T).Name, 1, ConfigFileName, string.Empty, false);
        }

        /// <summary>
        /// �ӵ�ǰִ�е�·���ж�ȡ�ļ����ҽ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FileNameWithoutPath">�ļ������Ʋ�����·��</param>
        /// <param name="IsCheckParent">�Ƿ��ѯ��·���µ��ļ�</param>
        /// <returns></returns>
        public static T GetConfig<T>(string FileNameWithoutPath, bool IsCheckParent) where T : class, new()
        {
            return GetConfigInternal<T>(typeof(T).Name, 3, FileNameWithoutPath, string.Empty, IsCheckParent);
        }

        /// <summary>
        /// ��ָ����XML�ļ���Section�н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigFileName"></param>
        /// <param name="XPath"></param>
        /// <returns></returns>
        public static T GetConfig<T>(string ConfigFileName, string XPath) where T : class, new()
        {
            return GetConfigInternal<T>(typeof(T).Name, 2, ConfigFileName, XPath, false);
        }

        #endregion

        #region �������������ö���

        /// <summary>
        /// �������������ö���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigName"></param>
        /// <param name="FromType"></param>
        /// <param name="ConfigFileName"></param>
        /// <param name="XPath"></param>
        /// <param name="IsCheckParent"></param>
        /// <returns></returns>
        private static T GetConfigInternal<T>(string ConfigName, int FromType, string ConfigFileName, string XPath, bool IsCheckParent) where T : class, new()
        {
            T o = null;

            //TODO: ��ǰ�ǲ������ö��������������Ϊ�����, �������һ��Ӧ�ô�������ͬ��λ�ö�ȡ��ͬ���͵Ļ����ʱ, �������ͻ.
            if (Dict.ContainsKey(ConfigName))
                return Dict[ConfigName] as T;

            lock (Dict)
            {
                if (!Dict.ContainsKey(ConfigName))
                {
                    switch (FromType)
                    {
                        case 1:
                            o = FromConfigFile<T>(ConfigFileName);
                            break;
                        case 2:
                            o = FromXmlSection<T>(ConfigFileName, XPath);
                            break;
                        case 3:
                            o = FromConfigFile<T>(ConfigFileName, IsCheckParent);
                            break;
                        default:
                            o = FromAppConfig<T>(ConfigName);
                            break;
                    }


                    if (o == null)
                        o = new T();

                    Dict.Add(ConfigName, o);

                }
                else
                    o = Dict[ConfigName] as T;
            }

            return o;
        }

        #endregion

        #region �Ӳ�ͬ��Դ�������ö���

        /// <summary>
        /// ��.NET��׼�����ļ��н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigName"></param>
        /// <returns></returns>
        private static T FromAppConfig<T>(string ConfigName) where T : class, new()
        {
            XmlNode node = ConfigurationManager.GetSection(ConfigName) as XmlNode;

            
            if (node != null)
            {
                using (XmlNodeReader reader = new XmlNodeReader(node))
                {
                    XmlSerializer xs = XmlTools.GetXmlSerializer(typeof(T));

                    return xs.Deserialize(reader) as T;
                }
            }

            return null;
        }

        /// <summary>
        /// ��һ��Xml�н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigFileName"></param>
        /// <returns></returns>
        private static T FromConfigFile<T>(string ConfigFileName) where T : class, new()
        {
            if (File.Exists(ConfigFileName))
            {
                using (XmlReader reader = XmlReader.Create(ConfigFileName))
                {
                    XmlSerializer xs = XmlTools.GetXmlSerializer(typeof(T));
                    return xs.Deserialize(reader) as T;

                }
            }

            return null;
        }

        private static T FromConfigFile<T>(string ConfigFileNameWithoutPath, bool IsCheckParent) where T : class, new()
        {
            //�����ļ�
            FileInfo file = FileTools.FindFile(ConfigFileNameWithoutPath);

            if (file == null || file.Exists == false)
            {
                if (IsCheckParent == true && FileTools.CurrentDirectory.Parent.Exists)//û���ļ�, �и�Ŀ¼, Ĭ�Ϸŵ���Ŀ¼��
                    file = new FileInfo(Path.Combine(FileTools.CurrentDirectory.Parent.FullName, ConfigFileNameWithoutPath));
                else//û���ļ�, û�и�Ŀ¼, Ĭ�Ϸŵ���ǰĿ¼��
                    file = new FileInfo(Path.Combine(FileTools.CurrentDirectory.FullName, ConfigFileNameWithoutPath));
            }

            if(file.Exists)
                return FromConfigFile<T>(file.FullName);

            return null;
        }

        /// <summary>
        /// ��һ��Xml��Section�н���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigFileName"></param>
        /// <param name="XPath"></param>
        /// <returns></returns>
        private static T FromXmlSection<T>(string ConfigFileName, string XPath) where T : class, new()
        {
            if (File.Exists(ConfigFileName))
            {
                XmlDocument xdoc = new XmlDocument();

                xdoc.Load(ConfigFileName);

                XmlNode node = xdoc.SelectSingleNode(XPath);

                if (node != null)
                {
                    using (XmlNodeReader reader = new XmlNodeReader(node))
                    {
                        XmlSerializer xs = XmlTools.GetXmlSerializer(typeof(T));

                        return xs.Deserialize(reader) as T;
                    }
                }
            }

            return null;
        }

        #endregion

        #region ����������Ϣ

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FileNameWithoutPath"></param>
        /// <param name="IsCheckParent"></param>
        /// <param name="config"></param>
        public static void SaveConfig<T>(string FileNameWithoutPath, bool IsCheckParent, T config)
        {
            //�����ļ�
            FileInfo file = FileTools.FindFile(FileNameWithoutPath);

            if (file == null || file.Exists == false)
            {
                if (IsCheckParent == true && FileTools.CurrentDirectory.Parent.Exists)//û���ļ�, �и�Ŀ¼, Ĭ�Ϸŵ���Ŀ¼��
                    file = new FileInfo(Path.Combine(FileTools.CurrentDirectory.Parent.FullName, FileNameWithoutPath));
                else//û���ļ�, û�и�Ŀ¼, Ĭ�Ϸŵ���ǰĿ¼��
                    file = new FileInfo(Path.Combine(FileTools.CurrentDirectory.FullName, FileNameWithoutPath));
            }

            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                XmlSerializerNamespaces xsns = new XmlSerializerNamespaces();
                xsns.Add("", "");

                xs.Serialize(sw, config, xsns);
            }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="FileNameWithoutPath"></param>
        /// <param name="IsCheckParent"></param>
        /// <param name="config"></param>
        internal static void SaveConfig(string FileNameWithoutPath, bool IsCheckParent, object config)
        {
            //�����ļ�
            FileInfo file = FileTools.FindFile(FileNameWithoutPath);

            if (file == null || file.Exists == false)
            {
                if (IsCheckParent == true && FileTools.CurrentDirectory.Parent.Exists)//û���ļ�, �и�Ŀ¼, Ĭ�Ϸŵ���Ŀ¼��
                    file = new FileInfo(Path.Combine(FileTools.CurrentDirectory.Parent.FullName, FileNameWithoutPath));
                else//û���ļ�, û�и�Ŀ¼, Ĭ�Ϸŵ���ǰĿ¼��
                    file = new FileInfo(Path.Combine(FileTools.CurrentDirectory.FullName, FileNameWithoutPath));
            }


            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                XmlSerializer xs = new XmlSerializer(config.GetType());

                XmlSerializerNamespaces xsns = new XmlSerializerNamespaces();
                xsns.Add("", "");

                xs.Serialize(sw, config, xsns);
            }
        }

        #endregion

        #region ����Զ�̿��������ļ�

        internal static string RemotePassword = string.Empty;

        private static bool HasEnableaRemoteConfig = false;
        /// <summary>
        /// ����Զ�̿��������ļ�
        /// </summary>
        /// <param name="TcpPort"></param>
        /// <param name="Password">Զ�̷�������</param>
        /// <returns></returns>        
        public static void  EnableRemoteConfig(int TcpPort, string Password)
        {

            if (TcpPort <= 0)
                return;
            if (HasEnableaRemoteConfig)
                return;

            if (string.IsNullOrEmpty(Password))
                return;

            RemotePassword = Password;
            try
            {
                System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(
                    new System.Runtime.Remoting.WellKnownServiceTypeEntry(typeof(RemoteConfig), "Z.Config", System.Runtime.Remoting.WellKnownObjectMode.SingleCall));
                TcpChannel channel = new TcpChannel(TcpPort);
                ChannelServices.RegisterChannel(channel, false); 
                HasEnableaRemoteConfig = true;
            }
            catch (Exception ex)
            {
                HasEnableaRemoteConfig = false;
                logger.Error(ex);  
            }     
        }
        #endregion
    }
}
