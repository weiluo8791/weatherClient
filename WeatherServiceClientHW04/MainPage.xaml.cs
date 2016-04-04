using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
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
            this.InitializeComponent();
            List<string> dSource = new List<string>
            {
                "Fahrenheit",
                "Celsius"
            };
            DegreeType.ItemsSource = dSource;
        }

        private void WeatherInput_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WeatherView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            WeatherServiceHW04 clientSDK = new WeatherServiceHW04();

            string dateTimeString = "";
            DateTime dateTime = DateTime.MinValue;
            decimal? value = null;


            string date = DateTime.Parse(valueDate.Date.ToString()).ToString("MM/dd/yyyy");
            string time = DateTime.Parse(valueTime.Time.ToString()).ToString("h:mm tt");
            dateTimeString = date + " " + time;
            dateTime = DateTime.Parse(dateTimeString);
            if ((string) DegreeType.SelectedValue == "Celsius")
            {
                value =  (decimal?) (9.0 / 5.0 * (double) (decimal.Parse(Value.Text))) + 32;
            }
            else value = Decimal.Parse(Value.Text);

            Temperature temp = new Temperature()
            {
                Degree = value.ToString(),
                RecorDateTime = dateTime
            };

            var resultTask = clientSDK.Temperatures.PostTemperatureByTemperatureWithOperationResponseAsync(temp);
            
        }
    }
}
