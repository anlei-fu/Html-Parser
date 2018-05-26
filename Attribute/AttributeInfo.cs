 public class AttributeInfo :IEnumerable<AttributeItem>
    {
        public AttributeInfo()
        { }
        public AttributeInfo(string text)
        {
           

        }


        public string this[string key]
        {
            get
            {
                var b = GetAttribute(key);
                if (b == null)
                    throw new Exception("Not Caontain This Key!");
                return b.Value;
            }
            set
            {
                var b = GetAttribute(key);
                if (b == null)
                    throw new Exception("Not Caontain This Key!");
                b.Value = value;
            }
        }
        private List<AttributeItem> Attributes = new List<AttributeItem>();

        public AttributeItem GetAttribute(string key)
        {
            foreach (var item in Attributes)
                if (item.Key == key)
                    return item;
            return null;
        }
        public AttributeItem GetAttribute(AttributeItem item)
        {
            var b = GetAttribute(item.Key);
            if (b == null)
                return null;
            if (b.Value != item.Value)
                return null;
            return b;
        }
        /************************************
         * if key already exist old-value=new-value,
         * else  add new item
         * ***********************************/
        public void Add(string key, string value)
        {
            var b = GetAttribute(key);
            if (b != null)
            {
                b.Value = value;
                return;
            }
            Attributes.Add(new AttributeItem(key, value));
        }
        public void Add(AttributeItem item)
        {
            var b = GetAttribute(item.Key);
            if (b != null)
            {
                b.Value = item.Value;
                return;
            }
            Attributes.Add(item);
        }
        public bool Contains(string key)
                    => GetAttribute(key) != null;
        public void Clear()
        {
            Attributes.Clear();
        }
        
        public void Remove(string key)
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                if (Attributes[i].Key != key)
                    continue;
                Attributes.RemoveRange(i, 1);
                return;
            }

        }
        public override string ToString()
        {
            string str = "";
            foreach (var item in Attributes)
                str += item.ToString() + " ";
            return str;
        }

        public IEnumerator<AttributeItem> GetEnumerator()
        {
            foreach (var item in Attributes)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Attributes.GetEnumerator();
        }
    }
