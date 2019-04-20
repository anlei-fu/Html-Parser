using System.Collections.Generic;
using System.Text;

namespace Jasmine.Parsers.Html
{
    /// <summary>
    /// entity of a html element
    /// </summary>
    public class Element 
    {
     
        public Element()
        {

        }
        public string Name { get; set; }
        public ElementType ElementType { get; set; }
        public AtrrributeCollection Attributes { get; set; } = new AtrrributeCollection();
        public int Count { get => Children.Count; }
        public string InnerHtml => ToString();
        public string InnerText { get; set; }
        internal SingleOrDouble SingleOrDouble { get; set; }
        internal StartOrEnd StartOrEnd { get; set; }
        public Element Parent { get; set; }
        public Element Left { get; set; }
        public bool HasLeft => Left != null;
        public Element Right { get; set; }
        public bool HasRight => Right != null;
        public List<Element> Children { get; set; } = new List<Element>();
        public bool HasChildren => Children.Count != 0;
       
        public Element this[int index]
        {
            get
            {
                return Children[index];
            }
        }
        public static ElementType GetElemnetType(string Name)
        {
            switch (Name)
            {
                case "!doctype": return ElementType.Doctype;
                case "a": return ElementType.A;
                case "div": return ElementType.Div;
                case "span": return ElementType.Span;
                case "li": return ElementType.Li;
                case "dl": return ElementType.Dl;
                case "dt": return ElementType.Dt;
                case "dd": return ElementType.Dd;
                case "tt": return ElementType.Tt;
                case "th": return ElementType.Th;
                case "ul": return ElementType.Ul;
                case "td": return ElementType.Td;
                case "p": return ElementType.P;
                case "h1": return ElementType.H1;
                case "h2": return ElementType.H2;
                case "h3": return ElementType.H3;
                case "br": return ElementType.Br;
                case "tr": return ElementType.Tr;
                case "style": return ElementType.Style;
                case "script": return ElementType.Script;
                case "h4": return ElementType.H4;
                case "h5": return ElementType.H5;
                case "h6": return ElementType.H6;
                case "img": return ElementType.Img;
                case "tbody": return ElementType.TBody;
                case "table": return ElementType.Table;
                case "ol": return ElementType.Ol;
                case "iframe": return ElementType.Iframe;
                case "input": return ElementType.Input;
                case "ins": return ElementType.Ins;
                case "title": return ElementType.Title;
                case "link": return ElementType.Link;
                case "i": return ElementType.I;
                case "meta": return ElementType.Meta;
                case "!DOCTYPE": return ElementType.Doctype;
                case "!": return ElementType.Annotation;
                case "body": return ElementType.Body;
                case "head": return ElementType.Head;
                case "html": return ElementType.Html;
                case "font": return ElementType.Font;
                case "footer": return ElementType.Footer;
                case "form": return ElementType.Form;
                case "abbr": return ElementType.Abbr;
                case "address": return ElementType.Address;
                case "applet": return ElementType.Applet;
                case "acronym": return ElementType.Acronym;
                case "area": return ElementType.Area;
                case "article": return ElementType.Article;
                case "aside": return ElementType.Aside;
                case "audio": return ElementType.Audio;
                case "base": return ElementType.Bese;
                case "basefont": return ElementType.BaseFont;
                case "bdi": return ElementType.Bdi;
                case "bdo": return ElementType.Bdo;
                case "big": return ElementType.Big;
                case "blockquote": return ElementType.BlockQuote;
                case "button": return ElementType.Button;
                case "b": return ElementType.B;
                case "canvas": return ElementType.Canvas;
                case "caption": return ElementType.Caption;
                case "center": return ElementType.Center;
                case "cite": return ElementType.Cite;
                case "code": return ElementType.Code;
                case "colgroup": return ElementType.ColGroup;
                case "col": return ElementType.Col;
                case "command": return ElementType.Command;
                case "datalist": return ElementType.DataList;
                case "del": return ElementType.Del;
                case "details": return ElementType.Details;
                case "dfn": return ElementType.Dfn;
                case "dir": return ElementType.Dir;
                case "embed": return ElementType.Embed;
                case "em": return ElementType.Em;
                case "fieldset": return ElementType.FieldSet;
                case "figcaption": return ElementType.Figcaption;
                case "figure": return ElementType.Figure;
                case "frameset": return ElementType.FrameSet;
                case "frame": return ElementType.Frame;
                case "header": return ElementType.Header;
                case "hgroup": return ElementType.Hgroup;
                case "hr": return ElementType.Hr;
                case "keygen": return ElementType.KeyGen;
                case "kbd": return ElementType.Kbd;
                case "label": return ElementType.Label;
                case "legend": return ElementType.Legend;
                case "map": return ElementType.Map;
                case "mark": return ElementType.Mark;
                case "menu": return ElementType.Menu;
                case "meter": return ElementType.Meter;
                case "nav": return ElementType.Nav;
                case "noframes": return ElementType.Noframes;
                case "noscript": return ElementType.Noscript;
                case "object": return ElementType.Abject;
                case "optgroup": return ElementType.OptGroup;
                case "option": return ElementType.Option;
                case "output": return ElementType.Output;
                case "param": return ElementType.Param;
                case "pre": return ElementType.Pre;
                case "progress": return ElementType.Progress;
                case "q": return ElementType.Q;
                case "rp": return ElementType.Rp;
                case "ruby": return ElementType.Ruby;
                case "samp": return ElementType.Samp;
                case "select": return ElementType.Select;
                case "small": return ElementType.Small;
                case "source": return ElementType.Source;
                case "strike": return ElementType.Strike;
                case "strong": return ElementType.Strong;
                case "sub": return ElementType.Sub;
                case "summary": return ElementType.Summary;
                case "sup": return ElementType.Sup;
                case "s": return ElementType.S;
                case "textarea": return ElementType.TextArea;
                case "tfoot": return ElementType.TFoot;
                case "thead": return ElementType.Thead;
                case "time": return ElementType.Time;
                case "track": return ElementType.Track;
                case "u": return ElementType.U;
                case "var": return ElementType.Var;
                case "video": return ElementType.Video;
                case "wbr": return ElementType.Wbr;
                case "rt": return ElementType.Rt;
                case "section": return ElementType.Section;
                default: return ElementType.Unknow;

            }
        }

