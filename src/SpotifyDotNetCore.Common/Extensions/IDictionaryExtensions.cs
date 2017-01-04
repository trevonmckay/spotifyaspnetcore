using System.Collections.Generic;
using System.Text;

namespace SpotifyDotNetCore
{
    public static class IDictionaryExtensions
    {
        public static string ToQueryString(this IDictionary<string, string> self)
        {
            if(self.Keys.Count == 0)
                return string.Empty;
            
            var sb = new StringBuilder();
            foreach(string key in self.Keys)
            {
                if(sb.Length > 0)
                    sb.Append("&");
                
                string val = null;
                self.TryGetValue(key, out val);

                string encValue = (val == null) ? string.Empty : System.Net.WebUtility.HtmlEncode(val);

                sb.AppendFormat("{0}={1}", key, encValue);
            }

            return "?" + sb.ToString();
        }
    }
}