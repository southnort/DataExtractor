﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    [Serializable]
    class Contact : Item, IComparable
    {
        public string Name { get; set; }        
        public List<CustomField> CustomFields { get; set; }

        public List<string> Sources { get; set; }

        public int CompareTo(object obj)
        {
            Contact other = (Contact)obj;
            if (other.CreationDate > CreationDate) return -1;
            else return 1;
        }
    }
}
