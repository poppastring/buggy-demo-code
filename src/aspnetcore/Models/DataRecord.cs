using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuggyDemoWeb.Models
{
    public class DataRecord
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }

        public string State { get; set; }

        public List<DataRecord> MyList { get { return _instances; } }

        public int TotalCount { get { return _instances.Count; } }

        private static List<DataRecord> _instances = new List<DataRecord>();

        private byte[] internaldata = new byte[8000];

        public DataRecord()
        {
            _instances.Add(this);

            for (int i = 0; i < internaldata.Length; i++)
            {
                internaldata[i] = 0x20;
            }
        }
    }
}
