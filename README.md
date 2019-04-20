# Html-Parser
Parse the html page in c# language
##### The element almost equels to javasceript-node,you can operate the elemnet as javasceript-node what you do in javascript excepts css;
##### It Parses very fast, 150,000,000 chars per one second


### Usage
``` c#
var page=Downloader.DownloadHtmlWithEncoding("http://www.baidu.com",Encoding.Utf8);
HtmlParser hp=new HtmlParser();
var root=hp.Parse(page);//get the root node html



