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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task__Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
           // Set up a timer to update the clock every second
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => UpdateClock();
            timer.Start();
        }
        // A method to handle the click event of the "Create Task" button
        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTasks save = new SaveTasks();
            save.Show();
            this.Close();
        }
        // A method to handle the click event of the "View Tasks" button
        private void ViewTasksButton_Click(object sender, RoutedEventArgs e)
        {
            ViewTasks view = new ViewTasks();
            view.Show();
            this.Close();
        }
        // A method to update the clock label every second
        private void UpdateClock()
        {
            if(clocklbl != null)
            {
                clocklbl.Content = DateTime.Now.ToString("hh:mm:ss tt");
            }
        }
    }
}
