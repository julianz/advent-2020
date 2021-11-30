using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent {
    public class Config {
        public int DefaultYear { get; set; } = 2021;
        public string SessionCookie { get; set; } = "";
        public string ApplicationDirectory { get; set; } = "";
        public string InputDirectory { get; set; } = "Input";
    }
}
