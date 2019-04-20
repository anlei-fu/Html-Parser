using System.Collections.Generic;

namespace Jasmine.Parsers.Html
{
    /// <summary>
    /// use to replace html escape-chars
    /// </summary>
    public class HtmlEscapeStringMapper
    {
        /// <summary>
        /// this dictionary not includes all  escape-chars
        /// </summary>
        private static Dictionary<string, char> _dic = new Dictionary<string, char>()
        {
            {"&quot;",'"' },  {"&#34;",'"' },  {"&#x22;",'"' },  {"&amp;",'&' },  {"&#38;",'&' },  {"&#x26;",'&' },
            { "&lt;",'<' },  {"&#60;",'<' },
            {"&#x3C;",'<' },  {"&gt;",'>' },  {"&#62;",'>' },  {"&#x3E;",'>' },    {"&#160;",' ' },  {"&#xA0;",' ' },
            { "&nbsp;",' ' },  {"&iexcl;",'¡' },
            {"&cent;",'¢' },  {"&pound;",'£' },  {"&curren;",'¤' },  {"&yen;",'¥' },  {"&sect;",'§' },  {"&uml;",'¨' },  {"&copy;",'©' },
            { "&ordf;",'ª' },  {"&laquo;",'«' },
            { "&not;",'¬' },
            {"&shy;",' ' },  {"&reg;",'®' },  {"&macr;",'¯' },  {"&deg;",'°' },  {"&plusmn;",'±' },  {"&sup2;",'²' },
            { "&sup3;",'³' },  {"&acute;",'´' },  {"&micro;",'µ' },
        };
        public static bool Contains(string escapeString) => _dic.ContainsKey(escapeString);
        public static string Replace(string escapeString)
        {
            return _dic.ContainsKey(escapeString) ? _dic[escapeString].ToString() : escapeString;
        }
       
    }

}
