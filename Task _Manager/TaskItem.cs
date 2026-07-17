using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task__Manager
{
    public class TaskItem
    {
        private static int _nextId = 1; //Static field to keep track of the next available Id
        public int Id { get; private set; } // Auto-incremented Id
        public string Title { get; set; } //Title of the task
        public string Description { get; set; } //Description of the task
        public DateTime DueDate { get; set; } //Due date of the task
        public bool IsCompleted { get; set; } //Completion status of the task
        // Empty constructor to allow for parameterless instantiation of TaskItem
        public TaskItem()
        {
        }
        // Constructor to initialize a new TaskItem with the provided values
        public TaskItem(string title, string description, DateTime dueDate, bool isCompleted)
        {
            Id = _nextId++; //Assign the next available Id and increment the static field
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.IsCompleted = isCompleted;
        }
    }
}
