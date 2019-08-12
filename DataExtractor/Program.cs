using System;
using System.IO;



namespace DataExtractor
{
    class Program
    {
        private static string outputFileName = "//Output.xlsx";
        private static string storageFileName = "DataBase//storage.bin";
        private static DataStorage database;


        static void Main(string[] args)
        {
            InitializeDataBase();
            LoadData();

            PrepareToCaclulate();
            StageOne();           

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
