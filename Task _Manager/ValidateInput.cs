using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task__Manager
{
    public class ValidateInput
    {
        // Method to validate the title input
        public static bool IsTitleValid(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Method to validate due date input
        public static bool IsDueDateValid(DateTime dueDate)
        {
            if (dueDate < DateTime.Today)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}