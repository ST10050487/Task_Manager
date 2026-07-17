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
        }
        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTasks save = new SaveTasks();
            save.Show();
            this.Close();
        }
        private void ViewTasksButton_Click(object sender, RoutedEventArgs e)
        {
            ViewTasks view = new ViewTasks();
            view.Show();
            this.Close();
        }
    }
}
