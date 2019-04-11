using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Extensions
{
    public static class Extenstions
    {
        public static string ToJson(this Dictionary<string, string> dict)
        {
            var entries = dict.Select(d =>
                string.Format('"'+ "{0}"+'"'+":" + '"'+ "{1}" +'"', d.Key, d.Value));
            Console.WriteLine("{" + string.Join(",", entries) + "}");
            return "{" + string.Join(",", entries) + "}";
        }
    }
}
