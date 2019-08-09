using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    class Program
    {        
        private static string outputFileName = "//Output.xlsx";

        static void Main(string[] args)
        {
            Authorizator authorizator = new Authorizator();
            var client = authorizator.CreateClient();
            var loader = new DataLoader(client);

            var leads = loader.GetLeadsJSON();
            var contacts = loader.GetContactsJSON();
            var tasks = loader.GetTasksJSON();
            Console.WriteLine("1");

            var notesContacts = loader.GetContactsNotesJSON();
            var notesLeads = loader.GetLeadsNotesJSON();
            var notesTasks = loader.GetTasksNotesJSON();
            Console.WriteLine("2");

            var converter = new JSONtoDataConverter();
            Console.WriteLine("3");
            var storage = new DataStorage();
            Console.WriteLine("4");
            storage.Contacts = converter.GetContacts(contacts);
            //storage.Leads = converter.GetLeads(leads);
            //storage.Tasks = converter.GetTasks(tasks);
            //storage.NoteContacts = converter.GetNotes(notesContacts);
            //storage.NoteLeads = converter.GetNotes(notesLeads);
            //storage.NoteTasks = converter.GetNotes(notesTasks);

            Console.WriteLine(storage.Contacts.Count);

           // Console.WriteLine(json[0]._embedded.items[0].name.ToString());



            Console.Read();

        }









        //static void StageOne()
        //{

        //    var outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + outputFileName;

        //    DataReader reader = new DataReader();
        //    List<Contact> contacts = new List<Contact>();
        //    foreach (var file in contactsFileNames)
        //    {
        //        var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + file;
        //        contacts.AddRange(reader.ReadContacts(filePath));
        //    }
        //    contacts.Sort();

        //    DataHandlerStageOne handler = new DataHandlerStageOne();
        //    var table = handler.GetData(contacts);

        //    DataWriter writer = new DataWriter();
        //    writer.SaveData(table, outputFilePath, "Stage01");
        //}

        //static void StageTwo()
        //{




        //}

    }
}
