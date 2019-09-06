using System;
using RestSharp;
using Newtonsoft.Json;

namespace MakeevTelegramBot
{
    public class Weather
    {
        public class WeatherData
        {
            public Current current { get; set; }
        }

        public class Current
        {
            public double temp_c { get; set; }
            public Condition condition { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
        }

        const string API_URL = "http://api.apixu.com/v1/current.json";
        const string API_KEY = "1cd176a66c794110b0a134936190509";
        const string FINAL_URL = API_URL + "?key=" + API_KEY + "&lang=ru&q=";
        private RestClient RC = new RestClient();



        public Weather()
        {
            
        }

        public string getWeatherInCity(string city)
        {
            var URL = FINAL_URL + city;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<WeatherData>(Response.Content);

            var Temp = (int)Data.current.temp_c;
            return $"В городе {city} сейчас {Data.current.condition.text}, где-то {Temp} градусов";

        }

    }

}



