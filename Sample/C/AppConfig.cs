using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Sample.C
{
    public class AppConfig
    {
        #region ��ȡ������Ϣ

        public static AppConfig Instance
        {
            get
            {
                return Z.C.AppConfigHandler.GetConfig<AppConfig>("AppConfig.xml", true);
            }
        }

        #endregion

        [XmlAttribute]
        public string MQAddress = "12345";

        [XmlAttribute]
        public bool IsLog = false;
    }
}
