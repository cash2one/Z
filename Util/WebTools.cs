using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Xml;
using Z.C;
using Z.Log;
using Z.OAuth;

namespace Z.Util
{
    /// <summary>
    /// ΪWebӦ���ṩ�Ĺ���
    /// </summary>
    public static class WebTools
    {
        private readonly static Logger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        static WebTools()
        {
            IPAddress[] list = Dns.GetHostAddresses(Dns.GetHostName());
            if (list.Length == 1)
                ServerIp = list[0].ToString();
            else if(list.Length > 1)
            {
                ServerIp = list[0].ToString();
                if(ServerIp.StartsWith("10."))
                    ServerIp = list[1].ToString();
            }
        }

        #region ��ȡ�ͻ��˵ĵ�ַ

        /// <summary>
        /// ��ȡ�ͻ��˵ĵ�ַ
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = string.Empty;

            try
            {
                if (HttpContext.Current != null)
                {
                    result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (string.IsNullOrEmpty(result))
                    {
                        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        result = HttpContext.Current.Request.UserHostAddress;
                    }

                    if (string.IsNullOrEmpty(result) == false && result.IndexOf(',') >= 0)
                    {
                        result = result.Split(',')[0];
                    }
                }
            }
            catch
            {
                result = "-1.-1.-1.-1";
            }

            return result;
        }

        #endregion

        #region ��ȡ�ͻ��˵ĵ�ַ