        internal static SingleOrDouble GetSingleOrDouble(ElementType ElementType)
        {
            switch (ElementType)
            {
                case ElementType.Link:
                case ElementType.Img:
                case ElementType.Annotation:
                case ElementType.Doctype:
                case ElementType.Acronym:
                case ElementType.Area:
                case ElementType.Bese:
                case ElementType.BaseFont:
                case ElementType.Br:
                case ElementType.Embed:
                case ElementType.Input:
                case ElementType.Meta:
                case ElementType.Frame:
                case ElementType.Hr:
                case ElementType.KeyGen:
                case ElementType.Source:
                case ElementType.Track:
                case ElementType.Param:
                    return SingleOrDouble.Single;

                default:
                    return SingleOrDouble.Double;

            }

        }


        public void Add(Element element)
        {
            Children.Add(element);
            element.Parent = this;
        }
       
        public void AddRange(IEnumerable<Element> input)
        {
            foreach (var item in input)
                Add(item);
        }

        public void Remove(int index)
        {

            Children.RemoveAt(index);
        }
      
        public void Remove(string name)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].Name == name)
                {
                    Children.RemoveAt(i);
                    --i;
                }
        }
      
        public void Remove(ElementType type)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].ElementType == type)
                {
                    Children.RemoveAt(i);
                    --i;
                }

        }
       
        public void Remove(Attribute AttributeItem)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].Attributes.Contains(AttributeItem.Key))
                    if (Children[i].Attributes[AttributeItem.Key] == AttributeItem.Value)
                    {
                        Children.RemoveAt(i);
                        --i;
                    }

        }
       
        public void RemoveRange(int start, int length)
        {
            Children.RemoveRange(start, length);
        }
        
        public void RemoveAll(string name)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.RemoveAll(name);

            Remove(name);
        }
       
        public void RemoveAll(ElementType type)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.RemoveAll(type);

            Remove(type);
        }
       
        public void RemoveAll(Attribute AttributeItem)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.RemoveAll(AttributeItem);

            Remove(AttributeItem);
        }
      
        public void Insert(int index, Element element)
        {
            Children.Insert(index, element);
        }
      
        public void InsertRange(int start, IEnumerable<Element> input)
        {
            Children.InsertRange(start, input);
        }
       
        public List<Element> GetDirectChildrenByElementType(ElementType type)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
                if (item.ElementType == type)
                    ls.Add(item);
            return ls;
        }
        
        public List<Element> GetDirectChildrenByAttirbute(Attribute AttributeItem)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
            {
                if (item.Attributes == null)
                    continue;
                if (item.Attributes.GetAttribute(AttributeItem) == null)
                    continue;
                ls.Add(item);
            }
            return ls;
        }
     

        public List<Element> GetDirectChildrenByAttirbuteKey(string key)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
            {
                if (item.Attributes == null)
                    continue;
                if (item.Attributes.Contains(key))
                    ls.Add(item);
            }
            return ls;
        }
      
        public List<Element> GetDirectChildrenByName(string name)
        {
            var ls = new List<Element>();

            foreach (var item in Children)
                if (item.Name == name)
                    ls.Add(item);

            return ls;
        }
        
        public List<Element> GetAllChildrenByElementType(ElementType type)
        {
            var ls = new List<Element>();

            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.GetAllChildrenByElementType(type))
                        ls.Add(item1);

            if (ElementType == type)
                ls.Add(this);
            return ls;
        }
        
        public List<Element> GetAllChildrenByAttirbute(Attribute AttributeItem)
        {
            var ls = new List<Element>();
            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.GetAllChildrenByAttirbute(AttributeItem))
                        ls.Add(item1);

            if (Attributes == null)
                return ls;
            if (Attributes.GetAttribute(AttributeItem) != null)
                ls.Add(this);

            return ls;
        }
        
        public List<Element> GetAllChildrenByAttirbuteKey(string key)
        {
            var ls = new List<Element>();

            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.GetAllChildrenByAttirbuteKey(key)) ls.Add(item1);

            if (Attributes == null)
                return ls;
            if (Attributes.GetAttribute(key) != null)
                ls.Add(this);
            return ls;
        }
       
        public List<Element> GetAllChildrenByName(string name)
        {
            var ls = new List<Element>();

            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.GetAllChildrenByName(name))
                        ls.Add(item1);

            if (Name == name)
                ls.Add(this);
            return ls;
        }

       
        public override string ToString()
        {
            var t = 0;
            var b = this;
            while (b.Parent != null)
            {
                t++;
                b = b.Parent;
            }
            var prefix = repeat("  ", t);
            var sb = new StringBuilder();
            if (SingleOrDouble == SingleOrDouble.Double)
                sb.Append($"{prefix}<{Name} {Attributes.ToString()}>\r\n{prefix + "    "}{InnerText}\r\n");
            else
                sb.Append($"{prefix}<{Name} {Attributes.ToString()}/>\r\n");
            foreach (var item in Children)
                sb.Append(item.ToString());
            if (SingleOrDouble == SingleOrDouble.Double)
                sb.Append($"{prefix}</{Name} >\r\n");
            return sb.ToString();
        }

     
      
        private string repeat(string pattern,int time)
        {
            var t = 0;
            var sb = new StringBuilder();
            while (++t < time)
                sb.Append(pattern);
            return sb.ToString();
        }
    }
}
