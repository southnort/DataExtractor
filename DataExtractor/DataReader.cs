using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using System.Globalization;


namespace DataExtractor
{
    class DataReader
    {
        private char[] separator = new char[] { ',' };

        public List<Contact> ReadContacts(string filePath)
        {
            List<Contact> result = new List<Contact>();
            var wb = new XLWorkbook(filePath);
            var ws = wb.Worksheet(1);
            for (int i = 2; i < ws.RowCount(); i++)
            {
                var row = ws.Row(i);
                if (row.IsEmpty()) break;
                else
                {
                    Contact contact = new Contact();
                    contact.Name = row.Cell("B").Value.ToString();
                    contact.CreationDate = DateTime.Parse(row.Cell("D").Value.ToString());
                    contact.Sources = GetSources(row.Cell("AE").Value.ToString());

                    result.Add(contact);
                }

            }

            result.Sort();
            return result;
        }

        private List<string> GetSources(string input)
        {
            List<string> result = new List<string>();
            var array = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in array)
            {
                var temp = str.Replace(" ", "");
                if (!result.Contains(temp))
                    result.Add(temp);
            }

            if (result.Count < 1) result.Add(" - ");
            return result;
        }

    }
}
