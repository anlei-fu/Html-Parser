# Html-Parser
Parse  html page in c# language
##### Provide normal element operation of html dom tree
##### Hight speed ,almost 150,000,000 char per second
##### A base component  of a net spider system


### Usage
``` c#
var page=Downloader.DownloadHtmlWithEncoding("http://www.baidu.com",Encoding.Utf8);
HtmlParser hp=new HtmlParser();
var root=hp.Parse(page);//get the root node html
var title=root.GetAllChildrenByTagName("title")[0];



