using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    class JSONtoDataConverter
    {
        public List<Contact> GetContacts(List<dynamic> list)
        {
            return GetList<Contact>(list);
        }

        public List<Lead> GetLeads(List<dynamic> list)
        {
            return GetList<Lead>(list);
        }

        public List<Task> GetTasks(List<dynamic> list)
        {
            return GetList<Task>(list);
        }

        public List<Note> GetNotes(List<dynamic> list)
        {
            return GetList<Note>(list);
        }

        private List<T> GetList<T>(List<dynamic> list)
        {
            var result = new List<T>();
            foreach (var array in list)
                foreach (var item in array._embedded.items)
                    result.Add(Construct<T>(item));

            return result;
        }

        private T Construct<T>(dynamic item)
        {
            if (typeof(T) == typeof(Contact))
            {
                return CreateContact(item);
            }

            else if (typeof(T) == typeof(Lead))
            {
                return CreateLead(item);
            }

            else if (typeof(T) == typeof(Task))
            {
                return CreateTask(item);
            }

            else if (typeof(T) == typeof(Note))
            {
                return CreateNote(item);
            }

            else
            {
                throw new Exception(typeof(T).ToString() + " is invalid class");
            }

        }

        private Contact CreateContact(dynamic item)
        {
            var contact = new Contact()
            {
                Id = GetInt(item.id),
                CreationDate = GetDateTime(item.created_at),

            };

            return contact;
        }

        private Lead CreateLead(dynamic item)
        {
            throw new NotImplementedException();
        }

        private Task CreateTask(dynamic item)
        {
            throw new NotImplementedException();
        }

        private Note CreateNote(dynamic item)
        {
            throw new NotImplementedException();
        }


        private int GetInt(dynamic val)
        {
            return (int)val;
        }

        private string GetString(dynamic val)
        {
            return (string)val;
        }

        private DateTime GetDateTime(dynamic val)
        {
            var num = (int)val;
            return new DateTime(num);
        }

    }
}
