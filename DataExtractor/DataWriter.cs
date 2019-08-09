using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClosedXML.Excel;
using System.IO;


namespace DataExtractor
{
    class DataWriter
    {
        public void SaveData(DataTable table, string filePath, string sheetName)
        {
            var book = new XLWorkbook(filePath);            

            var sheet = 
                book.Worksheets.Add(table, sheetName);
            
            book.Save();

        }
    }
}

