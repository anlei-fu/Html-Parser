using System;
using System.Collections;
using System.Collections.Generic;

namespace Fal.DataStructure
{
    /*******************************************************
     * For Xml/Html Node atrribute 
     * just as class="btn" ,style="color:red",name="fal"
     * *******************************************************/
    public class AttributeItem
    {
        public AttributeItem()
        {
        }
        public AttributeItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; } = "";
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; } = "";
       /// <summary>
       /// 重写ToString
       /// </summary>
       /// <returns></returns>
        public override string ToString() => $"{Key}=\"{Value}\"";
        /// <summary>
        /// 重写equel
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {

            if (obj is null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            return ((AttributeItem)obj).Key == Key && ((AttributeItem)obj).Value == Value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(AttributeItem item1, AttributeItem item2)
        {
            if (item1 is null)
                return item2 is null;
            return item1.Equals(item2);
        }
        public static bool operator !=(AttributeItem item1, AttributeItem item2) => !(item1 == item2);
    }
