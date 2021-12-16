using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class PresetElement : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public PresetElement()
        {
            Name = "";
            Params = new Dictionary<string, string>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public static string ToPrettyString(Dictionary<string, string> dict)
        {
            var str = new StringBuilder();
            foreach (var pair in dict)
            {
                str.Append(String.Format("{0}: {1}\n", pair.Key, pair.Value));
            }
            return str.ToString().Substring(0, str.Length - 1);
        }
    }
}
