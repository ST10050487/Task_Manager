using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;

namespace Task__Manager
{
    // A class that implements the IWeatherService interface to fetch weather data from an external API
    public class WeatherService : IWeatherService
    {
        // A constructor that initializes the HttpClient and API key
        private readonly HttpClient _httpClient;
        // A constructor that initializes the HttpClient and API key
        private readonly string _apiKey;
        // A dictionary to cache the weather data for each city with a timestamp
        private readonly Dictionary<string, (WeatherResponse Data, DateTime Timestamp)> _weatherCache
            = new Dictionary<string, (WeatherResponse, DateTime)>();
        // A dictionary to cache the forecast data for each city with a timestamp
        private readonly Dictionary<string, (ForecastResponse Data, DateTime Timestap)> _forecastCache
            = new Dictionary<string, (ForecastResponse, DateTime)>();
        // A constant that defines how long to cache the data before refreshing it
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);
        // A constructor that initializes the HttpClient and API key
        public WeatherService(HttpClient httpClient, string apikey)
        {
            _httpClient = httpClient;
            _apiKey = apikey;
        }
        // A method that fetches the current weather data for a given city asynchronously
        public async Task<WeatherResponse> GetCurrentWeatherAsync(string city)
        {
            // Check if the weather data for the city is already cached and still valid
            if (_weatherCache.ContainsKey(city))
            {
                // If the cached data is still valid, return it
                var (data, timestamp) = _weatherCache[city];
                if(DateTime.Now - timestamp < _cacheDuration)
                {
                    return data;
                }
            }
            // code to fetch current weather data from the OpenWeatherMap API
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            // Send a GET request to the API and get the response
            var response = await _httpClient.GetAsync(url);
            // Ensure the response is successful, otherwise throw an exception
            response.EnsureSuccessStatusCode();
            // Read the response content as a string
            string jsonResponse = await response.Content.ReadAsStringAsync();
            // Set the JsonSerializerOptions to ignore case when deserializing the JSON response
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // Deserialize the JSON response into a WeatherResponse object and return it
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(jsonResponse, options);
            // Cache the weather data for the city with the current timestamp
            _weatherCache[city] = (weatherData, DateTime.Now);
            // Return the weather data
            return weatherData;
        }
        // A method that fetches the weather forecast data for a given city asynchronously
        public async Task<ForecastResponse> GetForecastAsync(string city)
        {
            // Check if the forecast data for the city is already cached and still valid
            if (_forecastCache.ContainsKey(city))
            {
                // If the cached data is still valid, return it
                var (data, timestamp) = _forecastCache[city];
                // If the cached data is still valid, return it
                if (DateTime.Now - timestamp < _cacheDuration)
                {
                    return data;
                }
            }
            // code to fetch forecast data from the OpenWeatherMap API
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric";
            // Send a GET request to the API and get the response
            var response = await _httpClient.GetAsync(url);
            // Ensure the response is successful, otherwise throw an exception
            response.EnsureSuccessStatusCode();
            // Read the response content as a string
            string jsonResponse = await response.Content.ReadAsStringAsync();
            // Set the JsonSerializerOptions to ignore case when deserializing the JSON response
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // Deserialize the JSON response into a ForecastResponse object and return it
            var forecastData = JsonSerializer.Deserialize<ForecastResponse>(jsonResponse, options);
            // Cache the forecast data for the city with the current timestamp
            _forecastCache[city] = (forecastData, DateTime.Now);
            // Return the forecast data
            return forecastData;
        }
    }
    // Classes to represent the JSON responses from the OpenWeatherMap API
    public class WeatherResponse
    {
        public string Name { get; set; }
        public MainInfo Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
    }
    // A class to represent the forecast response from the OpenWeatherMap API
    public class ForecastResponse
    {
        public CityInfo City { get; set; }
        public ForecastItem[] List { get; set; }
    }
    // A class to represent the main weather information from the OpenWeatherMap API
    public class MainInfo
    {
        public double Temp { get; set; }
    }
    // A class to represent the weather description from the OpenWeatherMap API
    public class WeatherInfo
    {
        public string Description { get; set; }
    }
    // A class to represent the city information from the OpenWeatherMap API
    public class CityInfo
    {
        public string Name { get; set; }
    }
    // A class to represent the forecast item from the OpenWeatherMap API
    public class ForecastItem
    {
        public MainInfo Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public string Dt_txt { get; set; }
    }

}
