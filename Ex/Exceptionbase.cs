using System;
using System.Collections.Generic;
using System.Text;

namespace Z.Ex
{
    /// <summary>
    /// �쳣����
    /// </summary>
    public abstract class Exceptionbase : Exception
    {
        /// <summary>
        /// �Ƿ��ʽ����ջ��Ϣ
        /// </summary>
        internal protected bool IsFormatStack = true;

        /// <summary>
        /// ���캯��
        /// </summary>
        public Exceptionbase()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="msg"></param>
        public Exceptionbase(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public Exceptionbase(string msg, Exception ex)
            : base(msg, ex)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sbXml"></param>
        public virtual void ToXmlElements(StringBuilder sbXml)
        {
        }
    }
}
