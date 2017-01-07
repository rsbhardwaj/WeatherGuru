using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherGuru
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            string key = "f94b1691f4e2013a4040befc4250458f";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip=" + zipCode+ "&appid=" + key + "&units=imperial";
            // http://api.openweathermap.org/data/2.5/weather?zip=411057&appid=f94b1691f4e2013a4040befc4250458f&units=imperial
            //Make sure developers running this sample replaced the API key
            if (key != "f94b1691f4e2013a4040befc4250458f")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";

                return weather;
            }
            else
                return null;
        }
    }
}
