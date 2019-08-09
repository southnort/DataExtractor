using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    class Contact : IComparable
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string> Sources { get; set; }

        public int CompareTo(object obj)
        {
            Contact other = (Contact)obj;
            if (other.CreationDate > CreationDate) return -1;
            else return 1;
        }
    }
}
