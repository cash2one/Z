using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Z.Util
{
    /// <summary>
    /// SMTP���ʼ�������
    /// </summary>
    [Serializable]
	public class SmtpMail
	{
		#region fields
		private string enter="\r\n";

		/// <summary>
		/// �趨���Դ��룬Ĭ���趨ΪGB2312���粻��Ҫ������Ϊ""
		/// </summary>
		private string _charset="GB2312";

		/// <summary>
		/// �����˵�ַ
		/// </summary>
		private string _from="";

		/// <summary>
		/// ����������
		/// </summary>
		private string _fromName="";

		/// <summary>
		/// �ռ�������
		/// </summary> 
		private string _recipientName="";

		/// <summary>
		/// �ռ����б�
		/// </summary>
		private Hashtable Recipient=new Hashtable();

		/// <summary>
		/// �ʼ�����������
		/// </summary> 
		private string mailserver="";

		/// <summary>
		/// �ʼ��������˿ں�
		/// </summary> 
		private int mailserverport=25;

		/// <summary>
		/// SMTP��֤ʱʹ�õ��û���
		/// </summary>
		private string username="";

		/// <summary>
		/// SMTP��֤ʱʹ�õ�����
		/// </summary>
		private string password="";

		/// <summary>
		/// �Ƿ���ҪSMTP��֤
		/// </summary>
		private bool ESmtp=false;

		/// <summary>
		/// �Ƿ�Html�ʼ�
		/// </summary>
		private bool _html=false;


		/// <summary>
		/// �ʼ������б�
		/// </summary>
		private IList Attachments = new ArrayList();

		/// <summary>
		/// �ʼ��������ȼ���������Ϊ"High","Normal","Low"��"1","3","5"
		/// </summary>
		private string priority="Normal";

		/// <summary>
		/// �ʼ�����
		/// </summary>
		private string _subject;

		/// <summary>
		/// �ʼ�����
		/// </summary>
		private string _body;

		/// <summary>
		/// �ռ�������
		/// </summary>
		private int RecipientNum=0;

		/// <summary>
		/// ����ռ�������
		/// </summary>
		private int recipientmaxnum=20;

		/// <summary>
		/// ������Ϣ����
		/// </summary>
		private string errmsg;

		/// <summary>
		/// TcpClient�����������ӷ�����
		/// </summary> 
		private TcpClient tc;

		/// <summary>
		/// NetworkStream����
		/// </summary> 
		private NetworkStream ns;

		/// <summary>
		/// ������������¼
		/// </summary>
		private string logs="";

		/// <summary>
		/// SMTP��������ϣ��
		/// </summary>
		private Hashtable ErrCodeHT = new Hashtable();

		/// <summary>
		/// SMTP��ȷ�����ϣ��
		/// </summary>
		private Hashtable RightCodeHT = new Hashtable();

		#endregion

		#region Properties


		/// <summary>
		/// �ʼ�����
		/// </summary>
		public string Subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				this._subject = value;
			}
		}

		/// <summary>
		/// �ʼ�����
		/// </summary>
		public string Body
		{
			get
			{
				return this._body;
			}
			set
			{
				this._body = value;
			}
		}


		/// <summary>
		/// �����˵�ַ
		/// </summary>
		public string From
		{
			get
			{
				return _from;
			}
			set
			{
				this._from = value;
			}
		}

		/// <summary>
		/// �趨���Դ��룬Ĭ���趨ΪGB2312���粻��Ҫ������Ϊ""
		/// </summary>
		public string Charset
		{
			get
			{
				return this._charset;
			}
			set
			{
				this._charset = value;
			}
		}

		/// <summary>
		/// ����������
		/// </summary>
		public string FromName
		{
			get
			{
				return this._fromName;
			}
			set
			{
				this._fromName = value;
			}
		}

		/// <summary>
		/// �ռ�������
		/// </summary>
		public string RecipientName
		{
			get
			{
				return this._recipientName;
			}
			set
			{
				this._recipientName = value;
			}
		}

		/// <summary>
		/// �ʼ���������������֤��Ϣ
		/// ���磺"user:pass@www.server.com:25"��Ҳ��ʡ�Դ�Ҫ��Ϣ����"user:pass@www.server.com"��"www.server.com"
		/// </summary> 
		public string SmtpServer
		{
			set
			{
				string maidomain=value.Trim();
				int tempint;

				if(maidomain!="")
				{
					tempint=maidomain.IndexOf("@");
					if(tempint!=-1)
					{
						string str=maidomain.Substring(0,tempint);
						SmtpUsername=str.Substring(0,str.IndexOf(":"));
						SmtpPassword=str.Substring(str.IndexOf(":")+1,str.Length-str.IndexOf

							(":")-1);
						maidomain=maidomain.Substring(tempint+1,maidomain.Length-tempint-1);
					}

					tempint=maidomain.IndexOf(":");
					if(tempint!=-1)
					{
						mailserver=maidomain.Substring(0,tempint);
						mailserverport=System.Convert.ToInt32(maidomain.Substring

							(tempint+1,maidomain.Length-tempint-1));
					}
					else
					{
						mailserver=maidomain;

					}


				}

			}
		}



		/// <summary>
		/// �ʼ��������˿ں�
		/// </summary> 
		public int MailDomainPort
		{
			set
			{
				mailserverport=value;
			}
		}



		/// <summary>
		/// SMTP��֤ʱʹ�õ��û���
		/// </summary>
		public string SmtpUsername
		{
			set
			{
				if(value.Trim()!="")
				{
					username=value.Trim();
					ESmtp=true;
				}
				else
				{
					username="";
					ESmtp=false;
				}
			}
		}

		/// <summary>
		/// SMTP��֤ʱʹ�õ�����
		/// </summary>
		public string SmtpPassword
		{
			set
			{
				password=value;
			}
		} 

		/// <summary>
		/// �ʼ��������ȼ���������Ϊ"High","Normal","Low"��"1","3","5"
		/// </summary>
		public string Priority
		{
			set
			{
				switch(value.ToLower())
				{
					case "high":
						priority="High";
						break;

					case "1":
						priority="High";
						break;

					case "normal":
						priority="Normal";
						break;

					case "3":
						priority="Normal";
						break;

					case "low":
						priority="Low";
						break;

					case "5":
						priority="Low";
						break;

					default:
						priority="Normal";
						break;
				}
			}
		}

		/// <summary>
		///�Ƿ�Html�ʼ�
		/// </summary>
		public bool Html
		{
			get
			{
				return this._html;
			}
			set
			{
				this._html = value;
			}
		}


		/// <summary>
		/// ������Ϣ����
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				return errmsg;
			}
		}

		/// <summary>
		/// ������������¼���緢�ֱ��������ʹ�õ�SMTP���������뽫����ʱ��Logs�����ң�lion-a@sohu.com�����ҽ��������ԭ��
		/// </summary>
		public string Logs
		{
			get
			{
				return logs;
			}
		}

		/// <summary>
		/// ����ռ�������
		/// </summary>
		public int RecipientMaxNum
		{
			set
			{
				recipientmaxnum = value;
			}
		}


		#endregion

		#region Methods


		/// <summary>
		/// ����ʼ�����
		/// </summary>
		/// <param name="FilePath">��������·��</param>
		public void AddAttachment(params string[] FilePath)
		{
			if(FilePath==null)
			{
				throw(new ArgumentNullException("FilePath"));
			}
			for(int i=0;i<FilePath.Length;i++)
			{
				Attachments.Add(FilePath[i]);
			}
		}

		/// <summary>
		/// ���һ���ռ��ˣ�������recipientmaxnum����������Ϊ�ַ�������
		/// </summary>
		/// <param name="Recipients">�������ռ��˵�ַ���ַ������飨������recipientmaxnum����</param> 
		public bool AddRecipient(params string[] Recipients)
		{
			if(Recipient==null)
			{
				Dispose();
				throw(new ArgumentNullException("Recipients"));
			}
			for(int i=0;i<Recipients.Length;i++)
			{
				string recipient = Recipients[i].Trim();
				if(recipient==String.Empty)
				{
					Dispose();
					throw(new ArgumentNullException("Recipients["+ i +"]"));
				}
				if(recipient.IndexOf("@")==-1)
				{
					Dispose();
					throw(new ArgumentException("Recipients.IndexOf(\"@\")==-1","Recipients"));
				}
				if(!AddRecipient(recipient))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// �����ʼ����������в�����ͨ���������á�
		/// </summary>
		public bool Send()
		{
			if(mailserver.Trim()=="")
			{
                throw (new ArgumentNullException("Recipient", "����ָ��SMTP������"));
			}

			return SendEmail();

		}


		/// <summary>
		/// �����ʼ�����
		/// </summary>
		/// <param name="smtpserver">smtp��������Ϣ����"username:password@www.smtpserver.com:25"��Ҳ��ȥ�����ִ�Ҫ��Ϣ����"www.smtpserver.com"</param>
		public bool Send(string smtpserver)
		{ 
			SmtpServer = smtpserver;
			return Send();
		}


		/// <summary>
		/// �����ʼ�����
		/// </summary>
		/// <param name="smtpserver">smtp��������Ϣ����"username:password@www.smtpserver.com:25"��Ҳ��ȥ�����ִ�Ҫ��Ϣ����"www.smtpserver.com"</param>
		/// <param name="from">������mail��ַ</param>
		/// <param name="fromname">����������</param>
		/// <param name="to">�ռ��˵�ַ</param>
		/// <param name="toname">�ռ�������</param>
		/// <param name="html">�Ƿ�HTML�ʼ�</param>
		/// <param name="subject">�ʼ�����</param>
		/// <param name="body">�ʼ�����</param>
		public bool Send(string smtpserver,string from,string fromname,string to,string toname,bool html,string 

			subject,string body)
		{
			SmtpServer = smtpserver;
			From=from;
			FromName=fromname;
			AddRecipient(to);
			RecipientName=toname;
			Html=html;
			Subject=subject;
			Body=body;
			return Send();
		}


		#endregion

		#region Private Helper Functions

		/// <summary>
		/// ���һ���ռ���
		/// </summary> 
		/// <param name="Recipients">�ռ��˵�ַ</param>
		private bool AddRecipient(string Recipients)
		{
			if(RecipientNum<recipientmaxnum)
			{
				Recipient.Add(RecipientNum,Recipients);
				RecipientNum++;
				return true;
			}
			else
			{
				Dispose();
                throw (new ArgumentOutOfRangeException("Recipients", "�ռ��˹��಻�ɶ���" + recipientmaxnum + " ��"));
			}
		}

		void Dispose()
		{
			if(ns!=null)ns.Close();
			if(tc!=null)tc.Close();
		}

		/// <summary>
		/// SMTP��Ӧ�����ϣ��
		/// </summary>
		private void SMTPCodeAdd()
		{
            ErrCodeHT.Add("500", "�����ַ����");
            ErrCodeHT.Add("501", "������ʽ����");
            ErrCodeHT.Add("502", "�����ʵ��");
            ErrCodeHT.Add("503", "��������ҪSMTP��֤");
            ErrCodeHT.Add("504", "�����������ʵ��");
            ErrCodeHT.Add("421", "����δ�������رմ����ŵ�");
            ErrCodeHT.Add("450", "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����æ��");
            ErrCodeHT.Add("550", "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����δ�ҵ����򲻿ɷ��ʣ�");
            ErrCodeHT.Add("451", "����Ҫ��Ĳ�������������г���");
            ErrCodeHT.Add("551", "�û��Ǳ��أ��볢��<forward-path>");
            ErrCodeHT.Add("452", "ϵͳ�洢���㣬Ҫ��Ĳ���δִ��");
            ErrCodeHT.Add("552", "�����Ĵ洢���䣬Ҫ��Ĳ���δִ��");
            ErrCodeHT.Add("553", "�����������ã�Ҫ��Ĳ���δִ�У����������ʽ����");
            ErrCodeHT.Add("432", "��Ҫһ������ת��");
            ErrCodeHT.Add("534", "��֤���ƹ��ڼ�");
            ErrCodeHT.Add("538", "��ǰ�������֤������Ҫ����");
            ErrCodeHT.Add("454", "��ʱ��֤ʧ��");
            ErrCodeHT.Add("530", "��Ҫ��֤");

            RightCodeHT.Add("220", "�������");
            RightCodeHT.Add("250", "Ҫ����ʼ��������");
            RightCodeHT.Add("251", "�û��Ǳ��أ���ת����<forward-path>");
            RightCodeHT.Add("354", "�û��Ǳ��أ���ת����<forward-path>");
            RightCodeHT.Add("221", "��ʼ�ʼ����룬��<enter>.<enter>����");
            RightCodeHT.Add("334", "����رմ����ŵ�");
            RightCodeHT.Add("235", "��������Ӧ��֤Base64�ַ���");
		}


		/// <summary>
		/// ���ַ�������ΪBase64�ַ���
		/// </summary>
		/// <param name="str">Ҫ������ַ���</param>
		private string Base64Encode(string str)
		{
			byte[] barray;
			barray=Encoding.Default.GetBytes(str);
			return Convert.ToBase64String(barray);
		}


		/// <summary>
		/// ��Base64�ַ�������Ϊ��ͨ�ַ���
		/// </summary>
		/// <param name="str">Ҫ������ַ���</param>
		private string Base64Decode(string str)
		{
			byte[] barray;
			barray=Convert.FromBase64String(str);
			return Encoding.Default.GetString(barray);
		}


		/// <summary>
		/// �õ��ϴ��������ļ���
		/// </summary>
		/// <param name="FilePath">�����ľ���·��</param>
		private string GetStream(string FilePath)
		{
			//�����ļ�������
			System.IO.FileStream FileStr=new System.IO.FileStream(FilePath,System.IO.FileMode.Open);
			byte[] by=new byte[System.Convert.ToInt32(FileStr.Length)];
			FileStr.Read(by,0,by.Length);
			FileStr.Close();
			return(System.Convert.ToBase64String(by));
		}

		/// <summary>
		/// ����SMTP����
		/// </summary> 
		private bool SendCommand(string str)
		{
			byte[]WriteBuffer;
			if(str==null||str.Trim()==String.Empty)
			{
				return true;
			}
			logs+=str;
			WriteBuffer = Encoding.Default.GetBytes(str);
			try
			{
				ns.Write(WriteBuffer,0,WriteBuffer.Length);
			}
			catch
			{
                errmsg = "�������Ӵ���";
				return false;
			}
			return true;
		}

		/// <summary>
		/// ����SMTP��������Ӧ
		/// </summary>
		private string RecvResponse()
		{
			int StreamSize;
			string Returnvalue = String.Empty;
			byte[]ReadBuffer = new byte[1024] ;
			try
			{
				StreamSize=ns.Read(ReadBuffer,0,ReadBuffer.Length);
			}
			catch
			{
                errmsg = "�������Ӵ���";
				return "false";
			}

			if (StreamSize==0)
			{
				return Returnvalue ;
			}
			else
			{
				Returnvalue = Encoding.Default.GetString(ReadBuffer).Substring(0,StreamSize);
				logs+=Returnvalue+this.enter;
				return Returnvalue;
			}
		}

		/// <summary>
		/// �����������������һ��������ջ�Ӧ��
		/// </summary>
		/// <param name="str">һ��Ҫ���͵�����</param>
		/// <param name="errstr">�������Ҫ��������Ϣ</param>
		private bool Dialog(string str,string errstr)
		{
			if(str==null||str.Trim()=="")
			{
				return true;
			}
			if(SendCommand(str))
			{
				string RR=RecvResponse();
				if(RR=="false")
				{
					return false;
				}
				string RRCode=RR.Substring(0,3);
				if(RightCodeHT[RRCode]!=null)
				{
					return true;
				}
				else
				{
					if(ErrCodeHT[RRCode]!=null)
					{
						errmsg+=(RRCode+ErrCodeHT[RRCode].ToString());
						errmsg+=enter;
					}
					else
					{
						errmsg+=RR;
					}
					errmsg+=errstr;
					return false;
				}
			}
			else
			{
				return false;
			}

		}


		/// <summary>
		/// �����������������һ��������ջ�Ӧ��
		/// </summary>

		private bool Dialog(string[] str,string errstr)
		{
			for(int i=0;i<str.Length;i++)
			{
				if(!Dialog(str[i],""))
				{
					errmsg+=enter;
					errmsg+=errstr;
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// SendEmail
		/// </summary>
		/// <returns></returns>
		private bool SendEmail()
		{
			//��������
			try
			{
				tc=new TcpClient(mailserver,mailserverport);
			}
			catch(Exception e)
			{
				errmsg=e.ToString();
				return false;
			}

			ns = tc.GetStream();
			SMTPCodeAdd();

			//��֤���������Ƿ���ȷ
			if(RightCodeHT[RecvResponse().Substring(0,3)]==null)
			{
                errmsg = "��������ʧ��";
				return false;
			}


			string[] SendBuffer;
			string SendBufferstr;

			//����SMTP��֤
			if(ESmtp)
			{
				SendBuffer=new String[4];
				SendBuffer[0]="EHLO " + mailserver + enter;
				SendBuffer[1]="AUTH LOGIN" + enter;
				SendBuffer[2]=Base64Encode(username) + enter;
				SendBuffer[3]=Base64Encode(password) + enter;
                if (!Dialog(SendBuffer, "SMTP��������֤ʧ�ܣ���˶��û��������롣"))
					return false;
			}
			else
			{
				SendBufferstr="HELO " + mailserver + enter;
				if(!Dialog(SendBufferstr,""))
					return false;
			}

			//
			SendBufferstr="MAIL FROM:<" + From + ">" + enter;
            if (!Dialog(SendBufferstr, "�����˵�ַ���󣬻���Ϊ��"))
				return false;

			//
			SendBuffer=new string[recipientmaxnum];
			for(int i=0;i<Recipient.Count;i++)
			{
				SendBuffer[i]="RCPT TO:<" + Recipient[i].ToString() +">" + enter;

			}
            if (!Dialog(SendBuffer, "�ռ��˵�ַ����"))
				return false;

			/*
			SendBuffer=new string[10];
			for(int i=0;i<RecipientBCC.Count;i++)
			{

			SendBuffer[i]="RCPT TO:<" + RecipientBCC[i].ToString() +">" + enter;

			}

			if(!Dialog(SendBuffer,"�ܼ��ռ��˵�ַ����"))
			return false;
			*/
			SendBufferstr="DATA" + enter;
			if(!Dialog(SendBufferstr,""))
				return false;

			SendBufferstr="From:" + FromName + "<" + From +">" +enter;

			//if(ReplyTo.Trim()!="")
			//{
			// SendBufferstr+="Reply-To: " + ReplyTo + enter;
			//}

			//SendBufferstr+="To:" + RecipientName + "<" + Recipient[0] +">" +enter;
			SendBufferstr += "To:=?"+Charset.ToUpper()+"?B?"+Base64Encode(RecipientName)+"?="+"<"+Recipient[0]

				+">"+enter;

			SendBufferstr+="CC:";
			for(int i=0;i<Recipient.Count;i++)
			{
				SendBufferstr+=Recipient[i].ToString() + "<" + Recipient[i].ToString() +">,";
			}
			SendBufferstr+=enter;

			SendBufferstr+=((Subject==String.Empty || Subject==null)?"Subject:":((Charset=="")?("Subject:" + 

				Subject):("Subject:" + "=?" + Charset.ToUpper() + "?B?" + Base64Encode(Subject) +"?="))) + enter;
			SendBufferstr+="X-Priority:" + priority + enter;
			SendBufferstr+="X-MSMail-Priority:" + priority + enter;
			SendBufferstr+="Importance:" + priority + enter;
			SendBufferstr+="X-Mailer: Shanda.ESalse.StmpMail [lzh]" + enter;
			SendBufferstr+="MIME-Version: 1.0" + enter;
			if(Attachments.Count!=0)
			{
				SendBufferstr+="Content-Type: multipart/mixed;" + enter;
				SendBufferstr += " boundary=\"====="+

					(Html?"NextPart_liang19770542_":"NextPart_liang19771112_")+"=====\""+enter+enter;
			}

			if(Html)
			{
				if(Attachments.Count==0)
				{
					SendBufferstr += "Content-Type: multipart/alternative;"+enter;//���ݸ�ʽ�ͷָ���
					SendBufferstr += " boundary=\"=====NextPart_liang19771222_=====\""+enter+enter;
					//SendBufferstr += " boundary=\"====="+"lzh"+new Random().NextDouble().ToString()+"_=====\""+enter+enter;

					SendBufferstr += "This is a multi-part message in MIME format."+enter+enter;
				}
				else
				{
					SendBufferstr +="This is a multi-part message in MIME format."+enter+enter;
					SendBufferstr += "--=====NextPart_liang19770542_====="+enter;
					SendBufferstr += "Content-Type: multipart/alternative;"+enter;//���ݸ�ʽ�ͷָ���
					SendBufferstr += " boundary=\"=====NextPart_liang19771222_=====\""+enter+enter; 


				}
				SendBufferstr += "--=====NextPart_liang19771222_====="+enter;
				SendBufferstr += "Content-Type: text/plain;"+ enter;
				SendBufferstr += ((Charset=="")?(" charset=\"iso-8859-1\""):(" charset=\"" + 

					Charset.ToLower() + "\"")) + enter;
				SendBufferstr+="Content-Transfer-Encoding: base64" + enter + enter;
                SendBufferstr += Base64Encode("�ʼ�����ΪHTML��ʽ����ѡ��HTML��ʽ�鿴") + enter + enter;

				SendBufferstr += "--=====NextPart_liang19771222_====="+enter;



				SendBufferstr+="Content-Type: text/html;" + enter;
				SendBufferstr+=((Charset=="")?(" charset=\"iso-8859-1\""):(" charset=\"" + 

					Charset.ToLower() + "\"")) + enter;
				SendBufferstr+="Content-Transfer-Encoding: base64" + enter + enter;
				SendBufferstr+= Base64Encode(Body) + enter + enter;
				SendBufferstr += "--=====NextPart_liang19771222_=====--"+enter;
			}
			else
			{
				if(Attachments.Count!=0)
				{
					SendBufferstr += "--=====NextPart_liang19771112_====="+enter;
				}
				SendBufferstr+="Content-Type: text/plain;" + enter;
				SendBufferstr+=((Charset=="")?(" charset=\"iso-8859-1\""):(" charset=\"" + 

					Charset.ToLower() + "\"")) + enter;
				SendBufferstr+="Content-Transfer-Encoding: base64" + enter + enter;
				SendBufferstr+= Base64Encode(Body) + enter;
			}

			//SendBufferstr += "Content-Transfer-Encoding: base64"+enter;




			if(Attachments.Count!=0)
			{
				for(int i=0;i<Attachments.Count;i++)
				{
					string filepath = (string)Attachments[i];
					SendBufferstr += "--====="+ 

						(Html?"NextPart_liang19770542_":"NextPart_liang19771112_") +"====="+enter;
					//SendBufferstr += "Content-Type: application/octet-stream"+enter;
					SendBufferstr += "Content-Type: text/plain;"+enter;
					SendBufferstr += " name=\"=?"+Charset.ToUpper()+"?B?"+Base64Encode

						(filepath.Substring(filepath.LastIndexOf("\\")+1))+"?=\""+enter;
					SendBufferstr += "Content-Transfer-Encoding: base64"+enter;
					SendBufferstr += "Content-Disposition: attachment;"+enter;
					SendBufferstr += " filename=\"=?"+Charset.ToUpper()+"?B?"+Base64Encode

						(filepath.Substring(filepath.LastIndexOf("\\")+1))+"?=\""+enter+enter;
					SendBufferstr += GetStream(filepath)+enter+enter;
				}
				SendBufferstr += "--====="+ (Html?"NextPart_liang19770542_":"NextPart_liang19771112_") 

					+"=====--"+enter+enter;
			}



			SendBufferstr += enter + "." + enter;

            if (!Dialog(SendBufferstr, "�����ż���Ϣ"))
				return false;


			SendBufferstr="QUIT" + enter;
            if (!Dialog(SendBufferstr, "�Ͽ�����ʱ����"))
				return false;


			ns.Close();
			tc.Close();
			return true;
		}


		#endregion
	}
}