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
    /// Interaction logic for ViewTasks.xaml
    /// </summary>
    public partial class ViewTasks : Window
    {
        public ViewTasks()
        {
            InitializeComponent();
            // Load tasks when the window is initialized
            LoadTasks();
        }
        // Method to load tasks from the SaveTasks class and display them in the ListView
        private void LoadTasks()
        {
            TasksListView.ItemsSource = SaveTasks.taskItems;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
