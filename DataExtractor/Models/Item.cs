﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    [Serializable]
    abstract class Item
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
