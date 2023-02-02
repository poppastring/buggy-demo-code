using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyDemoConsole.Models
{
    public class User
    {
        public string subject { get; set; }
        public string[] aliases { get; set; }
        public Link[] links { get; set; }
        public person person { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
        public string template { get; set; }
    }


    public class person
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

}
