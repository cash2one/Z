// ���� 2008-03-31
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace Z.Enc
{
    /// <summary>
    /// ��̬������, MD5���ɹ���
    /// </summary>
    public static class MD5
    {
        #region ��ʼ��

        private static MD5CryptoServiceProvider provider
        {
            get
            {
                return new MD5CryptoServiceProvider();
            }
        }

        #endregion

        #region ��ʼ������

        /// <summary>
        /// ��byte��������MD5, ���ؾ��������String
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToString(byte[] data)
        {
            return BitConverter.ToString(
                provider.ComputeHash(
                    data)).Replace("-", "");
        }

        /// <summary>
        /// ��UTF8��String����MD5, ���ؾ��������String
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToString(string data)
        {
            return BitConverter.ToString(
                provider.ComputeHash(
                    Encoding.UTF8.GetBytes(
                        data))).Replace("-", "");
        }

        /// <summary>
        /// ��ָ�������String����MD5, ���ؾ��������String
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToString(string data, System.Text.Encoding encoding)
        {
            return BitConverter.ToString(
                provider.ComputeHash(
                    encoding.GetBytes(
                        data))).Replace("-", "");
        }

        /// <summary>
        /// ��UTF8��String����MD5, ����ԭʼ��MD5�ֽ�����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytes(string data)
        {
            return provider.ComputeHash(
                    Encoding.UTF8.GetBytes(
                        data));
        }

        /// <summary>
        /// ��ָ�������String����MD5, ����ԭʼ��MD5�ֽ�����
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(string data, System.Text.Encoding encoding)
        {
            return provider.ComputeHash(
                    encoding.GetBytes(
                        data));
        }

        /// <summary>
        /// ��byte��������MD5, ����ԭʼ��MD5�ֽ�����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytes(byte[] data)
        {
            return provider.ComputeHash(data);
        }

        /// <summary>
        /// ��byte����ת��ΪĬ�ϱ�����ַ���
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Format(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }


        /// <summary>
        /// ���ļ�������MD5, ���ؾ��������String
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string ToString(Stream inputStream)
        {
            return BitConverter.ToString(
                provider.ComputeHash(inputStream)).Replace("-", "");
        }
        #endregion

        #region ���ܲ���

        /// <summary>
        /// ���ܲ���
        /// </summary>
        /// <param name="FileName"></param>
        public static void Test(string FileName)
        {
            if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
            {
                Console.WriteLine("������Ҫָ��һ���ļ�");
                return;
            }

            #region ��ʼ��

            byte[] bytes = File.ReadAllBytes(FileName);

            string x = System.Text.Encoding.Default.GetString(bytes);

            int Times = 100;

            Stopwatch sw = new Stopwatch();

            #endregion

            #region string to string

            sw.Reset();

            sw.Start();

            string s2 = MD5.ToString(x);

            sw.Stop();

            long i3 = sw.ElapsedTicks;
            Console.WriteLine("3: {0}", i3);

            #endregion

            #region byte to string

            sw.Reset();

            sw.Start();

            string s = MD5.ToString(bytes);

            sw.Stop();

            long i2 = sw.ElapsedTicks;
            Console.WriteLine("2: {0}", i2);

            #endregion

            #region byte to byte

            sw.Reset();

            sw.Start();

            byte[] bs = MD5.ToBytes(bytes);

            sw.Stop();

            long i1 = sw.ElapsedTicks;
            Console.WriteLine("1: {0}", i1);

            #endregion

            #region ���ܲ��ԣ� ��Լ��Ҫ��ʱ2����

            long l1 = 0;

            for (int i = 0; i < Times; i++)
            {
                sw.Reset();
                sw.Start();
                MD5.ToBytes(bytes);
                sw.Stop();
                l1 = l1 + sw.ElapsedMilliseconds;
            }

            long l2 = 0;

            for (int i = 0; i < Times; i++)
            {
                sw.Reset();
                sw.Start();
                MD5.ToBytes(x);
                sw.Stop();
                l2 = l2 + sw.ElapsedMilliseconds;
            }

            long l3 = 0;

            for (int i = 0; i < Times; i++)
            {
                sw.Reset();
                sw.Start();
                MD5.ToString(x);
                sw.Stop();
                l3 = l3 + sw.ElapsedMilliseconds;
            }

            #endregion

            Console.WriteLine("MD5 byte to byte: {0}", l1 / Times);
            Console.WriteLine("MD5 string to byte: {0}", l2 / Times);
            Console.WriteLine("MD5 string to string: {0}", l3 / Times);
        }

        #endregion
    }
}
