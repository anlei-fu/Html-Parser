   /****************************************************************
     *  半元素
     ******************************************************************/
    public class SemiElement : IName
    {
        public SemiElement()
        { }

        /// <summary>
        /// 属性
        /// </summary>
        public AttributeInfo Attributes { get; set; } = new AttributeInfo();
        /// <summary>
        /// 元素类型 单标签 或者 双标签
        /// </summary>
        public SingleOrDouble SingleOrDouble;
        /// <summary>
        /// 标签属于 起始部分还是 结束部分
        /// </summary>
        public StartOrEnd StartOrEnd { get; set; }
        /// <summary>
        /// 标签 文本
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 标签起始位置
        /// </summary>
        public int Positionstart { get; set; }
        /// <summary>
        /// 标签结束位置
        /// </summary>
        public int Positionend { get; set; }
        /// <summary>
        /// 标签类型
        /// </summary>
        public virtual ElementType ElementType { get; set; }
        /// <summary>
        /// 标签名称 等同于类型
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 通过名字获取元素类型
        /// </summary>
        /// <param name="Name">名字</param>
        /// <returns></returns>
        public static ElementType GetType(string Name)
        {
            switch (Name)
            {
                case "!doctype": return ElementType.doctype;
                case "a": return ElementType.a;
                case "div": return ElementType.div;
                case "span": return ElementType.span;
                case "li": return ElementType.li;
                case "dl": return ElementType.dl;
                case "dt": return ElementType.dt;
                case "dd": return ElementType.dd;
                case "tt": return ElementType.tt;
                case "th": return ElementType.th;
                case "ul": return ElementType.ul;
                case "td": return ElementType.td;
                case "p": return ElementType.p;
                case "h1": return ElementType.h1;
                case "h2": return ElementType.h2;
                case "h3": return ElementType.h3;
                case "br": return ElementType.br;
                case "tr": return ElementType.tr;
                case "style": return ElementType.style;
                case "script": return ElementType.script;
                case "h4": return ElementType.h4;
                case "h5": return ElementType.h5;
                case "h6": return ElementType.h6;
                case "img": return ElementType.img;
                case "tbody": return ElementType.tbody;
                case "table": return ElementType.table;
                case "ol": return ElementType.ol;
                case "iframe": return ElementType.iframe;
                case "input": return ElementType.input;
                case "ins": return ElementType.ins;
                case "title": return ElementType.title;
                case "link": return ElementType.link;
                case "i": return ElementType.i;
                case "meta": return ElementType.meta;
                case "!DOCTYPE": return ElementType.doctype;
                case "!": return ElementType.denote;
                case "body": return ElementType.body;
                case "head": return ElementType.head;
                case "html": return ElementType.html;
                case "font": return ElementType.font;
                case "footer": return ElementType.footer;
                case "form": return ElementType.form;
                case "abbr": return ElementType.abbr;
                case "address": return ElementType.address;
                case "applet": return ElementType.applet;
                case "acronym": return ElementType.acronym;
                case "area": return ElementType.area;
                case "article": return ElementType.article;
                case "aside": return ElementType.aside;
                case "audio": return ElementType.audio;
                case "base": return ElementType.bese;
                case "basefont": return ElementType.basefont;
                case "bdi": return ElementType.bdi;
                case "bdo": return ElementType.bdo;
                case "big": return ElementType.big;
                case "blockquote": return ElementType.blockquote;
                case "button": return ElementType.button;
                case "b": return ElementType.b;
                case "canvas": return ElementType.canvas;
                case "caption": return ElementType.caption;
                case "center": return ElementType.center;
                case "cite": return ElementType.cite;
                case "code": return ElementType.code;
                case "colgroup": return ElementType.colgroup;
                case "col": return ElementType.col;
                case "command": return ElementType.command;
                case "datalist": return ElementType.datalist;
                case "del": return ElementType.del;
                case "details": return ElementType.details;
                case "dfn": return ElementType.dfn;
                case "dir": return ElementType.dir;
                case "embed": return ElementType.embed;
                case "em": return ElementType.em;
                case "fieldset": return ElementType.fieldset;
                case "figcaption": return ElementType.figcaption;
                case "figure": return ElementType.figure;
                case "frameset": return ElementType.frameset;
                case "frame": return ElementType.frame;
                case "header": return ElementType.header;
                case "hgroup": return ElementType.hgroup;
                case "hr": return ElementType.hr;
                case "keygen": return ElementType.keygen;
                case "kbd": return ElementType.kbd;
                case "label": return ElementType.label;
                case "legend": return ElementType.legend;
                case "map": return ElementType.map;
                case "mark": return ElementType.mark;
                case "menu": return ElementType.menu;
                case "meter": return ElementType.meter;
                case "nav": return ElementType.nav;
                case "noframes": return ElementType.noframes;
                case "noscript": return ElementType.noscript;
                case "object": return ElementType.abject;
                case "optgroup": return ElementType.optgroup;
                case "option": return ElementType.option;
                case "output": return ElementType.output;
                case "param": return ElementType.param;
                case "pre": return ElementType.pre;
                case "progress": return ElementType.progress;
                case "q": return ElementType.q;
                case "rp": return ElementType.rp;
                case "ruby": return ElementType.ruby;
                case "samp": return ElementType.samp;
                case "select": return ElementType.select;
                case "small": return ElementType.small;
                case "source": return ElementType.source;
                case "strike": return ElementType.strike;
                case "strong": return ElementType.strong;
                case "sub": return ElementType.sub;
                case "summary": return ElementType.summary;
                case "sup": return ElementType.sup;
                case "s": return ElementType.s;
                case "textarea": return ElementType.textarea;
                case "tfoot": return ElementType.tfoot;
                case "thead": return ElementType.thead;
                case "time": return ElementType.time;
                case "track": return ElementType.track;
                case "u": return ElementType.u;
                case "var": return ElementType.var;
                case "video": return ElementType.video;
                case "wbr": return ElementType.wbr;
                case "rt": return ElementType.rt;
                case "section": return ElementType.section;
                default: return ElementType.unknow;

            }
        }
        /// <summary>
        /// 获取单双
        /// </summary>
        public static SingleOrDouble GetSingleOrDouble(ElementType ElementType)
        {
            switch (ElementType)
            {
                case ElementType.link:
                case ElementType.img:
                case ElementType.denote:
                case ElementType.doctype:
                case ElementType.acronym:
                case ElementType.area:
                case ElementType.bese:
                case ElementType.basefont:
                case ElementType.br:
                case ElementType.embed:
                case ElementType.input:
                case ElementType.meta:
                case ElementType.frame:
                case ElementType.hr:
                case ElementType.keygen:
                case ElementType.source:
                case ElementType.track:
                case ElementType.param:
                    return SingleOrDouble.SINGLE;

                default:
                    return SingleOrDouble.BOUBLE;

            }

        }

    }
