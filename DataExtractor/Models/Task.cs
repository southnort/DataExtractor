using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExtractor
{
    [Serializable]
    class Task : Item
    {
        public int Element_Type { get; set; }
        public int Element_Id { get; set; }
        public int TaskType { get; set; }
        public DateTime Comlete_till_at { get; set; }
        public string Text { get; set; }
        public TaskResult Result { get; set; }
    }

    [Serializable]
    class TaskResult
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Text { get; set; }
    }
}
