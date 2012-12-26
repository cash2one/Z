using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Z.Ex;

namespace Z.Enc
{
    /// <summary>
    /// �򵥼���
    /// </summary>
    public static class SimpleEnc
    {
        #region EncryptString

        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strIn">��Ҫ���ܵ��ַ���</param>
        /// <param name="keyBytes">�ֵ�1</param>
        /// <param name="iMod">�ֵ�2</param>
        /// <param name="iChar">�ֵ�3</param>
        /// <returns></returns>
        public static string EncryptString(string strIn, byte[] keyBytes, int[,] iMod, char[,] iChar)
        {
            try
            {
                byte[] srcBytes = Encoding.UTF8.GetBytes(strIn);
                byte[] dstBytes = new byte[srcBytes.Length + 1];

                int KeyIndex = srcBytes[0];
                for (int i = 0; i < srcBytes.Length; i++)
                {
                    dstBytes[i] = (byte)(srcBytes[i] ^ keyBytes[KeyIndex]);
                    KeyIndex = ((KeyIndex + 1) % keyBytes.Length);
                }

                dstBytes[dstBytes.Length - 1] = srcBytes[0];

                for (int i = 0; i <= iMod.GetUpperBound(0); i++)
                {
                    int x = iMod[i, 0] % dstBytes.Length;
                    int y = iMod[i, 1] % dstBytes.Length;

                    byte t = dstBytes[x];
                    dstBytes[x] = dstBytes[y];
                    dstBytes[y] = t;
                }

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < dstBytes.Length; i++)
                {
                    sb.AppendFormat("{0:X2}", dstBytes[i]);
                }

                for (int i = 0; i <= iChar.GetUpperBound(0); i++)
                {
                    sb.Replace(iChar[i, 0], iChar[i, 1]);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new SimpleEncException("���ܴ���", ex);
            }
        }

        #endregion

        #region DecryptString

        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strOut">��Ҫ���ܵ��ַ���</param>
        /// <param name="keyBytes">�ֵ�1</param>
        /// <param name="iMod">�ֵ�2</param>
        /// <param name="iChar">�ֵ�3</param>
        /// <returns></returns>
        public static string DecryptString(string strOut, byte[] keyBytes, int[,] iMod, char[,] iChar)
        {
            try
            {
                StringBuilder sb = new StringBuilder(strOut);

                for (int i = iChar.GetUpperBound(0); i >= 0; i--)
                {
                    sb.Replace(iChar[i, 1], iChar[i, 0]);
                }

                byte[] srcBytes = new byte[sb.Length / 2];

                for (int i = 0; i < strOut.Length; i += 2)
                {
                    int j = i / 2;
                    srcBytes[j] = byte.Parse(sb.ToString(i, 2), NumberStyles.HexNumber);
                }

                for (int i = iMod.GetUpperBound(0); i >= 0; i--)
                {
                    int x = iMod[i, 0] % srcBytes.Length;
                    int y = iMod[i, 1] % srcBytes.Length;

                    byte t = srcBytes[x];
                    srcBytes[x] = srcBytes[y];
                    srcBytes[y] = t;
                }

                int KeyIndex = srcBytes[srcBytes.Length - 1];

                byte[] dstBytes = new byte[srcBytes.Length - 1];

                for (int i = 0; i < srcBytes.Length - 1; i++)
                {
                    dstBytes[i] = (byte)(srcBytes[i] ^ keyBytes[KeyIndex]);
                    KeyIndex = (KeyIndex + 1) % keyBytes.Length;
                }

                return Encoding.UTF8.GetString(dstBytes);
            }
            catch(Exception ex)
            {
                throw new SimpleEncException("���ܴ��� ", ex);
            }

        }

        #endregion
    }

    /// <summary>
    /// �򵥼����쳣��
    /// </summary>
    public class SimpleEncException : Exceptionbase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        public SimpleEncException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }

}