        /// <summary>
        /// ��ȡ�ͻ��˵ĵ�ַ
        /// </summary>
        /// <returns></returns>
        public static string GetClientHostName()
        {
            string result = string.Empty;

            try
            {
                if (HttpContext.Current != null)
                {
                    result = HttpContext.Current.Request.UserHostName;
                }
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region ��ȡ��ǰ�������ķ���������

        private static string ServerIp = string.Empty;

        /// <summary>
        /// ��ȡ��ǰ�������ķ�����IP(��IP������»�ȡ������)
        /// </summary>
        /// <returns></returns>
        public static string MyServerIP
        {
            get
            {
                if (string.IsNullOrEmpty(ServerIp))
                {   
                    string hostName = Dns.GetHostName();
                    IPAddress[] address = Dns.GetHostAddresses(hostName);
                    ServerIp = address.Length > 0 ? address[0].ToString() : hostName;
                }
                return ServerIp;
            }
        }

        #endregion

        #region PostData

        const string sUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        const string sContentType = "application/x-www-form-urlencoded";

        /// <summary>  
        /// Post data��url  
        /// </summary>   
        /// <param name="data">Ҫpost������</param> 
        /// <param name="url">Ŀ��url</param> 
        /// <param name="encoding">����/���ñ���</param> 
        /// <returns>��������Ӧ</returns>   
        public static string PostDataToUrlWithoutAgent(string data, string url, Encoding encoding)
        {
            byte[] bytesToPost = encoding.GetBytes(data);
            return PostDataToUrl(bytesToPost, url, encoding, "Z");
        }

        /// <summary>  
        /// Post data��url  
        /// </summary>   
        /// <param name="data">Ҫpost������</param> 
        /// <param name="url">Ŀ��url</param> 
        /// <param name="encoding">����/���ñ���</param> 
        /// <param name="agent">�ͻ��˴���</param>
        /// <returns>��������Ӧ</returns>   
        public static string PostDataToUrl(string data, string url, Encoding encoding, string agent)
        {
            byte[] bytesToPost = encoding.GetBytes(data);
            return PostDataToUrl(bytesToPost, url, encoding, agent);
        }

        /// <summary> 
        /// Post data��url  
        /// </summary>
        /// <param name="data">Ҫpost������</param> 
        /// <param name="url">Ŀ��url</param>  
        /// <param name="encoding">����/���ñ���</param>
        /// <returns>��������Ӧ</returns> 
        /// <param name="agent">�ͻ��˴�������</param>
        static string PostDataToUrl(byte[] data, string url, Encoding encoding, string agent)
        {

            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                throw new ApplicationException(string.Format("Invalid url string: {0}", url));
            }

            if (string.IsNullOrEmpty(agent))
                httpRequest.UserAgent = sUserAgent;
            else
                httpRequest.UserAgent = agent;
            httpRequest.ContentType = sContentType;
            httpRequest.Method = "POST";
            httpRequest.ContentLength = data.Length;
            Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            Stream responseStream;
            try
            {
                responseStream = httpRequest.GetResponse().GetResponseStream();
            }
            catch (Exception e)
            {
                throw e;
            }
            string stringResponse = string.Empty;
            using (StreamReader responseReader = new StreamReader(responseStream, encoding))
            {
                stringResponse = responseReader.ReadToEnd();
            }
            responseStream.Close();
            return stringResponse;
        }

        #endregion

        #region ����һ��ָ���ļ�, �������ݵ��ַ���

        /// <summary>
        /// ����һ��ָ���ļ�, �������ݵ��ַ���
        /// </summary>
        /// <param name="url">URL��ַ</param>
        /// <param name="TimeoutSecond">��ʱ����, ����Ϊ��λ</param>
        /// <remarks>�˷�������ҪĿ���ǽ��WebClientû��TimeOut������</remarks>
        /// <returns></returns>
        public static string DownloadStringWithSecond(string url, int TimeoutSecond)
        {
            return DownloadString(url, TimeoutSecond * 1000);
        }
          

        /// <summary>
        /// ����һ��ָ���ļ�, �������ݵ��ַ���
        /// </summary>
        /// <param name="url"></param>
        /// <param name="TimeoutMilliseconds">��ʱ����, �Ժ���Ϊ��λ</param>
        /// <remarks>�˷�������ҪĿ���ǽ��WebClientû��TimeOut������</remarks>
        /// <returns></returns>
        public static string DownloadString(string url, int TimeoutMilliseconds)
        {
            return DownloadString(url, TimeoutMilliseconds, new UTF8Encoding(false));
        }

        /// <summary>
        /// ��������, ʹ��Ĭ�ϵ�UserAgent
        /// </summary>
        /// <param name="url"></param>
        /// <param name="TimeoutMilliseconds"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string DownloadString(string url, int TimeoutMilliseconds, Encoding encoding)
        {
            return DownloadString(url, TimeoutMilliseconds, encoding, string.Empty);
        }


        /// <summary>
        /// ����һ��ָ���ļ�, �������ݵ��ַ���
        /// </summary>
        /// <param name="url"></param>
        /// <param name="TimeoutMilliseconds">��ʱ����, �Ժ���Ϊ��λ</param>
        /// <param name="encoding">�����ļ��ı����ʽ</param>
        /// <param name="UserAgent">UserAgent</param>
        /// <remarks>�˷�������ҪĿ���ǽ��WebClientû��TimeOut������</remarks>
        /// <returns></returns>
        public static string DownloadString(string url, int TimeoutMilliseconds, Encoding encoding, string UserAgent)
        {
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Timeout = TimeoutMilliseconds;
            req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

            if(string.IsNullOrEmpty(UserAgent) == false)
                req.UserAgent = UserAgent;
            
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            Stream s = resp.GetResponseStream();
            StringBuilder sb = new StringBuilder();
            StreamReader reader = new StreamReader(s, encoding);

            using (TextWriter writer = new StringWriter(sb))
            {
                writer.Write(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
            }

            return sb.ToString();
        }

        /// <summary>
        /// ����һ��ָ���ļ�, �������ݵ��ַ���
        /// </summary>
        /// <param name="config">������Ϣ</param>
        /// <param name="objs">�����б�</param>
        /// <returns></returns>
        public static string DownloadStringWithSecond(WebServerConfiguration config, params object[] objs)
        {
            string url = string.Format(config.Address, objs);

            return DownloadString(url, config.TimeoutSecond * 1000);
        }

        #endregion

        #region ��һ���ļ����ص�ָ����λ��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpAddress"></param>
        /// <param name="localPath"></param>
        public static void DownloadFile(string httpAddress, string localPath)
        {
            var request = WebRequest.Create(httpAddress);

            using (var localFile = File.OpenWrite(localPath))
            {
                request.GetResponse().GetResponseStream().CopyTo(localFile);
            }
        }
        #endregion

        #region ��ȡ Request ���صĲ���

        /// <summary>
        /// ��ȡ Request ���صĲ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="IsThrowException">������������Ƿ��׳��쳣</param>
        /// <returns></returns>
        public static T GetRequestValue<T>(string name, T defaultValue, bool IsThrowException)
        {
            string s = HttpContext.Current.Request[name];
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    if (!IsThrowException)
                        return defaultValue;

                    throw new InvalidCastException("ҳ��δ�������:" + name);
                }

                return (T)Convert.ChangeType(s, typeof(T));
            }
            catch (InvalidCastException)
            { throw; }
            catch
            {
                if(IsThrowException)
                    throw new InvalidCastException("ҳ�洫�����" + name + "��ֵ" + s + "���Ϸ�");

                return defaultValue;
            }
        }

        #endregion

