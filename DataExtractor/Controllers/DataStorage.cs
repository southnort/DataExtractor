using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace DataExtractor
{
    [Serializable]   
    class DataStorage
    {
        public List<Contact> Contacts;
        public List<Lead> Leads;
        public List<Task> Tasks;

        public List<Note> NoteContacts;
        public List<Note> NoteLeads;
        public List<Note> NoteTasks;

        public DataStorage()
        {
            Contacts = new List<Contact>();
            Leads = new List<Lead>();
            Tasks = new List<Task>();
            NoteContacts = new List<Note>();
            NoteLeads = new List<Note>();
            NoteTasks = new List<Note>();
        }


    }

    class StorageController
    {
        public DataStorage GetStorage(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        return (DataStorage)bf.Deserialize(fs);
                    }
                }
                catch
                {
                    return new DataStorage();
                }
            }

            else return new DataStorage();

        }

        public void SaveStorage(DataStorage storage, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, storage);
            }
        }

        public void DeleteStorageFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
