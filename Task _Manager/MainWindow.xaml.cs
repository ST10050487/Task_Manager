using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            // Await the async method in the constructor using a helper
            Loaded += async (s, e) => await DisplayWeatherForecast();

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
        private async Task DisplayWeatherForecast()
        {
            var httpClient = new HttpClient();
            var weatherService = new WeatherService(httpClient, "613f6012da449f597765f46ed746f2b6");

            // Fetch and check current weather
            var currentWeather = await weatherService.GetCurrentWeatherAsync("Cape Town");
            if (currentWeather != null && currentWeather.Main != null)
            {
                weatherlbl.Content = $"Current Temp in {currentWeather.Name}: {currentWeather.Main.Temp}°C";
            }
            else
            {
                weatherlbl.Content = "Unable to load current weather.";
            }

            // Fetch and check forecast
            var forecast = await weatherService.GetForecastAsync("Cape Town");
            if (forecast != null && forecast.List != null)
            {
                forecastlbl.Content = $"Forecast for {forecast.City?.Name ?? "Unknown"}:";
                foreach (var item in forecast.List.Take(3))
                {
                    forecastlbl.Content += $"\n{item.Dt_txt}: {item.Main?.Temp}°C, {item.Weather?[0]?.Description}";
                }
            }
            else
            {
                forecastlbl.Content = "Unable to load forecast.";
            }
        }
    }
}
