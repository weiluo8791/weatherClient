using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherServiceClientHW04
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemperatureLookup : Page
    {
        public string IntervalType;
        public TemperatureLookup()
        {
            InitializeComponent();
            List<string> timeTypeSource = new List<string>
            {
                "year",
                "month",
                "week",
                "day"
            };

            TimeComboBox.ItemsSource = timeTypeSource;
            TimeComboBox.SelectedItem = timeTypeSource[0];
            TimeTextBox.PlaceholderText = timeTypeSource[0];
        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null);
        }

        private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TimeComboBox.SelectedValue != null) IntervalType = TimeComboBox.SelectedValue.ToString();
            switch (IntervalType)
            {
                case "year":
                    TimeTextBox.PlaceholderText = "Year";
                    break;
                case "month":
                    TimeTextBox.PlaceholderText = "Month";
                    break;
                case "week":
                    TimeTextBox.PlaceholderText = "Week";
                    break;
                case "day":
                    TimeTextBox.PlaceholderText = "Day";
                    break;
                default:
                    TimeTextBox.PlaceholderText = "";
                    break;
            }
        }

        private async void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherServiceHW04 clientSdk = new WeatherServiceHW04();
            Task<HttpOperationResponse<string>> resultTask = null;
            List<RadioButton> radioButtons = DegreeTypeStackPanel.Children.OfType<RadioButton>().ToList();
            RadioButton rbTarget = radioButtons.First(r => r.IsChecked != null && (bool)r.IsChecked);
            string whichType = (string)rbTarget.Content;

            try
            {
                StatusTextBlock.Text = "GET Request Made, waiting for response...";
                StatusTextBlock.Foreground = new SolidColorBrush(Colors.White);
                StatusBorder.Background = new SolidColorBrush(Colors.Blue);
                OutTextBlock.Text = string.Empty;

                // Let the user know something happening
                progressBar.IsIndeterminate = true;

                switch (whichType)
                {
                    case "Average":
                        resultTask =
                            clientSdk.Temperatures.GetAverageTemperatureByTypeAndPeriodWithOperationResponseAsync(
                                IntervalType, int.Parse(TimeTextBox.Text));
                        break;
                    case "High":
                        resultTask =
                            clientSdk.Temperatures.GetHighTemperatureByTypeAndPeriodWithOperationResponseAsync(
                                IntervalType, int.Parse(TimeTextBox.Text));
                        break;
                    case "Low":
                        resultTask =
                            clientSdk.Temperatures.GetLowTemperatureByTypeAndPeriodWithOperationResponseAsync(
                                IntervalType, int.Parse(TimeTextBox.Text));
                        break;
                }
                // Wait until task completes
                if (resultTask != null)
                {
                    resultTask.Wait();

                    StatusBorder.Background = new SolidColorBrush(Colors.Green);
                    StatusTextBlock.Text = "Request completed!";

                    // Display results
                    if (resultTask.Result.Response.IsSuccessStatusCode)
                    {
                        // Extract the value from the result
                        var messageResult = resultTask.Result.Body;
                        // Set the text block with the result
                        OutTextBlock.Text = messageResult;
                    }
                    else
                    {
                        StatusBorder.Background = new SolidColorBrush(Colors.Orange);
                        StatusTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                        OutTextBlock.Text = "Nothing returned!";
                    }
                }
            }
            catch (HttpOperationException ex)
            {
                // Display the exception message
                string responseMsg = await ex.Response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(responseMsg);
                string messageResult = o.Value<string>("message");
                OutTextBlock.Text = messageResult;
                StatusTextBlock.Text = ex.Message;
                StatusBorder.Background = new SolidColorBrush(Colors.Red);
            }
            catch (Exception ex)
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
}
