//by vertraxumtes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Numerics;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
 
 
namespace KonsolenTests
{
    class Program
    { 
        public const string APIKey = "63fc4a1c3c719dcd2c8ab677f6ff0820";
 
        static void Main(string[] args)
        {
 
            Console.WriteLine("Please specify a location for example Berlin or NewYork");
            string Ort = Console.ReadLine();
            WeatherObject weather = getWetterByName(Ort);
            Console.WriteLine("Weather in " + Ort + " is as follows:");
            Console.WriteLine("Temperature: " + (weather.main.temp - 273) + " C");
            Console.WriteLine("Air pressure: " + weather.main.pressure + " hPa");
            Console.WriteLine("Humidity: " + weather.main.humidity + " %");
            Console.WriteLine("Maximum temperature: " + (weather.main.temp_max - 273) + " C");
            Console.WriteLine("Minimum temperature: " + (weather.main.temp_min -273) + " C");
            Console.WriteLine("Description: " + weather.weather[0].description);
            Console.ReadLine();            
        }
//API Interaction
        public static WeatherObject getWetterByName(string Ort)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://api.openweathermap.org/data/2.5/weather?q=%22);
            sb.Append(Ort +"&APPID=");
            sb.Append(APIKey);
            WebRequest request = WebRequest.Create(sb.ToString());
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return new JavaScriptSerializer().Deserialize<WeatherObject>(responseFromServer);
        }
 
    }
//Necessary classes to convert the received weather data in json format into a "WeatherObject" object
 
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }
 
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
 
    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }
 
    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }
 
    public class Clouds
    {
        public int all { get; set; }
    }
 
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
 
    public class WeatherObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

//by vertraxumtes