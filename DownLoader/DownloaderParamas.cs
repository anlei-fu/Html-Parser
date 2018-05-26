using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fal.Spider
{
    /******************************
     * Http 请求头的封装
     * Copyright all rights reserved by fal
     * e-mail:18108342263@163.com
     * ****************************/
    public class DownlaodParamas
    {
        public DownlaodParamas()
        { }
        public DownlaodParamas(string url)
        {
            Url = url;
        }
        /// <summary>
        /// 等待时长
        /// </summary>
        public int Timeout { get; set; } = 3000;
        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string UserAgent { get; set; }= "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; BOIE9;ZHCN)";
        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; } = "GET";
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = "";
        /// <summary>
        /// 接受类型
        /// </summary>
        public string Accept { get; set; } = "*/*";
        /// <summary>
        /// 
        /// </summary>
        public bool Keep_Alive { get; set; } = true;
        /// <summary>
        /// 代理服务器
        /// </summary>
        public WebProxy Proxy { get; set; }
        /// <summary>
        /// 相联系的网页地址
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public CookieCollection Cookie { get; set; }
        /// <summary>
        /// http 头
        /// </summary>
        public WebHeaderCollection Headers { get; set; }
    }
    /******************
     * 下载器的结果类
     * Copyright all rights reserved by fal
     * e-mail:18108342263@163.com
     * **********************************/
    public class DownloadReults
    {
        /// <summary>
        /// 网页字符串
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// 是否下载成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        ///传过来的http 头
        /// </summary>
        public WebHeaderCollection Headers { get; set; }
        /// <summary>
        /// 传过来的cookie
        /// </summary>
        public CookieCollection Cookie { get; set; }
        
    }
}
