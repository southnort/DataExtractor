using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    class CustomField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<CustomFieldValue> Values { get; set; }

    }

    class CustomFieldValue
    {
        public string v_value { get; set; }
        public int v_enum { get; set; }

    }
}
