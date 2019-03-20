using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSoftware.BLL
{
    public class DictionaryOperation
    {
        public static string GetKey(Dictionary<string, int> dictionary, int value)
        {
            string keyValue = null;

            foreach (var pair in dictionary)
            {
                if (pair.Value == value)
                {
                    keyValue = pair.Key;
                }
            }

            return keyValue;

        }

        public static int GetValue(Dictionary<string, int> dictionary, string name)
        {

            return dictionary[name];
        }
    }
}