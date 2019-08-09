using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;



namespace DataExtractor
{
    class DataLoader
    {
        private WebClientEx webClient;

        public DataLoader(WebClientEx client)
        {
            webClient = client;
        }


        public List<dynamic> GetContactsJSON()
        {
            string request = @"https://massivpro.amocrm.ru/api/v2/contacts/";
            return GetJSONList(request);
        }

        public List<dynamic> GetLeadsJSON()
        {
            string request = @"https://massivpro.amocrm.ru/api/v2/leads";
            return GetJSONList(request);
        }

        public List<dynamic> GetTasksJSON()
        {
            string request = @"https://massivpro.amocrm.ru/api/v2/tasks";
            return GetJSONList(request);
        }


        public List<dynamic> GetContactsNotesJSON()
        {
            return GetJSONList(
                @"https://massivpro.amocrm.ru/api/v2/notes?type=contact");
        }

        public List<dynamic> GetLeadsNotesJSON()
        {
            return GetJSONList(
                @"https://massivpro.amocrm.ru/api/v2/notes?type=lead");
        }

        public List<dynamic> GetTasksNotesJSON()
        {
            return GetJSONList(
                @"https://massivpro.amocrm.ru/api/v2/notes?type=task");
        }


        private List<dynamic> GetJSONList(string request)
        {
            List<dynamic> resultList = new List<dynamic>();
            bool end = false;
            int iteration = 0;
            while (!end)
            {
                var url = GetUrl(request, iteration);

                var json = GetRawData(url);
                resultList.Add(json);
                if (json._embedded.items.Count < 500)
                    end = true;
                else iteration++;
            }

            return resultList;
        }

        private string GetUrl(string request, int iteration)
        {
            string url;
            if (iteration == 0)
                url = request;
            else
            {
                url = request.Contains("?") ?
                       request + "&limit_rows=500&limit_offset=" + iteration * 500 :
                      request + "?limit_rows=500&limit_offset=" + iteration * 500;
            }


            return url;
        }

        private dynamic GetRawData(string requestString)
        {
            var response = webClient.DownloadString(requestString);
            dynamic obj = JsonConvert.DeserializeObject(response);

            return obj;


        }

    }
}
