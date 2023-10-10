using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P04WeatherForecastAPI.Client
{
    public partial class MainWindow : Window
    {
        AccuWeatherService accuWeatherService;
        public MainWindow()
        {
            accuWeatherService = new AccuWeatherService();
            InitializeComponent();        
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            City[] cities= await accuWeatherService.GetLocations(txtCity.Text);
            lbData.ItemsSource = cities;
        }

        private async void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCity= (City)lbData.SelectedItem;
            if(selectedCity != null)
            {
                var weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);
                lblCityName.Content = selectedCity.LocalizedName;
                double tempValue = weather.Temperature.Metric.Value;
                string[] data = await accuWeatherService.GetInfo(selectedCity.Key);
                for(int i =0; i < 3; i++)
                {
                    data[i] = data[i].Replace(".", ",");
                    double container = double.Parse(data[i]);
                    double celcius = (container - 32) * 5 / 9;
                    data[i] = celcius.ToString("0.0").Replace(",", ".");
                }
                lblTemperatureValue.Content = data[3];
                lblTemperatureValue2.Content = data[4];
                lblTemperatureValue3.Content = Convert.ToString(tempValue);
                lblTemperatureValue4.Content = data[0];
                lblTemperatureValue5.Content = data[1];
                lblTemperatureValue6.Content = data[2];
                lblTemperatureValue7.Content = data[5];
            }
        }
    }
}
