﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    abstract class Note : Item
    {
        public int Element_Id { get; set; }
        public int Note_Type { get; set; }
        public string Text { get; set; }
    }
}
