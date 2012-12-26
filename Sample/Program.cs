using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Transactions;
using Z.DA;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using Z.Ex;
using Sample.C;
using System.Runtime.Remoting;
using Z.Util;
using Z.C;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(AppConfig.Instance.IsLog);
                AppConfigHandler.GetConfig<AppConfig>("AppConfig.xml");
                AppConfigHandler.EnableRemoteConfig(10008, "56yhgfrt");
                
                //AppConfigHandler.SaveConfig<AppConfig>("AppConfig.xml", true, AppConfig.Instance);


                string s = string.Empty;

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine("���ʹ�� Z.DA �������ݿ����");
                        Console.WriteLine("1. �������ݿ����(��AbstractDA����)");
                        Console.WriteLine("2. �������ݿ����(���������)");
                        Console.WriteLine("3. �������ݿ����(ִ�д洢����)");
                        Console.WriteLine("4. �������ݿ����(ʵ��һ�������ӣ� ����ÿ��ִ���ظ�����������Connection)");
                        Console.WriteLine("5. �������ݿ����(ʵ������)");
                        Console.WriteLine("6. �������ݿ����(��ӡ�ڲ�����SQL����)");
                        Console.WriteLine("7. �������ݿ����(���ط����б�Ĵ洢����)");
                        Console.WriteLine("8. �������ݿ����(���ش洢����, �������κ�ֵ)");
                        Console.WriteLine("9. �������ݿ����(����һ���ַ�������)");
                        Console.WriteLine("0, q, x. �˳�");
                        Console.WriteLine("c. ����");
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("ѡ������:");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        s = Console.ReadLine().ToLower();

                        Console.ForegroundColor = ConsoleColor.Green;

                        switch (s)
                        {
                            case "1":
                                HowTO_1();
                                break;
                            case "2":
                                HowTO_2();
                                break;
                            case "3":
                                HowTO_3();
                                break;
                            case "4":
                                HowTO_4();
                                break;
                            case "5":
                                HowTO_5();
                                break;
                            case "6":
                                HowTO_6();
                                break;
                            case "7":
                                HowTO_7();
                                break;
                            case "8":
                                HowTO_8();
                                break;
                            case "9":
                                HowTO_9();
                                break;
                            case "0":
                            case "q":
                            case "x":
                                Console.ForegroundColor = ConsoleColor.Gray;
                                return;
                            case "c":
                                Console.Clear();
                                break;
                            default:
                                Console.Clear();
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        #region HowTO: 1. �������ݿ����(��AbstractDA����)

        /// <summary>
        /// HowTO: 1. �������ݿ����(��AbstractDA����)
        /// </summary>
        static void HowTO_1()
        {
            Console.WriteLine("HowTO: 1. ��AbstractDA���������ݿ����");
            TestAbstractDA da = new TestAbstractDA();

            da.DeleteAll();

            int UserID = da.Insert("����1", "����1");

            TestUser u = da.SelectUserByUserId(UserID);

            Console.WriteLine("����T_TEST:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);

            da.Update(u.ID, "�޸�֮����û���", "�޸�֮����û���");

            u = da.SelectUserByUserId(UserID);

            Console.WriteLine("����T_TEST:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);
        }

        #endregion

        #region HowTO: 2. �������ݿ����(���������)

        /// <summary>
        /// HowTO: 2. �������ݿ����(���������)
        /// </summary>
        static void HowTO_2()
        {
            Console.WriteLine("HowTO: 2. ֱ��ʹ��AbstractTable���б����");

            TestAbstractTable da = new TestAbstractTable();

            //��ձ�
            da.DeleteAll();

            //�����
            int UserID = da.Insert("���������", "���������");

            //��ѯ���
            TestUser u = da.SelectUserByUserId(UserID);

            Console.WriteLine("Insert(values):  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);

            u.UserName = "Update<T>�޸�ʾ��";
            u.UserPass = "Update<T>�޸�ʾ��";


            //Update<T>ʾ��, ֱ��ͨ��������и���
            da.Update<TestUser>(u);

            //��ѯDataSet
            DataSet ds = da.SelectDataSetByUserId(UserID);

            //Update<T>ʾ��, ֱ��ͨ��������и���
            if (ds.Tables[0].Rows.Count > 0)
            {
                da.Update<DataRow>(ds.Tables[0].Rows[0]);
            }


            //��ѯ���
            u = da.SelectUserByUserId(UserID);

            Console.WriteLine("Update<T>���:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);

            //Update(values)
            da.Update(u.ID, "�������޸�ʾ��", "�������޸�ʾ��");

            u = da.SelectUserByUserId(UserID);
            Console.WriteLine("Update(values)���:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);

            //Delete(����)
            da.Delete(u.ID);

            u.UserName = "���Ͳ���";
            u.UserPass = "���Ͳ���";

            //Insert<T>
            UserID = da.Insert<TestUser>(u);

            //��ѯ
            u = da.SelectUserByUserId(UserID);
            Console.WriteLine("Inser<T>���:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);

        }

        #endregion

        #region HowTO: 3. �������ݿ����(ִ�д洢����)

        /// <summary>
        /// HowTO: 3. �������ݿ����(ִ�д洢����)
        /// </summary>
        static void HowTO_3()
        {
            Console.WriteLine("HowTO: 3. ���ô洢����, ����DataSet");

            TestAbstractDA da = new TestAbstractDA();

            StoreProcedureInfo info = da.TestStoreProcedure("p_test", 2,DateTime.Now);

            Console.WriteLine("�洢����p_test�ķ���ֵ:{0}", info.ReturnCode);

            foreach (string s in info.OutputParameters.Keys)
            {
                Console.WriteLine("\t�������:{0}:{1}", s, info.OutputParameters[s]);
            }

            if (info.DataSet != null && info.DataSet.Tables.Count > 0)
            {
                Console.WriteLine("\tDataSet:");
                foreach (DataColumn c in info.DataSet.Tables[0].Columns)
                {
                    Console.Write("\t{0}", c.ColumnName);
                }
                Console.WriteLine();

                foreach (DataRow r in info.DataSet.Tables[0].Rows)
                {
                    foreach (DataColumn c in info.DataSet.Tables[0].Columns)
                    {
                        Console.Write("\t{0}", r[c]);
                    }
                    Console.WriteLine();
                }
            }
        }

        #endregion

        #region HowTO: 4. �������ݿ����(ʵ��һ�������ӣ� ����ÿ��ִ���ظ�����������Connection)

        /// <summary>
        /// HowTO: 4. �������ݿ����(ʵ��һ�������ӣ� ����ÿ��ִ���ظ�����������Connection)
        /// </summary>
        static void HowTO_4()
        {
            Console.WriteLine("HowTO: 4. �����ӣ�����ÿ��ִ���ظ�����������Connection");

            //�����ӱ��뾡��ȷ������
            using (TestAbstractConnection da = new TestAbstractConnection())
            {
                da.DeleteAll();

                int UserID = da.Insert("����1", "����1");

                TestUser u = da.SelectUserByUserId(UserID);

                Console.WriteLine("����T_TEST:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);

                da.Update(u.ID, "�޸�֮����û���", "�޸�֮����û���");

                u = da.SelectUserByUserId(UserID);

                Console.WriteLine("����T_TEST:  ID={0},UserName={1}, UserPass={2}", u.ID, u.UserName, u.UserPass);                
            }
        }

        #endregion

        #region HowTO: 5. Transaction,  ֧��������ͬ���ݿ�����֮�������

        /// <summary>
        /// HowTO: 5. Transaction,  ֧��������ͬ���ݿ�����֮�������
        /// </summary>
        static void HowTO_5()
        {
            Console.WriteLine("HowTO: 5. Transaction,  ֧��������ͬ���ݿ�����֮�������");

            TestAbstractDA da1 = new TestAbstractDA();
            TestAbstractTable da2 = new TestAbstractTable();

            try
            {

                using (TransScope scope = new TransScope(da1, da2))
                {
                    da1.DeleteAll();
                    da2.DeleteAll();

                    int id1 = da1.Insert("1", "1");
                    int id2 = da2.Insert("2", "2");
                    da1.Insert("3", "3");
                    da2.Insert("4", "4");

                    Console.WriteLine("da1:");
                    foreach (TestUser u in da1.SelectAll())
                    {
                        Console.WriteLine("\tID={0},UserName={1},UserPass={2}", u.ID, u.UserName, u.UserPass);
                    }

                    Console.WriteLine("da2:");
                    foreach (TestUser u in da2.SelectAll())
                    {
                        Console.WriteLine("\tID={0},UserName={1},UserPass={2}", u.ID, u.UserName, u.UserPass);
                    }

                    da1.Update(id1, "�޸�", "�޸�");
                    da2.Update(id2, "�޸�", "�޸�");

                    Console.WriteLine("da1�޸ĺ�:");
                    foreach (TestUser u in da1.SelectAll())
                    {
                        Console.WriteLine("\tID={0},UserName={1},UserPass={2}", u.ID, u.UserName, u.UserPass);
                    }

                    Console.WriteLine("da1�޸ĺ�:");
                    foreach (TestUser u in da2.SelectAll())
                    {
                        Console.WriteLine("\tID={0},UserName={1},UserPass={2}", u.ID, u.UserName, u.UserPass);
                    }


                    scope.Commit();
                }
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("SQL:" + ex.SQL);
            }
        }

        #endregion

        #region HowTO: 6. �������ݿ����(��ӡ�ڲ�����SQL����)

        /// <summary>
        /// 6. �������ݿ����(��ӡ�ڲ�����SQL����)
        /// </summary>
        static void HowTO_6()
        {
            Console.WriteLine("HowTO: 6. �������ݿ����(��ӡ�ڲ�����SQL����)");

            string s = DatabaseHelper.ShowCache();

            Console.WriteLine(s);
        }

        #endregion

        #region HowTO: 7. �������ݿ����(ִ�д洢���̷��ط����б�)

        /// <summary>
        /// HowTO: 7. �������ݿ����(ִ�д洢���̷��ط����б�)
        /// </summary>
        static void HowTO_7()
        {
            Console.WriteLine("HowTO: 7. ִ�д洢���̷��ط����б�");

            TestAbstractDA da = new TestAbstractDA();

            StoreProcedureList<TestUser> info = da.TestStoreProcedureList("p_test", 2);

            Console.WriteLine("�洢����p_test�ķ���ֵ:{0}", info.ReturnCode);

            foreach (string s in info.OutputParameters.Keys)
            {
                Console.WriteLine("\t�������:{0}:{1}", s, info.OutputParameters[s]);
            }

            foreach (TestUser u in info.List)
            {
                Console.WriteLine("\tID={0}, UserName={1}, UserPass={2}",
                    u.ID, u.UserName, u.UserPass);
            }
        }

        #endregion

        #region HowTO: 8. �������ݿ����(ִ�д洢���̲������κ�ֵ)

        /// <summary>
        /// HowTO: 8. �������ݿ����(ִ�д洢���̲������κ�ֵ)
        /// </summary>
        static void HowTO_8()
        {
            Console.WriteLine("HowTO: 8. ִ�д洢���̲������κ�ֵ");

            using (TestAbstractConnection da = new TestAbstractConnection())
            {
                da.TestWarning();
            }
        }

        #endregion

        #region HowTO: 9. �������ݿ����(����һ���ַ����б�)

        /// <summary>
        /// HowTO: 9. �������ݿ����(����һ���ַ����б�)
        /// </summary>
        static void HowTO_9()
        {
            Console.WriteLine("HowTO: 9. ����һ���ַ����б�");

            using (TestAbstractDA da = new TestAbstractDA())
            {
                List<string> list = da.TestListString();

                list.ForEach(delegate(string s) { Console.WriteLine(s); });


            }

        }

        #endregion


    }
}
