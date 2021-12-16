using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyDemoConsole.Models
{
    public class Product
    {
        string name;
        int id;
        char[] details = new char[10000];

        public Product(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
