using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    class Lead : Item, IComparable
    {
        public string Text { get; set; }
        public int ContactId { get; set; }


        public int CompareTo(object obj)
        {
            Contact other = (Contact)obj;
            if (other.CreationDate > CreationDate) return -1;
            else return 1;
        }

    }
}
