using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using PrevisaoTempo.Models;

namespace PrevisaoTempo.Service
{
    public class DataService
    {
        public static async Task<Tempo> GetPrevisaoDoTempo(string cidade)
        {
            string appID = "4b488a3f2d4165c345fe4573a0b6e40f";
            string API_CLIMA = "http://api.openweathermap.org/data/2.5/weather?q=" + cidade + "&units=metric" + "&appid=" + appID;

            dynamic result = await DataService.getDataFromService(API_CLIMA).ConfigureAwait(false);

            if (result["weather"] != null)
            {
                Tempo previsao = new Tempo();
                previsao.Title = (string)result["name"];
                previsao.Temperature = (string)result["main"]["temp"] + " C";
                previsao.Wind = (string)result["wind"]["speed"] + " mph";
                previsao.Humidity = (string)result["main"]["humidity"] + " %";
                previsao.Visibility = (string)result["weather"][0]["main"];

                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)result["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)result["sys"]["sunset"]);
                previsao.Sunrise = String.Format("{0:d/MM/yyyy HH:mm:ss}", sunrise);
                previsao.Sunset = String.Format("{0:d/MM/yyyy HH:mm:ss}", sunset);
                return previsao;
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }

        public static async Task<dynamic> getDataFromServiceByCity(string cidade)
        {
            string appID = "4b488a3f2d4165c345fe4573a0b6e40f";
            string API_CLIMA = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", cidade.Trim(), appID);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(API_CLIMA);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
    }
}
