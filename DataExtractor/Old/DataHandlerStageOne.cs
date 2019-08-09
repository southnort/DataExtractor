using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    /// <summary>
    /// Посчитать, сколько было новых клиентов в каждом месяце.
    /// Разделить их по источникам рекламы
    /// </summary>

    class DataHandlerStageOne
    {
        public DataTable GetData(List<Contact> contacts)
        {
            var sources = GetSources(contacts);
            var dates = GetDates(contacts);
            var dictionary = CreateDictionary(sources, dates);
            Calculate(contacts, dictionary, sources, dates);

            return ConvertDictionaryToTable(dictionary, sources, dates);
        }

        private List<string> GetSources(List<Contact> contacts)
        {
            var result = new List<string>();            
            result.Add("Всего");
            foreach (var con in contacts)
                foreach (var sour in con.Sources)
                    if (!result.Contains(sour))
                        result.Add(sour);

            return result;
        }

        private List<string> GetDates(List<Contact> contacts)
        {
            var result = new List<string>();
            result.Add("Источник");
            result.Add("Всего");
            foreach (var con in contacts)
            {
                var date = GetDateHeader(con.CreationDate);
                if (!result.Contains(date))
                    result.Add(date);
            }

            return result;
        }

        private string GetDateHeader(DateTime date)
        {
            return date.ToString("MM.yyyy");
        }


        private Dictionary<DictKey, int> CreateDictionary(List<string> sources, List<string> dates)
        {
            Dictionary<DictKey, int> result = new Dictionary<DictKey, int>();
            foreach (var sour in sources)
                foreach (var date in dates)
                {
                    DictKey key = new DictKey(sour, date);
                    result.Add(key, 0);
                }

            return result;
        }


        private void Calculate(List<Contact> contacts, Dictionary<DictKey, int> dictionary,
            List<string> sources, List<string> dates)
        {
            foreach (var con in contacts)
            {
                var date = GetDateHeader(con.CreationDate);
                DictKey totalKey = new DictKey("Всего", date);
                dictionary[totalKey]++;
                foreach (var source in con.Sources)
                {
                    DictKey key = new DictKey(source, date);
                    dictionary[key]++;
                    totalKey = new DictKey(source, "Всего");
                    dictionary[totalKey]++;
                }

            }

        }





        private DataTable ConvertDictionaryToTable(Dictionary<DictKey, int> dictionary,
            List<string> sources, List<string> dates)
        {
            DataTable table = new DataTable();
            table.Clear();

            foreach (var date in dates)
            {
                table.Columns.Add(date);
            }                       

            foreach (var source in sources)
            {
               var row = table.Rows.Add();
                row[0] = source;
                for (int i = 1; i < dates.Count; i++)
                {
                    var key = new DictKey(source, dates[i]);
                    row[i] = dictionary[key];
                }
            }

            return table;
        }



        private struct DictKey
        {
            public DictKey(string source, string date)
            {
                this.source = source;
                this.date = date;
            }

            public string source;
            public string date;
        }
    }


}
