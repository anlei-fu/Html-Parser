using Fal.Fal_Exception;
using Fal.Nlp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fal.DataStructure.Tree
{
    /****************************************************************
     *  半元素
     ******************************************************************/
    public class Semi_Element : IName
    {
        public Semi_Element()
        { }

        /// <summary>
        /// 属性
        /// </summary>
        public Attribute_Info Attributes { get; set; } = new Attribute_Info();
        /// <summary>
        /// 元素类型 单标签 或者 双标签
        /// </summary>
        public Single_Or_Double Single_Or_Double;
        /// <summary>
        /// 标签属于 起始部分还是 结束部分
        /// </summary>
        public Start_Or_End Start_Or_End { get; set; }
        /// <summary>
        /// 标签 文本
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 标签起始位置
        /// </summary>
        public int Position_start { get; set; }
        /// <summary>
        /// 标签结束位置
        /// </summary>
        public int Position_end { get; set; }
        /// <summary>
        /// 标签类型
        /// </summary>
        public virtual Element_Type Element_Type { get; set; }
        /// <summary>
        /// 标签名称 等同于类型
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 通过名字获取元素类型
        /// </summary>
        /// <param name="Name">名字</param>
        /// <returns></returns>
        public static Element_Type get_Type(string Name)
        {
            switch (Name)
            {
                case "!doctype": return Element_Type.doctype;
                case "a": return Element_Type.a;
                case "div": return Element_Type.div;
                case "span": return Element_Type.span;
                case "li": return Element_Type.li;
                case "dl": return Element_Type.dl;
                case "dt": return Element_Type.dt;
                case "dd": return Element_Type.dd;
                case "tt": return Element_Type.tt;
                case "th": return Element_Type.th;
                case "ul": return Element_Type.ul;
                case "td": return Element_Type.td;
                case "p": return Element_Type.p;
                case "h1": return Element_Type.h1;
                case "h2": return Element_Type.h2;
                case "h3": return Element_Type.h3;
                case "br": return Element_Type.br;
                case "tr": return Element_Type.tr;
                case "style": return Element_Type.style;
                case "script": return Element_Type.script;
                case "h4": return Element_Type.h4;
                case "h5": return Element_Type.h5;
                case "h6": return Element_Type.h6;
                case "img": return Element_Type.img;
                case "tbody": return Element_Type.tbody;
                case "table": return Element_Type.table;
                case "ol": return Element_Type.ol;
                case "iframe": return Element_Type.iframe;
                case "input": return Element_Type.input;
                case "ins": return Element_Type.ins;
                case "title": return Element_Type.title;
                case "link": return Element_Type.link;
                case "i": return Element_Type.i;
                case "meta": return Element_Type.meta;
                case "!DOCTYPE": return Element_Type.doctype;
                case "!": return Element_Type.denote;
                case "body": return Element_Type.body;
                case "head": return Element_Type.head;
                case "html": return Element_Type.html;
                case "font": return Element_Type.font;
                case "footer": return Element_Type.footer;
                case "form": return Element_Type.form;
                case "abbr": return Element_Type.abbr;
                case "address": return Element_Type.address;
                case "applet": return Element_Type.applet;
                case "acronym": return Element_Type.acronym;
                case "area": return Element_Type.area;
                case "article": return Element_Type.article;
                case "aside": return Element_Type.aside;
                case "audio": return Element_Type.audio;
                case "base": return Element_Type.bese;
                case "basefont": return Element_Type.basefont;
                case "bdi": return Element_Type.bdi;
                case "bdo": return Element_Type.bdo;
                case "big": return Element_Type.big;
                case "blockquote": return Element_Type.blockquote;
                case "button": return Element_Type.button;
                case "b": return Element_Type.b;
                case "canvas": return Element_Type.canvas;
                case "caption": return Element_Type.caption;
                case "center": return Element_Type.center;
                case "cite": return Element_Type.cite;
                case "code": return Element_Type.code;
                case "colgroup": return Element_Type.colgroup;
                case "col": return Element_Type.col;
                case "command": return Element_Type.command;
                case "datalist": return Element_Type.datalist;
                case "del": return Element_Type.del;
                case "details": return Element_Type.details;
                case "dfn": return Element_Type.dfn;
                case "dir": return Element_Type.dir;
                case "embed": return Element_Type.embed;
                case "em": return Element_Type.em;
                case "fieldset": return Element_Type.fieldset;
                case "figcaption": return Element_Type.figcaption;
                case "figure": return Element_Type.figure;
                case "frameset": return Element_Type.frameset;
                case "frame": return Element_Type.frame;
                case "header": return Element_Type.header;
                case "hgroup": return Element_Type.hgroup;
                case "hr": return Element_Type.hr;
                case "keygen": return Element_Type.keygen;
                case "kbd": return Element_Type.kbd;
                case "label": return Element_Type.label;
                case "legend": return Element_Type.legend;
                case "map": return Element_Type.map;
                case "mark": return Element_Type.mark;
                case "menu": return Element_Type.menu;
                case "meter": return Element_Type.meter;
                case "nav": return Element_Type.nav;
                case "noframes": return Element_Type.noframes;
                case "noscript": return Element_Type.noscript;
                case "object": return Element_Type.abject;
                case "optgroup": return Element_Type.optgroup;
                case "option": return Element_Type.option;
                case "output": return Element_Type.output;
                case "param": return Element_Type.param;
                case "pre": return Element_Type.pre;
                case "progress": return Element_Type.progress;
                case "q": return Element_Type.q;
                case "rp": return Element_Type.rp;
                case "ruby": return Element_Type.ruby;
                case "samp": return Element_Type.samp;
                case "select": return Element_Type.select;
                case "small": return Element_Type.small;
                case "source": return Element_Type.source;
                case "strike": return Element_Type.strike;
                case "strong": return Element_Type.strong;
                case "sub": return Element_Type.sub;
                case "summary": return Element_Type.summary;
                case "sup": return Element_Type.sup;
                case "s": return Element_Type.s;
                case "textarea": return Element_Type.textarea;
                case "tfoot": return Element_Type.tfoot;
                case "thead": return Element_Type.thead;
                case "time": return Element_Type.time;
                case "track": return Element_Type.track;
                case "u": return Element_Type.u;
                case "var": return Element_Type.var;
                case "video": return Element_Type.video;
                case "wbr": return Element_Type.wbr;
                case "rt": return Element_Type.rt;
                case "section": return Element_Type.section;
                default: return Element_Type.unknow;

            }
        }
        /// <summary>
        /// 获取单双
        /// </summary>
        public static Single_Or_Double get_Single_Or_Double(Element_Type Element_Type)
        {
            switch (Element_Type)
            {
                case Element_Type.link:
                case Element_Type.img:
                case Element_Type.denote:
                case Element_Type.doctype:
                case Element_Type.acronym:
                case Element_Type.area:
                case Element_Type.bese:
                case Element_Type.basefont:
                case Element_Type.br:
                case Element_Type.embed:
                case Element_Type.input:
                case Element_Type.meta:
                case Element_Type.frame:
                case Element_Type.hr:
                case Element_Type.keygen:
                case Element_Type.source:
                case Element_Type.track:
                case Element_Type.param:
                    return Single_Or_Double.SINGLE;

                default:
                    return Single_Or_Double.BOUBLE;

            }

        }

    }
    /******************************************
     * dom 标签的类
     * 包含了标签的基本属性和一些增删改查的方法
     *Design by fuanlei,all rights reserverd
     * e-mail:18108342263@163.com
     *******************************************/
    public class Element : Semi_Element, IComparable<Element>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Element()
        {

        }
        /// <summary>
        /// 上一个元素
        /// </summary>
        public Element Previous { get; set; }
        /// <summary>
        /// 下一个元素
        /// </summary>
        public Element Next { get; set; }
        /// <summary>
        /// 是否具有上一个元素
        /// </summary>
        public bool HasPrevious => Previous != null;
        /// <summary>
        /// 是否具有下一个元素
        /// </summary>
        public bool HasNext => Next != null;
        /// <summary>
        /// 是否含有子元素
        /// </summary>
        public bool HasChildren => Children.Count != 0;
        /// <summary>
        /// 通过index访问子元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Element this[int index]
        {
            get
            {
                if (index > Children.Count - 1)
                    Exception_Thrower.Throw_OutOfRange(index);
                return Children[index];
            }
        }
        /// <summary>
        /// 子元素个数
        /// </summary>
        public int Count { get => Children.Count; }
        /// <summary>
        /// 内容
        /// </summary>
        public string InnerHtml { get; set; } = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string InnerText { get; set; } = "";
        /// <summary>
        /// 子元素
        /// </summary>
        public List<Element> Children { get; set; } = new List<Element>();
        /// <summary>
        /// 父元素
        /// </summary>
        public Element Father { get; set; }

        /// <summary>
        /// 增加直接子元素
        /// </summary>
        /// <param name="element">要增加的子元素</param>
        public void Add(Element element)
        {
            Children.Add(element);
            element.Father = this;
        }
        /// <summary>
        /// 增加直接子元素
        /// </summary>
        /// <param name="input">要增加的子元素</param>
        public void Add_Range(IEnumerable<Element> input)
        {
            foreach (var item in input)
                Add(item);
        }

        public void Remove(int index)
        {
            Exception_Thrower.Checkthrow_OutOfRange(0, Count, index);
            Children.RemoveAt(index);
        }
        /// <summary>
        /// 通过元素名，移除直接子元素
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].Name == name)
                {
                    Children.RemoveAt(i);
                    --i;
                }
        }
        /// <summary>
        /// 通过元素类型移除直接子元素
        /// </summary>
        /// <param name="type">元素类型</param>
        public void Remove(Element_Type type)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].Element_Type == type)
                {
                    Children.RemoveAt(i);
                    --i;
                }

        }
        /// <summary>
        /// 通过属性项，移除直接子元素
        /// </summary>
        /// <param name="attribute_item">属性项</param>
        public void Remove(Attribute_Item attribute_item)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].Attributes.Contains_Key(attribute_item.Key))
                    if (Children[i].Attributes[attribute_item.Key] == attribute_item.Value)
                    {
                        Children.RemoveAt(i);
                        --i;
                    }

        }
        /// <summary>
        /// 移除直接子元素
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="length">长度</param>
        public void Remove_Range(int start, int length)
        {
            Exception_Thrower.Checkthrow_OutOfRange(0, Count, start + length);
            Children.RemoveRange(start, length);
        }
        /// <summary>
        /// 通过名字移除所有子节点
        /// </summary>
        /// <param name="name"></param>
        public void Remove_All(string name)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.Remove_All(name);

            Remove(name);
        }
        /// <summary>
        /// 通过元素类型，移除所有直接点
        /// </summary>
        /// <param name="type"></param>
        public void Remove_All(Element_Type type)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.Remove_All(type);

            Remove(type);
        }
        /// <summary>
        /// 通过属性项，移除所有子节点
        /// </summary>
        /// <param name="attribute_item"></param>
        public void Remove_All(Attribute_Item attribute_item)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.Remove_All(attribute_item);

            Remove(attribute_item);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="element">插入的元素</param>
        public void Insert(int index, Element element)
        {
            Exception_Thrower.Checkthrow_OutOfRange(0, Count, index);
            Children.Insert(index, element);
        }
        /// <summary>
        /// 插入范围
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="input">要插入的元素</param>
        public void Insert_Range(int start, IEnumerable<Element> input)
        {
            Exception_Thrower.Checkthrow_OutOfRange(0, Count, start);
            Children.InsertRange(start, input);
        }
        /// 通过元素类型， 访问直接子节点
        /// </summary>
        /// <param name="type">元素类型</param>
        /// <returns></returns>
        public List<Element> Get_Direct_Children_By_Element_Type(Element_Type type)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
                if (item.Element_Type == type)
                    ls.Add(item);
            return ls;
        }
        /// <summary>
        ///  通过属性项， 访问直接子节点
        /// </summary>
        /// <param name="attribute_item">属性项</param>
        /// <returns></returns>
        public List<Element> Get_Direct_Children_By_Attirbute(Attribute_Item attribute_item)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
            {
                if (item.Attributes == null)
                    continue;
                if (item.Attributes.Get_Attribute(attribute_item) == null)
                    continue;
                ls.Add(item);
            }
            return ls;
        }
        /// <summary>
        /// 通过包含属性键，访问直接子节点
        /// </summary>
        /// <param name="key">属性键</param>
        /// <returns></returns>
        public List<Element> Get_Direct_Children_By_Contain_Attirbute_Key(string key)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
            {
                if (item.Attributes == null)
                    continue;
                if (item.Attributes.Contains_Key(key))
                    ls.Add(item);
            }
            return ls;
        }
        /// <summary>
        /// 通过name 访问直接子节点
        /// </summary>
        /// <param name="name">元素名</param>
        /// <returns></returns>
        public List<Element> Get_Direct_Children_Name(string name)
        {
            var ls = new List<Element>();

            foreach (var item in Children)
                if (item.Name == name)
                    ls.Add(item);

            return ls;
        }
        /// <summary>
        ///通过元素类型， 获取所有子节点
        /// </summary>
        /// <param name="type">元素类型</param>
        /// <returns></returns>
        public List<Element> Get_All_Children_By_Element_Type(Element_Type type)
        {
            var ls = new List<Element>();
            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.Get_All_Children_By_Element_Type(type))
                        ls.Add(item1);

            if (Element_Type == type)
                ls.Add(this);
            return ls;
        }
        /// <summary>
        /// 通过属性项，获取所有子节点
        /// </summary>
        /// <param name="attribute_item">属性项</param>
        /// <returns></returns>
        public List<Element> Get_All_Children_By_Attirbute(Attribute_Item attribute_item)
        {
            var ls = new List<Element>();
            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.Get_All_Children_By_Attirbute(attribute_item))
                        ls.Add(item1);

            if (Attributes == null)
                return ls;
            if (Attributes.Get_Attribute(attribute_item) != null)
                ls.Add(this);

            return ls;
        }
        /// <summary>
        /// 通过属性键，获取所有子元素
        /// </summary>
        /// <param name="key">属性键</param>
        /// <returns></returns>
        public List<Element> Get_All_Children_By_Contain_Attirbute_Key(string key)
        {
            var ls = new List<Element>();

            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.Get_All_Children_By_Contain_Attirbute_Key(key)) ls.Add(item1);

            if (Attributes == null)
                return ls;
            if (Attributes.Get_Attribute(key) != null)
                ls.Add(this);
            return ls;
        }
        /// <summary>
        /// 通过元素名，获取所有子元素
        /// </summary>
        /// <param name="name">元素名</param>
        /// <returns></returns>
        public List<Element> Get_All_Children_By_Name(string name)
        {
            var ls = new List<Element>();

            if (Children.Count != 0)
                foreach (var item in Children)
                    foreach (var item1 in item.Get_All_Children_By_Name(name))
                        ls.Add(item1);

            if (Name == name)
                ls.Add(this);
            return ls;
        }

        /// <summary>
        /// 重写tostring（），并进行了格式化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var t = 0;
            var b = this;
            while (b.Father != null)
            {
                t++;
                b = b.Father;
            }
            var prefix = StringHelper.Repeat("  ", t);
            var sb = new StringBuilder();
            if (Single_Or_Double == Single_Or_Double.BOUBLE)
                sb.Append($"{prefix}<{Name} {Attributes.ToString()}>\r\n{prefix + "    "}{InnerText}\r\n");
            else
                sb.Append($"{prefix}<{Name} {Attributes.ToString()}/>\r\n");
            foreach (var item in Children)
                sb.Append(item.ToString());
            if (Single_Or_Double == Single_Or_Double.BOUBLE)
                sb.Append($"{prefix}</{Name} >\r\n");
            return sb.ToString();
        }

        /********************
         * 重写compareTo（）
         * ***************************/
        public int CompareTo(Element other)
        {
            return Position_end.CompareTo(other.Position_end);
        }
    }
}
