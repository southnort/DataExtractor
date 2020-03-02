using System;
using System.IO;
using System.Net;


namespace DataExtractor
{
    class Program
    {
        private static string outputFileName = "//Output.xlsx";
        private static string storageFileName = "DataBase//storage.bin";
        private static DataStorage database;


        static void Main(string[] args)
        {
            //InitializeDataBase();
            //LoadData();

            //PrepareToCaclulate();
            //StageOne();           

            using (WebClient client = new WebClient())
            {
                //string htmlCode = client.DownloadString(@"https://www.avito.ru/belgorod");

                string htmlCode = GetRequest();
                System.IO.File.WriteAllText(Environment.GetFolderPath
                    (Environment.SpecialFolder.DesktopDirectory)+
                    "\\outputhtmlcode.html", htmlCode);

                
            }

        }


        private static string GetRequest()
        {
            string url = @"http://zakupki.gov.ru/epz/order/quicksearch/search.html";

            var client = (HttpWebRequest)WebRequest.Create(url);
            client.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            client.CookieContainer = new CookieContainer();
            client.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            var htmlCode = client.GetResponse() as HttpWebResponse;

            return htmlCode.ToString();
        }

        private static void InitializeDataBase()
        {
            StorageController sc = new StorageController();            
            database = sc.GetStorage(storageFileName);
            Console.WriteLine("Database initialized");
        }

        private static void LoadData()
        {
            StorageController sc = new StorageController();
            sc.DeleteStorageFile(storageFileName);

            Authorizator authorizator = new Authorizator();
            var client = authorizator.CreateClient();
            var loader = new DataLoader(client);
            var converter = new JSONtoDataConverter();

            var leads = loader.GetLeadsJSON();
            database.Leads.AddRange(converter.GetLeads(leads));
            leads = null;
            Console.WriteLine("1");

            var contacts = loader.GetContactsJSON();
            database.Contacts.AddRange(converter.GetContacts(contacts));
            contacts = null;
            Console.WriteLine("2");

            var tasks = loader.GetTasksJSON();
            database.Tasks.AddRange(converter.GetTasks(tasks));
            tasks = null;
            Console.WriteLine("3");

            var notes = loader.GetContactsNotesJSON();
            database.NoteContacts.AddRange(converter.GetNotes(notes));
            notes = loader.GetLeadsNotesJSON();
            database.NoteLeads.AddRange(converter.GetNotes(notes));
            notes = loader.GetTasksNotesJSON();
            database.NoteTasks.AddRange(converter.GetNotes(notes));
            notes = null;
            Console.WriteLine("4");
            
            sc.SaveStorage(database, storageFileName);

            Console.WriteLine("Finish");


        }


                     

        static void PrepareToCaclulate()
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + outputFileName;
            if (File.Exists(dir))
                File.Delete(dir);            
        }

        static void StageOne()
        {
            var outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + outputFileName;
                       
            var contacts = database.Contacts;
            contacts.Sort();

            DataHandlerStageOne handler = new DataHandlerStageOne();
            var table = handler.GetData(contacts);

            DataWriter writer = new DataWriter();
            writer.SaveData(table, outputFilePath, "List01");
        }

        static void StageTwo()
        {




        }

    }
}
