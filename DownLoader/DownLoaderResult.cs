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
