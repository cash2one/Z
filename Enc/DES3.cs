using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Z.Enc
{
    /// <summary>
    /// DES3���� 
    /// </summary>
    public class DES3
    {
        static DES3()
        {
        }


        #region �ַ������ܺ���

        /// <summary>
        /// �ַ������ܺ���
        /// </summary>
        /// <param name="p_strContent">��������</param>
        /// <param name="p_strKey">32λ��Կ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string EncryptString(string p_strContent, string p_strKey)
        {
            //if ((p_strContent == null) || (p_strContent.Length == 0))
            //{
            //    throw new CryptographicException("Invalid Argument: Content");
            //}
            if ((p_strKey == null) || (p_strKey.Length != 32 && p_strKey.Length != 48))
            {
                throw new CryptographicException("Invalid Argument: Key");
            }
            try
            {
                byte[] Results = null;

                //����һ���Գ��㷨
                using (SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider())
                {
                    mCSP.Key = Convert.FromBase64String(p_strKey);
                    //ָ�����ܵ�����ģʽ
                    mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                    //��ȡ�����ü����㷨�����ģʽ
                    mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                    using (ICryptoTransform ct = mCSP.CreateEncryptor(mCSP.Key, null))
                    {
                        byte[] byt = Encoding.UTF8.GetBytes(p_strContent);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                            {
                                cs.Write(byt, 0, byt.Length);
                                cs.FlushFinalBlock();

                                Results = ms.ToArray();
                                cs.Close();
                            }
                        }
                    }
                }

                if (Results != null && Results.Length > 0)
                    return Convert.ToBase64String(Results);

                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region �ַ������ܺ���

        /// <summary>
        /// �ַ������ܺ���
        /// </summary>
        /// <param name="p_strContent">��Ҫ��������</param>
        /// <param name="p_strKey">32λ��Կ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DecryptString(string p_strContent, string p_strKey)
        {
            if ((p_strContent == null) || (p_strContent.Length == 0))
            {
                throw new CryptographicException("Invalid Argument: Content");
            }
            if ((p_strKey == null) || (p_strKey.Length != 32 && p_strKey.Length != 48))
            {
                throw new CryptographicException("Invalid Argument: Key");
            }
            try
            {
                
                byte[] Results;

                using (SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider())
                {
                    mCSP.Key = Convert.FromBase64String(p_strKey);
                    mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                    mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                    
                    using (ICryptoTransform ct = mCSP.CreateDecryptor(mCSP.Key, null))
                    {
                        byte[] byt = Convert.FromBase64String(p_strContent);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                            {
                                cs.Write(byt, 0, byt.Length);
                                cs.FlushFinalBlock();
                                cs.Close();
                                Results = ms.ToArray();
                            }
                            ms.Close();
                        }
                    }
                }

                if(Results != null && Results.Length > 0)
                    return Encoding.UTF8.GetString(Results);

                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
