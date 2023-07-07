using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuggyDemoWeb.Models
{
    public class CustomerRecord
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; } 
        public int Id { get; set; }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}



