using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Microsoft.Rest;
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
            //itemsource for combo box F or C
            List<string> dSource = new List<string>
            {
                "Fahrenheit",
                "Celsius"
            };
            DegreeType.ItemsSource = dSource;
            //set default value to Fahrenheit
            DegreeType.SelectedItem = dSource[0];
        }

        /// <summary>
        /// Switch page to temperature lookup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemperatureLookup_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TemperatureLookup), null);
        }

        /// <summary>
        /// Submit value to REST API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //new REST API Client  object
            WeatherServiceHW04 clientSdk = new WeatherServiceHW04();

            DateTime dateTime;
            decimal? value;
            string date = DateTime.Parse(valueDate.Date.ToString()).ToString("MM/dd/yyyy");
            string time = DateTime.Parse(valueTime.Time.ToString()).ToString("h:mm tt");
            //combine date and time from datepicker and timepicker
            var dateTimeString = date + " " + time;
            dateTime = DateTime.Parse(dateTimeString);

            //POST temperature 
            if (TemperatureRadioButton.IsChecked != null && (bool)TemperatureRadioButton.IsChecked)
            {
                //if value is in Celsius convert it to Fahrenheit
                if ((string)DegreeType.SelectedValue == "Celsius")
                {
                    value = (decimal?)(9.0 / 5.0 * (double)(decimal.Parse(Value.Text))) + 32;
                }
                else value = Decimal.Parse(Value.Text);

                Temperature temp = new Temperature()
                {
                    Degree = value.ToString(),
                    RecorDateTime = dateTime
                };

                StatusTextBlock.Text = "POST Request Made, waiting for response...";
                StatusTextBlock.Foreground = new SolidColorBrush(Colors.White);
                StatusBorder.Background = new SolidColorBrush(Colors.Blue);
                // Let the user know something happening
                progressBar.IsIndeterminate = true;

                try
                {
                    Task<HttpOperationResponse<Temperature>> resultTask =
                        clientSdk.Temperatures.PostTemperatureByTemperatureWithOperationResponseAsync(temp);

                    // Wait until task completes
                    if (resultTask != null)
                    {
                        try
                        {
                            resultTask.Wait();
                        }
                        catch (AggregateException ex)
                        {
                            StatusTextBlock.Text = ex.Message;
                            StatusBorder.Background = new SolidColorBrush(Colors.Red);
                        }
                        StatusBorder.Background = new SolidColorBrush(Colors.Green);
                        StatusTextBlock.Text = "Request completed!";
                        progressBar.IsIndeterminate = false;
                    }
                }
                catch (HttpOperationException ex)
                {
                    // Display the exception message
                    StatusTextBlock.Text = ex.Message;
                    StatusBorder.Background = new SolidColorBrush(Colors.Red);
                }
                finally
                {
                    progressBar.IsIndeterminate = false;
                }
            }

            //POST Humidity
            if (HumidityRadioButton.IsChecked != null && (bool)HumidityRadioButton.IsChecked)
            {
                value = Decimal.Parse(Value.Text);
                Humidity humi = new Humidity()
                {
                    Percentage = value.ToString(),
                    RecorDateTime = dateTime
                };
                try
                {
                    Task<HttpOperationResponse<Humidity>> resultTask =
                        clientSdk.Humidities.PostHumidityByHumidityWithOperationResponseAsync(humi);
                    // Wait until task completes
                    if (resultTask != null)
                    {
                        try
                        {
                            resultTask.Wait();
                        }
                        catch (AggregateException ex)
                        {
                            StatusTextBlock.Text = ex.Message;
                            StatusBorder.Background = new SolidColorBrush(Colors.Red);
                        }
                        StatusBorder.Background = new SolidColorBrush(Colors.Green);
                        StatusTextBlock.Text = "Request completed!";
                        progressBar.IsIndeterminate = false;
                    }
                }
                catch (HttpOperationException ex)
                {
                    // Display the exception message
                    StatusTextBlock.Text = ex.Message;
                    StatusBorder.Background = new SolidColorBrush(Colors.Red);
                }
                finally
                {
                    progressBar.IsIndeterminate = false;
                }
            }

            //POST Pressure
            if (PressureRadioButton.IsChecked != null && (bool)PressureRadioButton.IsChecked)
            {
                value = Decimal.Parse(Value.Text);
                Pressure pres = new Pressure()
                {
                    Millibar = value.ToString(),
                    RecorDateTime = dateTime
                };
                try
                {
                    Task<HttpOperationResponse<Pressure>> resultTask = clientSdk.Pressures.PostPressureByPressureWithOperationResponseAsync(pres);
                    // Wait until task completes
                    if (resultTask != null)
                    {
                        try
                        {
                            resultTask.Wait();
                        }
                        catch (AggregateException ex)
                        {
                            StatusTextBlock.Text = ex.Message;
                            StatusBorder.Background = new SolidColorBrush(Colors.Red);
                        }
                        StatusBorder.Background = new SolidColorBrush(Colors.Green);
                        StatusTextBlock.Text = "Request completed!";
                        progressBar.IsIndeterminate = false;
                    }
                }
                catch (HttpOperationException ex)
                {
                    // Display the exception message
                    StatusTextBlock.Text = ex.Message;
                    StatusBorder.Background = new SolidColorBrush(Colors.Red);
                }
                finally
                {
                    progressBar.IsIndeterminate = false;
                }
            }
        }

        /// <summary>
        /// When click on Humidty change text in ValueTextBox and hide temperature type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HumidityChoice_Checked(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "Percentage";
            DegreeType.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// When click on Temperature change text in ValueTextBox and show temperature type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemperatureChoice_Checked(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "Degree";
            DegreeType.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// When click on Pressure change text in ValueTextBox and hide temperature type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PressureCHoice_Checked(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "Percentage";
            DegreeType.Visibility = Visibility.Collapsed;
        }
    }
}
