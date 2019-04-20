using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Parsers.Html
{
    /// <summary>
    /// entity of  html element  attributes
    /// </summary>
    public class AtrrributeCollection : IReadOnlyCollection<Attribute>
    {
        public AtrrributeCollection()
        {
        }

        public string this[string key]
        {
            get
            {
                var b = GetAttribute(key);

                if (b == null)
                    throw new InvalidOperationException("attribute is not found!");

                return b.Value;
            }
            set
            {
                var b = GetAttribute(key);

                if (b == null)
                    throw new InvalidOperationException("attribute is not found!");

                b.Value = value;
            }
        }
        private List<Attribute> _attributes = new List<Attribute>();

        public int Count => _attributes.Count;

        public Attribute GetAttribute(string key)
        {
            foreach (var item in _attributes)
            {
                if (item.Key == key)
                    return item;
            }

            return null;
        }
        public Attribute GetAttribute(Attribute item)
        {
            var b = GetAttribute(item.Key);

            return b?.Value != item.Value ? null : b;
        }

        public void Add(string key, string value)
        {
            var b = GetAttribute(key);

            if (b != null)
            {
                b.Value = value;

                return;
            }

            _attributes.Add(new Attribute(key, value));
        }
        public void Add(Attribute item)
        {
            var b = GetAttribute(item.Key);

            if (b != null)
            {
                b.Value = item.Value;

                return;
            }

            _attributes.Add(item);
        }
        public bool Contains(string key)
                    => GetAttribute(key) != null;
        public void Clear()
        {
            _attributes.Clear();
        }

        public void Remove(string key)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Key != key)
                    continue;

                _attributes.RemoveRange(i, 1);

                return;
            }

        }
        public override string ToString()
        {
            var ret = "";

            foreach (var item in _attributes)
                ret += item.ToString() + " ";

            return ret;
        }

        public IEnumerator<Attribute> GetEnumerator()
        {
            foreach (var item in _attributes)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _attributes.GetEnumerator();
        }
    }
    /// <summary>
    /// enterty of html attribute 
    /// </summary>
    public class Attribute
    {
        public Attribute()
        {
        }
        public Attribute(string key, string value)
        {
            Key = key;
            Value = value;
        }
     
        public string Key { get; set; } = "";
       
        public string Value { get; set; } = "";
       
        public override string ToString() => $"{Key}=\"{Value}\"";
       
        public override bool Equals(object obj)
        {

            if (obj is null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return ((Attribute)obj).Key == Key && ((Attribute)obj).Value == Value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Attribute item1, Attribute item2)
        {
            if (item1 is null)
                return item2 is null;

            return item1.Equals(item2);
        }

        public static bool operator !=(Attribute item1, Attribute item2) => !(item1 == item2);
    }
   
}
