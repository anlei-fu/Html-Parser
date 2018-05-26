  /******************************************
     * dom 标签的类
     * 包含了标签的基本属性和一些增删改查的方法
     *Design by fuanlei,all rights reserverd
     * e-mail:18108342263@163.com
     *******************************************/
    public class Element : SemiElement, IComparable<Element>
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
        public void Remove(ElementType type)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].ElementType == type)
                {
                    Children.RemoveAt(i);
                    --i;
                }

        }
        /// <summary>
        /// 通过属性项，移除直接子元素
        /// </summary>
        /// <param name="AttributeItem">属性项</param>
        public void Remove(AttributeItem AttributeItem)
        {
            for (int i = 0; i < Count; i++)
                if (Children[i].Attributes.Contains(AttributeItem.Key))
                    if (Children[i].Attributes[AttributeItem.Key] == AttributeItem.Value)
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
        public void RemoveRange(int start, int length)
        {
            Children.RemoveRange(start, length);
        }
        /// <summary>
        /// 通过名字移除所有子节点
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAll(string name)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.RemoveAll(name);

            Remove(name);
        }
        /// <summary>
        /// 通过元素类型，移除所有直接点
        /// </summary>
        /// <param name="type"></param>
        public void RemoveAll(ElementType type)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.RemoveAll(type);

            Remove(type);
        }
        /// <summary>
        /// 通过属性项，移除所有子节点
        /// </summary>
        /// <param name="AttributeItem"></param>
        public void RemoveAll(AttributeItem AttributeItem)
        {
            if (Children.Count != 0)
                foreach (var item in Children)
                    item.RemoveAll(AttributeItem);

            Remove(AttributeItem);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="element">插入的元素</param>
        public void Insert(int index, Element element)
        {
            Children.Insert(index, element);
        }
        /// <summary>
        /// 插入范围
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="input">要插入的元素</param>
        public void InsertRange(int start, IEnumerable<Element> input)
        {
            Children.InsertRange(start, input);
        }
        /// 通过元素类型， 访问直接子节点
        /// </summary>
        /// <param name="type">元素类型</param>
        /// <returns></returns>
        public List<Element> GetDirectChildrenByElementType(ElementType type)
        {
            var ls = new List<Element>();
            foreach (var item in Children)
                if (item.ElementType == type)
                    ls.Add(item);
            return ls;
        }
        /// <summary>
        ///  通过属性项， 访问直接子节点
        /// </summary>
        /// <param name="AttributeItem">属性项</param>
        /// <returns></returns>
        public List<Element> GetDirectChildrenByAttirbute(AttributeItem AttributeItem)
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
        /// <summary>
        /// 通过包含属性键，访问直接子节点
        /// </summary>
        /// <param name="key">属性键</param>
        /// <returns></returns>
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
        /// <summary>
        /// 通过name 访问直接子节点
        /// </summary>
        /// <param name="name">元素名</param>
        /// <returns></returns>
        public List<Element> GetDirectChildrenByName(string name)
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
        /// <summary>
        /// 通过属性项，获取所有子节点
        /// </summary>
        /// <param name="AttributeItem">属性项</param>
        /// <returns></returns>
        public List<Element> GetAllChildrenByAttirbute(AttributeItem AttributeItem)
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
        /// <summary>
        /// 通过属性键，获取所有子元素
        /// </summary>
        /// <param name="key">属性键</param>
        /// <returns></returns>
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
        /// <summary>
        /// 通过元素名，获取所有子元素
        /// </summary>
        /// <param name="name">元素名</param>
        /// <returns></returns>
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
            if (SingleOrDouble == SingleOrDouble.BOUBLE)
                sb.Append($"{prefix}<{Name} {Attributes.ToString()}>\r\n{prefix + "    "}{InnerText}\r\n");
            else
                sb.Append($"{prefix}<{Name} {Attributes.ToString()}/>\r\n");
            foreach (var item in Children)
                sb.Append(item.ToString());
            if (SingleOrDouble == SingleOrDouble.BOUBLE)
                sb.Append($"{prefix}</{Name} >\r\n");
            return sb.ToString();
        }

        /********************
         * 重写compareTo（）
         * ***************************/
        public int CompareTo(Element other)
        {
            return Positionend.CompareTo(other.Positionend);
        }
    }
