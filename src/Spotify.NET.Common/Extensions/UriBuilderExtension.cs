using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Spotify.NET.Common
{
    internal static class UriBuilderExtensions
    {
        private static IEnumerable<KeyValuePair<string, string>> GetQueryStringAsNameValuePair(this UriBuilder self)
        {
            if (string.IsNullOrWhiteSpace(self.Query))
                return null;

            string s = self.Query;
            var collection = new List<KeyValuePair<string, string>>();

            // remove anything other than query string from url
            if (s.Contains("?"))
            {
                s = s.Substring(s.IndexOf('?') + 1);
            }

            foreach (string kvPair in Regex.Split(s, "&"))
            {
                string[] singlePair = Regex.Split(kvPair, "=");
                if (singlePair.Length == 2)
                {
                    collection.Add(new KeyValuePair<string, string>(singlePair[0], singlePair[1]));
                }
                else
                {
                    // only one key with no value specified in query string
                    collection.Add(new KeyValuePair<string, string>(singlePair[0], string.Empty));
                }
            }

            return collection;
        }
        internal static void AddParameter(this UriBuilder self, string name, string value, bool urlEncodeValue = true, bool urlEncodeKey = true)
        {
            var _nameValueCollection = self.GetQueryStringAsNameValuePair();
            var nameValueCollection = new List<KeyValuePair<string, string>>();
            if(_nameValueCollection != null)
            {
                nameValueCollection.AddRange(_nameValueCollection);
            }
            nameValueCollection.Add(new KeyValuePair<string, string>(name, value));

            if (nameValueCollection.Count == 0)
            {
                self.Query = String.Empty;
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var kvPair in nameValueCollection)
                {
                    string _key = (urlEncodeKey) ? WebUtility.UrlEncode(kvPair.Key) : kvPair.Key;
                    string _value = (urlEncodeValue) ? WebUtility.UrlEncode(kvPair.Value) : kvPair.Value;
                    string val = (_key != null) ? (_key + "=") : string.Empty;

                    if(sb.Length > 0)
                        sb.Append("&");
                    
                    sb.Append(val);
                    sb.Append(_value);
                }

                self.Query = sb.ToString();
            }
        }
    }
}