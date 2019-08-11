using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    class Lead : Item, IComparable
    {
        public string Name { get; set; }
        public int ContactId { get; set; }


        public int CompareTo(object obj)
        {
            Lead other = (Lead)obj;
            if (other.CreationDate > CreationDate) return -1;
            else return 1;
        }

    }
}
