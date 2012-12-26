using System;
using System.Diagnostics;
using System.Text;
using log4net;
using log4net.Config;
using Z.Ex;
using Z.Util;

namespace Z.Log
{
    /// <summary>
    /// ��־��
    /// </summary>
    public class Logger
    {
        private const string NULL_STRING = "<NULL>";
        private ILog logger;

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static Logger()
        {
            try
            {
                XmlConfigurator.ConfigureAndWatch(FileTools.FindFile("log4net.config"));
            }
            catch (Exception)
            {
                System.Diagnostics.Trace.TraceError("load log4net configuration file failed.");
            }
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">��־����</param>
        public Logger(string name)
        {
            logger = LogManager.GetLogger(name);
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="type">��־����</param>
        public Logger(Type type)
        {
            logger = LogManager.GetLogger(type);
        }

        #region IsEnabled
        /// <summary>
        /// �Ƿ��¼Debug��־
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return logger.IsDebugEnabled; }
        }
        /// <summary>
        /// �Ƿ��¼Info��־
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return logger.IsInfoEnabled; }
        }
        /// <summary>
        /// �Ƿ��¼Warn��־
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return logger.IsWarnEnabled; }
        }
        /// <summary>
        /// �Ƿ��¼Error��־
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return logger.IsErrorEnabled; }
        }

        #endregion

        #region Debug
        /// <summary>
        /// д��Debug��־
        /// </summary>
        /// <param name="message">��־</param>
        public void Debug(string message)
        {
            logger.Debug(message);
            Trace.TraceInformation("Debug:" + message);
        }
        /// <summary>
        /// д��Debug��־
        /// </summary>
        /// <param name="info">��־ʵ��</param>
        public void Debug(LogInfo info)
        {
            logger.Debug(info.ToString());
            Trace.TraceInformation("Debug:" + info.ToString());
        }
        /// <summary>
        /// д��Debug��־
        /// </summary>
        /// <param name="parameters">��־����</param>
        public void Debug(params object[] parameters)
        {
            if (logger.IsDebugEnabled)
            {
                StringBuilder sb = new StringBuilder();
                foreach (object obj in parameters)
                {
                    if (obj != null)
                    {
                        sb.Append(obj.ToString());
                        sb.Append("    ");
                    }
                }
                logger.Debug(sb.ToString());
            }
        }
        #endregion

        #region Info
        /// <summary>
        /// д��Info��־
        /// </summary>
        /// <param name="message">��־</param>
        public void Info(string message)
        {
            logger.Info(message);
            Trace.TraceInformation("Info:" + message);
        }
        /// <summary>
        /// д��Info��־
        /// </summary>
        /// <param name="info">��־ʵ��</param>
        public void Info(LogInfo info)
        {
            logger.Info(info.ToString());
        }
        /// <summary>
        /// д��Info��־
        /// </summary>
        /// <param name="parameters">��־��������</param>
        public void Info(params object[] parameters)
        {
            if (logger.IsInfoEnabled)
            {
                StringBuilder sb = new StringBuilder();
                foreach (object obj in parameters)
                {
                    if (obj != null)
                    {
                        sb.Append(obj.ToString());
                        sb.Append("    ");
                    }
                }
                logger.Info(sb.ToString());
            }
        }
        #endregion

        #region Warning
        /// <summary>
        /// д��Warn��־
        /// </summary>
        /// <param name="message">��־</param>
        public void Warn(string message)
        {
            logger.Warn(message);
            Trace.TraceWarning("Warn:" + message);
        }
        /// <summary>
        /// д��Warn��־
        /// </summary>        
        public void Warn(LogInfo info)
        {
            logger.Warn(info.ToString());
        }
        /// <summary>
        /// д��Warn��־
        /// </summary>
        /// <param name="parameters">��־��������</param>
        public void Warn(params object[] parameters)
        {
            if (logger.IsWarnEnabled)
            {
                StringBuilder sb = new StringBuilder();
                foreach (object obj in parameters)
                {
                    if (obj != null)
                    {
                        sb.Append(obj.ToString());
                        sb.Append("    ");
                    }
                }
                logger.Warn(sb.ToString());
            }
        }
        #endregion

        #region Error
        /// <summary>
        /// д�������־
        /// </summary>
        /// <param name="message">��־</param>
        public void Error(string message)
        {
            logger.Error(message);
            Trace.TraceError("Error:" + message);
        }
        /// <summary>
        /// д�������־
        /// </summary>
        /// <param name="info">��־ʵ��</param>
        public void Error(LogInfo info)
        {
            logger.Error(info.ToString());
        }

        /// <summary>
        /// д�������־
        /// </summary>
        /// <param name="ex">�쳣</param>
        public void Error(Exception ex)
        {
            logger.Error(ExceptionFormatter.FormatException(ex));
        }

        /// <summary>
        /// д�������־
        /// </summary>
        /// <param name="ex">�쳣</param>
        /// <param name="message">������־��Ϣ</param>
        public void Error(Exception ex, string message)
        {
            logger.Error(message + Environment.NewLine + ExceptionFormatter.FormatException(ex));
        }

        /// <summary>
        /// д�������־
        /// </summary>
        /// <param name="ex">�쳣</param>
        /// <param name="parameters">��־��������</param>
        public void Error(Exception ex, params object[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object obj in parameters)
            {
                if (obj != null)
                {
                    sb.Append(obj.ToString());
                    sb.Append("    ");
                }
                else
                {
                    sb.Append(NULL_STRING);
                    sb.Append("    ");
                }
            }
            logger.Error(sb.ToString() + Environment.NewLine + ExceptionFormatter.FormatException(ex));
        }
        #endregion

    }
}
