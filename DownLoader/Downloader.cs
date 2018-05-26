using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
namespace Fal
{
    namespace Spider
    {
        /**********************************************
         * html 下载类
         * Copyright all rights reserved by fal
         * e-mail:18108342263@163.com
         * *****************************************/
        public  class DownLoader
        {
            /// <summary>
            /// 超时设置
            /// </summary>
            public static int timeout { get; set; } = 3000;
            /// <summary>
            /// 通过url下载图片
            /// </summary>
            /// <param name="url"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            public static Image DownloadImage(string url, int timeout)
            {
                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Timeout = timeout;
                    WebResponse response = request.GetResponse();
                    Stream resStream = response.GetResponseStream();
                    return Image.FromStream(resStream);
                }
                catch
                {
                    return null;
                }
            }
            /// <summary>
            /// 使用指定编码 获取网页
            /// </summary>
            /// <param name="url"></param>
            /// <param name="encoding"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            public static string DownloadHtmlWithEncoding(string url, Encoding encoding, int timeout = 3000)
            {
                try
                {
                    var request =createRequest(url,timeout);
                    var response = (HttpWebResponse)request.GetResponse();
                    var resStream = response.GetResponseStream();
                    using (var sr = new StreamReader(resStream, encoding))
                                    return sr.ReadToEnd();
                }
                catch
                {
                    return null;
                }
            }
            /// <summary>
            /// 获取网页，只解码一次
            /// </summary>
            /// <param name="url"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            public static string DownloadHtml(string url, int timeout = 10000)
            {
                try
                {
                    var request = createRequest(url, timeout);
                    var response = (HttpWebResponse)request.GetResponse();
                    var c = response.ContentEncoding;
                    var resStream = response.GetResponseStream();
                    using (var sr = new StreamReader(resStream, Encoding.GetEncoding(c)))
                           return sr.ReadToEnd();
                }
                catch
                {
                    return null;
                }
            }
            /// <summary>
            /// 获取网页，可能解码两次
            /// 在未知网页编码时使用
            /// </summary>
            /// <param name="url"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            public static string Download(string url, int timeout = 10000)
            {
                try
                {
                    byte[] array = null;
                    using (WebResponse wr = createRequest(url, timeout).GetResponse())
                    {
                        Stream stream = wr.GetResponseStream();
                        array = getByte(stream);
                        return getString(array);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            /// <summary>
            /// 通过完整 http获取网页
            /// 如果用默认请求头，可能会遭到某些网站的屏蔽
            /// 此下载函数 可以添加cookie，代理服务器等信息
            /// 在下载结果中 也会保留 cookie等信息
            /// </summary>
            /// <param name="parama">request 参数</param>
            /// <returns></returns>
            public static DownloadReults DownloadHtml(DownlaodParamas parama)
            {
                DownloadReults result = new DownloadReults();
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(parama.Url);
                    request.Headers = parama.Headers;
                    request.Timeout = parama.Timeout;
                    request.CookieContainer.Add(new Uri(parama.Url), parama.Cookie);
                    request.UserAgent = parama.UserAgent;
                    request.Accept = parama.Accept;
                    request.KeepAlive = parama.Keep_Alive;
                    request.Proxy = parama.Proxy;

                    var response = (HttpWebResponse)request.GetResponse();
                    var stream= response.GetResponseStream();
                    var sr = new StreamReader(stream);
                    var array = getByte(stream);
                    result.Html = getString(array); 
                    result.Headers = response.Headers;
                    result.Cookie = response.Cookies;
                    result.IsSuccess = true;
                }
                catch
                {
                    result.IsSuccess = false;
                }
                return result;
            }
            /// <summary>
            /// 通过url下载文件
            /// </summary>
            /// <param name="url"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            public static FileStream DownloadFile(string url, int timeout = 20000000)
            {
                try
                {
                    var request = WebRequest.Create(url);
                    request.Timeout = timeout;
                    var response = request.GetResponse();
                    var resStream = response.GetResponseStream();
                    return (FileStream)resStream;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    sr.Close();
                    resStream.Close();
                }
            }
            /// <summary>
            /// request对象
            /// </summary>
            private static WebRequest request;
            /// <summary>
            /// response对象
            /// </summary>
            private static WebResponse response;
            /// <summary>
            /// 网络流
            /// </summary>
            private static Stream resStream;
            /// <summary>
            /// reader
            /// </summary>
            private static StreamReader sr;
            /// <summary>
            /// 下载到的网页
            /// </summary>
            private static string content;
            /// <summary>
            /// 是否是默认编码
            /// 默认编码为 utf-8
            /// </summary>
            /// <returns></returns>
            private static bool isDefaultEncoding()
            {
                try
                {
                    sr = new StreamReader(resStream, Encoding.Default);
                    content = sr.ReadToEnd();
                    content = content.Replace(" ", "");
                    resStream.Close();
                    sr.Close();
                    sr.Dispose();
                    if (content.IndexOf("charset=\"utf") != -1 || content.IndexOf("charset=\"UTF") != -1 || content.IndexOf("charset=utf") != -1 || content.IndexOf("charset=UTF") != -1) return false;
                    else return true;
                }
                catch
                {
                    return false;
                }
            }
            /// <summary>
            /// 创建request对象
            /// </summary>
            /// <param name="url"></param>
            /// <param name="timeout"></param>
            /// <returns></returns>
            private static HttpWebRequest createRequest(string url, int timeout)
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; BOIE9;ZHCN)";
                req.Method = "GET";
                req.Accept = "*/*";
                req.Timeout = timeout;
                return req;
            }
            /// <summary>
            /// 保存字节流
            /// </summary>
            /// <param name="stream"></param>
            /// <returns></returns>
            private static byte[] getByte(Stream stream)
            {
                List<byte> ls = new List<byte>();
                byte[] buf = new byte[1024];
                while (true)
                {
                    int len = stream.Read(buf, 0, buf.Length);
                    if (len <= 0)
                        break;
                    ls.AddRange(buf);
                }
                return ls.ToArray();
            }
            /// <summary>
            /// 获取字符串
            /// </summary>
            /// <param name="bt"></param>
            /// <returns></returns>
            private static string getString(byte[] bt)
            {
                var text = Encoding.UTF8.GetString(bt);
                var charset = SpiderHelper.Get_CharSet(Encoding.UTF8.GetString(bt));
                if (charset.ToUpper().IndexOf("UTF") == -1)
                    return Encoding.Default.GetString(bt);
                return text;
            }
            /// <summary>
            /// 通过解析过的网页获取编码
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            private static Encoding getEncoding(string str)
            {
                str = str.ToUpper();
                if (str.Contains("GB"))
                    return Encoding.Default;
                else
                    return Encoding.UTF8;
            }
        }
    }
}
