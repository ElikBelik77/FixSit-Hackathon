using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Extensions
{
    /// <summary>
    /// Extenstions for bultins classes.
    /// </summary>
    public static class Extenstions
    {
        /// <summary>
        /// Converts a dictionary to json string.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns>json string representing the dictionary</returns>
        public static string ToJson(this Dictionary<string, string> dict)
        {
            var entries = dict.Select(d =>
                string.Format('"'+ "{0}"+'"'+":" + '"'+ "{1}" +'"', d.Key, d.Value));
            Console.WriteLine("{" + string.Join(",", entries) + "}");
            return "{" + string.Join(",", entries) + "}";
        }
    }
}
