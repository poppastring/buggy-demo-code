using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyDemoConsole.Models
{
    public class Foo
    {
        public Bar Bar { get; set; } = new Bar();
    }

    public class Bar
    {
        public Baz Baz { get; set; }
    }

    public class Baz
    {
        public string Name { get; set; } = "Barry";
    }
}
