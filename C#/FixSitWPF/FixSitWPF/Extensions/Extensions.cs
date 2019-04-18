using System.Collections.Generic;
using System.Linq;

namespace FixSitWPF.Extensions
{
    /// <summary>
    /// Extensions for built ins classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a dictionary to json string.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns>json string representing the dictionary</returns>
        public static string ToJson(this Dictionary<string, string> dict)
        {
            IEnumerable<string> entries = dict.Select(d =>
                string.Format('"'+ "{0}"+'"'+":" + '"'+ "{1}" +'"', d.Key, d.Value));
            return "{" + string.Join(",", entries) + "}";
        }
    }
}
