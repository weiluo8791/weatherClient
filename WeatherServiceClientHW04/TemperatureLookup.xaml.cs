using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Microsoft.Rest;

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
            //itemsource for combo box 
            List<string> timeTypeSource = new List<string>
            {
                "year",
                "month",
                "week",
                "day"
            };
            //set the itemsource for combo box
            TimeComboBox.ItemsSource = timeTypeSource;
            //set the default to year
            TimeComboBox.SelectedItem = timeTypeSource[0];
            //set the placehodertext to year
            TimeTextBox.PlaceholderText = timeTypeSource[0];
        }

        /// <summary>
        /// going back to main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null);
        }

        /// <summary>
        /// Change placeholder text depends on combo box selected value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Get average, high, low temperature for a time period
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherServiceHW04 clientSdk = new WeatherServiceHW04();
            Task<HttpOperationResponse<string>> resultTask = null;
            //check whick radio button is selected within the same stackpanel
            List<RadioButton> radioButtons = DegreeTypeStackPanel.Children.OfType<RadioButton>().ToList();
            RadioButton rbTarget = radioButtons.First(r => r.IsChecked != null && (bool)r.IsChecked);
            string whichType = (string)rbTarget.Content;

            try
            {
                StatusTextBlock.Text = "GET Request Made, waiting for response...";
                StatusTextBlock.Foreground = new SolidColorBrush(Colors.White);
                StatusBorder.Background = new SolidColorBrush(Colors.Blue);
                //init result text box
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
