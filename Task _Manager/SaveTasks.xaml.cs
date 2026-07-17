using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task__Manager
{
    /// <summary>
    /// Interaction logic for SaveTasks.xaml
    /// </summary>
    public partial class SaveTasks : Window
    {
        // Static list to hold all task items
        public static List<TaskItem> taskItems = new List<TaskItem>();
        
        public SaveTasks()
        {
            InitializeComponent();
            // Set the default date of the DueDatePicker to today's date
            DueDatePicker.SelectedDate = DateTime.Today;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Get input from UI elements
            string title = TitleTextBox.Text;
            string description = DescriptionTextBox.Text;
            // Use today's date as fallback if no date is selected
            DateTime dueDate = DueDatePicker.SelectedDate ?? DateTime.Today;
            bool isCompleted = IsCompletedCheckBox.IsChecked ?? false;

            if(ValidateInput.IsTitleValid(title) == false)
            {
                titleErrorlabel.Content = "** Enter A Task Title";
                titleErrorlabel.Foreground = Brushes.Red;
                return;
            }else if(ValidateInput.IsDueDateValid(dueDate) == false)
            {
                dueDateErrorlabel.Content = "** Due Date Cannot Be In The Past";
                dueDateErrorlabel.Foreground = Brushes.Red;
                return;
            }
            else
            {
                // Create a new TaskItem and populate it with the input values
                TaskItem newTask = new TaskItem();
                // If input is valid, create a new TaskItem and add it to the list
                newTask.Title = title;
                newTask.Description = description;
                newTask.DueDate = dueDate;
                newTask.IsCompleted = isCompleted;
                //Add the current task to the List of Tasks
                taskItems.Add(newTask);
                //Show confirmation message
                ClosingMessageBox();
            }
        }
        // Event handler for the Cancel button click event
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            // Close the current window without saving
            this.Close();
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Clear the title error label when the text changes
            titleErrorlabel.Content = string.Empty;
        }
        private void DueDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the due date error label when the user picks a new date
            dueDateErrorlabel.Content = string.Empty;
        }
        // Method to show a confirmation message box after saving a task
        private void ClosingMessageBox()
        {
            // Ask the user what to do next
            MessageBoxResult result = MessageBox.Show(
                "Task Saved Successfully!\n\nWould you like to add another task?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // They clicked Yes: Clear the inputs so they can start fresh
                TitleTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
                DueDatePicker.SelectedDate = DateTime.Today;
                IsCompletedCheckBox.IsChecked = false;

                // Put the typing cursor back in the title box
                TitleTextBox.Focus();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                // They clicked No: Close the window and return to main
                this.Close();
            }
        }
    }
}