        /// <summary>
        /// �ж�Զ���ļ��Ƿ����
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsExist(string uri)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = "HEAD";
                req.Timeout = 1000;
                res = (HttpWebResponse)req.GetResponse();
                return (res.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }
        }

        /// <summary>
        /// ����һ������JSON���ݵ�Web��ַ
        /// </summary>
        public static T CallJsonApi<T>(this WebRequestDescriptor proxyRequest, object[] urlArgs, object[] postArgs)
            where T : class
        {
            return GetResponse(proxyRequest, urlArgs, postArgs).ExtractJsonData<T>();
        }

        /// <summary>
        /// ��Web��Ӧ����ȡ��JSON����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T ExtractJsonData<T>(this WebResponse response)
            where T : class
        {
            using (var responseStream = response.GetResponseStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                return ser.ReadObject(responseStream) as T;
            }
        }

        /// <summary>
        /// ����һ������Xml���ݵ�Web��ַ
        /// </summary>
        public static XmlReader CallXmlApi(this WebRequestDescriptor proxyRequest, object[] urlArgs, object[] postArgs)
        {
            return new XmlTextReader(GetResponse(proxyRequest, urlArgs, postArgs).GetResponseStream());
        }

        /// <summary>
        /// ����һ��Oauth API, �Զ�����ǩ���߼�����������Body��
        /// </summary>
        /// <param name="oauthInfo">OAuth��֤��Ϣ</param>
        /// <param name="parameters">ҵ����ز�������OAuth��֤��Ϣ�е�����������</param>
        /// <param name="contentType">������������</param>
        /// <returns></returns>
        public static WebResponse CallOAuthApi(this OAuthInfomation oauthInfo, QueryParameterList parameters, string contentType = "application/x-www-form-urlencoded")
        {
            var oauth = new OAuthSignatureGenerator();
            var result = oauth.GenerateSignature(oauthInfo, parameters);
            var postData = Encoding.UTF8.GetBytes(parameters.ToString());
            var request = WebRequest.Create(oauthInfo.RequestUri);
            request.Method = oauthInfo.HttpMethod;
            request.ContentLength = postData.Length;
            request.ContentType = contentType;
            request.Headers["Authorization"] = oauthInfo.GenerateAuthorizationHeader(result.Signature);
            using (var responseStream = request.GetRequestStream())
            {
                responseStream.Write(postData, 0, postData.Length);
            }

            return request.GetResponse();
        }

        /// <summary>
        /// ����һ�����ض����������ݵ�Web��ַ�������ļ�����
        /// </summary>
        public static WebResponse CallStreamApi(this WebRequestDescriptor proxyRequest, object[] urlArgs, object[] postArgs)
        {
            return GetResponse(proxyRequest, urlArgs, postArgs);
        }

        /// <summary>
        /// ��HTTP����ͷ�У���Basic��֤��ʽ��ȡ����֤��Ϣ��
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Tuple<string, string> ExtractBasicAuthorization(this HttpRequest request)
        {
            var auth = request.Headers["Authorization"];
            if (auth == null)
            {
                return new Tuple<string, string>(null, null);
            }
            var authString = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring(6)));
            var authParas = authString.Split(new[] { ':' });
            if (authParas.Length == 2)
            {
                return new Tuple<string, string>(authParas[0], authParas[1]);
            }
            else
            {
                return new Tuple<string, string>(null, null);
            }
        }

        private static WebResponse GetResponse(WebRequestDescriptor proxyRequest, object[] urlArgs, object[] postArgs)
        {
            var postData = Encoding.UTF8.GetBytes(String.Format(proxyRequest.PostData, postArgs));
            var request = WebRequest.Create(String.Format(proxyRequest.Url, urlArgs)) as HttpWebRequest;
            if (!String.IsNullOrEmpty(request.Host))
                request.Host = proxyRequest.Host;
            request.Method = proxyRequest.Method;
            request.ContentLength = postData.Length;
            request.CookieContainer = new CookieContainer();
            request.ContentType = proxyRequest.ContentType;
            using (var responseStream = request.GetRequestStream())
            {
                responseStream.Write(postData, 0, postData.Length);
            }

            return request.GetResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="TimeoutMilliseconds"></param>
        /// <param name="UserAgent"></param>
        /// <returns></returns>
        public static Stream DownloadImage(string url, int TimeoutMilliseconds, string UserAgent)
        {
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Timeout = TimeoutMilliseconds;
            req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            if (string.IsNullOrEmpty(UserAgent))
                req.UserAgent = sUserAgent;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream s = resp.GetResponseStream();
            return s;
        }

        /// <summary>
        /// �淶��URI
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string Normalize(this Uri uri)
        {
            var normalizedUrl = String.Format("{0}://{1}", uri.Scheme, uri.Host);
            // ����Ĭ�ϵĶ˿ں�
            if (!((uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443)))
            {
                normalizedUrl += ":" + uri.Port;
            }
            normalizedUrl += uri.AbsolutePath;

            return normalizedUrl;
        }

        /// <summary>
        /// �ж�IPV4�ĵ�ַ��ָ��������
        /// </summary>
        /// <param name="ip">ipv4��ַ</param>
        /// <param name="subNet">������ַ</param>
        /// <param name="subNetMask">���������ַ</param>
        /// <returns></returns>
        public static bool IsIpInSubNet(string ip, string subNet, string subNetMask)
        {
            IPAddress subnetAddr = IPAddress.Parse(subNet);
            IPAddress ipAddr = IPAddress.Parse(ip);
            IPAddress subnetMaskAddr = IPAddress.Parse(subNetMask);
#pragma warning disable 618
            return subnetAddr.Address == (ipAddr.Address & subnetMaskAddr.Address);
#pragma warning restore 618
        }
    }
}
