using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Web;
using System.Net.Http;


namespace DataExtractor
{
    class Authorizator
    {
        private string url = @"https://massivpro.amocrm.ru/private/api/auth.php";

        private string login = "416-200@mail.ru";
        private string apiKey = "004b1f78f44599900f5b1b532795cd4f96d92c60";



        public WebClientEx CreateClient()
        {
            using (var client = new WebClientEx())
            {
                var data = new NameValueCollection
            {
                {"USER_LOGIN",login },
                {"USER_HASH",apiKey },
            };

                var response = client.UploadValues(url, data);               
                return client;
            }



        }



        //    //    var uri = new Uri(url);
        //    //CookieContainer container = new CookieContainer();

        //    //HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
        //    //webReq.Method = "POST";
        //    //webReq.CookieContainer = container;


        //    //string data = $"USER_LOGIN={login}&USER_HASH={apiKey}";
        //    //byte[] buffer = System.Text.Encoding.UTF32.GetBytes(data);
        //    //webReq.ContentType = "application/x-www-form-urlencoded";
        //    //webReq.ContentLength = buffer.Length;


        //    //Stream PostData = webReq.GetRequestStream();
        //    //PostData.Write(buffer, 0, buffer.Length);
        //    //PostData.Close();

        //    //HttpWebResponse WebResp = (HttpWebResponse)webReq.GetResponse();
        //    //Stream Answer = WebResp.GetResponseStream();
        //    //StreamReader _Answer = new StreamReader(Answer);
        //    //WebResp.Close();

        //    //return webReq.CookieContainer;

        //    //Console.WriteLine(response.ToString());



        //    //Console.WriteLine(container.GetCookies(uri).Count.ToString());
        //    //Console.Read();



    }
}
