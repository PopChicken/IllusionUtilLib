using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUL.Utilities {
    public class LanguageManager {
        public readonly Dictionary<string, string> Text;
        public string ActivatedLang { get; }
        public LanguageManager(string lang) {
            this.ActivatedLang = lang;
        }
    }
}
