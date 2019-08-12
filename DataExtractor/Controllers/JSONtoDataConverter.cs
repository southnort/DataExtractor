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
                Name = GetString(item.name),
                CustomFields = GetCustomFields(item.custom_fields),
                Sources = new List<string>(),
            };

            foreach (var field in contact.CustomFields)
            {
                if (field.Name == "Источник рекламы")
                {
                    foreach (var val in field.Values)
                        contact.Sources.Add(val.v_value);

                    break;
                }
            }

            return contact;
        }

        private Lead CreateLead(dynamic item)
        {
            var lead = new Lead()
            {
                Id = GetInt(item.id),
                CreationDate = GetDateTime(item.created_at),
                Name = GetString(item.name),
                ContactId = GetInt(item.main_contact.id),
            };

            return lead;

        }

        private Task CreateTask(dynamic item)
        {
            var task = new Task()
            {
                Id = GetInt(item.id),
                CreationDate = GetDateTime(item.created_at),
                Element_Type = GetInt(item.element_type),
                Element_Id = GetInt(item.element_id),
                TaskType = GetInt(item.task_type),
                Comlete_till_at = GetDateTime(item.complete_till_at),
                Text = GetString(item.text),
                Result = new TaskResult()
                {
                    Id = GetInt(item.result.id),
                    TaskId = GetInt(item.id),
                    Text = GetString(item.result.text),
                },
            };

            return task;
        }

        private Note CreateNote(dynamic item)
        {
            var note = new Note()
            {
                Id = GetInt(item.id),
                CreationDate = GetDateTime(item.created_at),
                Element_Id = GetInt(item.element_id),
                Element_Type = GetInt(item.element_type),
                Note_Type = GetInt(item.note_type),
                Text = GetString(item.text),
            };
            return note;

        }




        private int GetInt(dynamic val)
        {
            if (val != null)
                return (int)val;
            else return 0;

        }

        private string GetString(dynamic val)
        {
            return (string)val;
        }

        private DateTime GetDateTime(dynamic val)
        {
            var num = (int)val;
            var startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return startDate.AddSeconds(num).ToLocalTime();
        }


        private List<CustomField> GetCustomFields(dynamic val)
        {
            var result = new List<CustomField>();
            foreach (var item in val)
            {
                result.Add(GetCustomField(item));
            }
            return result;
        }

        private CustomField GetCustomField(dynamic val)
        {
            return new CustomField()
            {
                Id = val.id,
                Name = val.name,
                Values = GetCustomFieldValues(val),
            };
        }

        private List<CustomFieldValue> GetCustomFieldValues(dynamic val)
        {
            var result = new List<CustomFieldValue>();
            foreach (var item in val)
            {
                result.Add(new CustomFieldValue()
                {
                    v_value = val.value,
                });
            }
            return result;
        }
    }
}
