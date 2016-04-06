using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WeatherServiceClientHW04.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherServiceClientHW04
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            List<string> dSource = new List<string>
            {
                "Fahrenheit",
                "Celsius"
            };
            DegreeType.ItemsSource = dSource;
            DegreeType.SelectedItem = dSource[0];
        }

        private void TemperatureLookup_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TemperatureLookup), null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            WeatherServiceHW04 clientSdk = new WeatherServiceHW04();

            DateTime dateTime;
            decimal? value;
            string date = DateTime.Parse(valueDate.Date.ToString()).ToString("MM/dd/yyyy");
            string time = DateTime.Parse(valueTime.Time.ToString()).ToString("h:mm tt");
            var dateTimeString = date + " " + time;
            dateTime = DateTime.Parse(dateTimeString);
            
            if (TemperatureRadioButton.IsChecked != null && (bool)TemperatureRadioButton.IsChecked)
            {

                if ((string) DegreeType.SelectedValue == "Celsius")
                {
                    value = (decimal?) (9.0/5.0*(double) (decimal.Parse(Value.Text))) + 32;
                }
                else value = Decimal.Parse(Value.Text);

                Temperature temp = new Temperature()
                {
                    Degree = value.ToString(),
                    RecorDateTime = dateTime
                };

               var resultTask = clientSdk.Temperatures.PostTemperatureByTemperatureWithOperationResponseAsync(temp);
            }
            if (HumidityRadioButton.IsChecked != null && (bool)HumidityRadioButton.IsChecked)
            {
                value = Decimal.Parse(Value.Text);
                Humidity humi = new Humidity()
                {
                    Percentage = value.ToString(),
                    RecorDateTime = dateTime
                };
                var resultTask = clientSdk.Humidities.PostHumidityByHumidityWithOperationResponseAsync(humi);
            }
            if (PressureRadioButton.IsChecked != null && (bool)PressureRadioButton.IsChecked)
            {
                value = Decimal.Parse(Value.Text);
                Pressure pres = new Pressure()
                {
                    Millibar = value.ToString(),
                    RecorDateTime = dateTime
                };
                var resultTask = clientSdk.Pressures.PostPressureByPressureWithOperationResponseAsync(pres);
            }

        }

        private void HumidityChoice_Checked(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "Percentage";
            DegreeType.Visibility = Visibility.Collapsed;
        }

        private void TemperatureChoice_Checked(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "Degree";
            DegreeType.Visibility = Visibility.Visible;
        }

        private void PressureCHoice_Checked(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "Percentage";
            DegreeType.Visibility = Visibility.Collapsed;
        }
    }
}
