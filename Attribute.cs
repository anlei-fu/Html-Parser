 using Fal.Fal_Exception;
using Fal.Nlp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Fal.DataStructure
{
    /*******************************************************
     * For Xml/Html Node atrribute 
     * just as class="btn" ,style="color:red",name="fal"
     * *******************************************************/
    public class Attribute_Item : IFrom_String
    {
        public Attribute_Item()
        {
        }
        public Attribute_Item(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public void From_String(string str)
        {
            var b = StringHelper.Splite(str, "=");
            if (b.Count != 2)
                return;
            Key = b[0].Substring(0, b[0].Length - 1);
            Value = b[1].Substring(1, b[1].Length - 3);
        }
        public override string ToString() => $"{Key}=\"{Value}\"";
        public override bool Equals(object obj)
        {

            if (obj is null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            return ((Attribute_Item)obj).Key == Key && ((Attribute_Item)obj).Value == Value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Attribute_Item item1, Attribute_Item item2)
        {
            if (item1 is null)
                return item2 is null;
            return item1.Equals(item2);
        }
        public static bool operator !=(Attribute_Item item1, Attribute_Item item2) => !(item1 == item2);
    }
    public class Attribute_Info : IFrom_String,IEnumerable<Attribute_Item>
    {
        public Attribute_Info()
        { }
        public Attribute_Info(string text)
        {
            From_String(text);

        }


        public string this[string key]
        {
            get
            {
                var b = Get_Attribute(key);
                if (b == null)
                    throw new Exception("Not Caontain This Key!");
                return b.Value;
            }
            set
            {
                var b = Get_Attribute(key);
                if (b == null)
                    throw new Exception("Not Caontain This Key!");
                b.Value = value;
            }
        }
        private List<Attribute_Item> Attributes = new List<Attribute_Item>();

        public Attribute_Item Get_Attribute(string str)
        {
            foreach (var item in Attributes)
                if (item.Key == str)
                    return item;
            return null;
        }
        public Attribute_Item Get_Attribute(Attribute_Item item)
        {
            var b = Get_Attribute(item.Key);
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
            var b = Get_Attribute(key);
            if (b != null)
            {
                b.Value = value;
                return;
            }
            Attributes.Add(new Attribute_Item(key, value));
        }
        public void Add(Attribute_Item item)
        {
            var b = Get_Attribute(item.Key);
            if (b != null)
            {
                b.Value = item.Value;
                return;
            }
            Attributes.Add(item);
        }
        public bool Contains_Key(string key)
                    => Get_Attribute(key) != null;
        public void Clear()
        {
            Attributes.Clear();
        }
        /************************************************************************
        *this is main method .
        * resolving a string,to initai member.
        * the string must look like ' name=""', splite by blank,and "" is necceesiry
        ***********************************************************************/
        public void From_String(string text)
        {
            if (string.IsNullOrEmpty(text)) Exception_Thrower.Throw_argNull(text);
            var t = 0;
            while (true)
            {
                t = text.IndexOf("=");
                if (t == -1) return;
                var item = new Attribute_Item();
                item.Key = text.Substring(0, t).Trim();
                text = text.Remove(0, t + 1);
                t = text.IndexOf("\"");
                if (t == -1) return;
                text = text.Substring(t + 1, text.Length - t - 1);
                t = t = text.IndexOf("\"");
                if (t == -1) return;
                item.Value = text.Substring(0, t).Trim();
                Add(item);
                if (t + 1 > text.Length) return;
                text = text.Substring(t + 1, text.Length - t - 1);
            }
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

        public IEnumerator<Attribute_Item> GetEnumerator()
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
}
