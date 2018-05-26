# Html-Parser
Parse the html file  in c# language
##### The element almost Equels to Javasceript node,you can operate the elemnet as node what you do in javascript except css;
##### It Parses very fast, 140,000,000 chars per one second


### Usage
``` c#
var b=Downloader.DownloadHtmlWithEncoding("http://ww.baidu.com",Encoding.Utf8);
HtmlParser hp=new HtmlParser();
var d=hp.Parse(b);//get the root node html
var title=d.Get_All_Children_By_Element_Type(ElementType.title)[0];//title will be "百度一下，你就知道了"；


