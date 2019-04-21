using HtmlAgilityPack;
using Jamine.Parser.Html;
using System;
using System.Diagnostics;
using System.IO;

namespace ParserTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new HtmlParser();
            var psrWatch = new Stopwatch();

            var page = File.ReadAllText("youku-home-page.txt");
            psrWatch.Start();
            var root = parser.Parse(page);
            var htmlParserElapsed = psrWatch.ElapsedMilliseconds;

            var doc = new HtmlDocument();
            var hapWatch = new Stopwatch();
            hapWatch.Start();
            doc.LoadHtml(page);
            var hapElapsed = hapWatch.ElapsedMilliseconds;

            Console.WriteLine(root);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"Tset parsing speed of youku-home page,  page length : {page.Length}");
            Console.WriteLine($"HtmlAgilityPack Time-Spnd:{hapWatch.Elapsed.Milliseconds}ms");
            Console.WriteLine($"HtmlParser Time-Spend:{htmlParserElapsed}ms");
            Console.Read();
        }
    }
}
