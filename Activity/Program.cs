using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class Zipcode
    {
        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("country abbreviation")]
        public string countryabbreviation { get; set; }

        [JsonProperty("post code")]
        public string postcode { get; set; }

        public List<place> places { get; set; }  

    }

    public class place
    {
        [JsonProperty("place name")]
        public string Name { get; set; }
        [JsonProperty("longitude")]
        public string longitude { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("state abbreviation")]
        public string stateabbreviation { get; set; }
        [JsonProperty("latitude")]
        public string latitude { get; set;}
    }

   
   
    class Program
    {
        private static readonly HttpClient client = new HttpClient();  
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while(true)
            {
                try
                {
                    Console.WriteLine("Enter A Zip Code. Press Enter without writing a name to quit the Program");

                    var zipcode = Console.ReadLine();

                    if (string.IsNullOrEmpty(zipcode))
                    {
                        break;
                    }

                    var result = await client.GetAsync("http://api.zippopotam.us/us/" + zipcode.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var location = JsonConvert.DeserializeObject<Zipcode>(resultRead);
                    Console.WriteLine("------");
                    Console.WriteLine("Zip code: " + location.postcode);
                    Console.WriteLine("Country: "+location.country);
                    Console.WriteLine("Country abbreviation: " + location.countryabbreviation);
                    Console.WriteLine("place name:") ;
                    foreach (var place in location.places)
                    {
                        Console.WriteLine("- " + place.Name);
                        Console.WriteLine("  Longitude: " + place.longitude);
                        Console.WriteLine("  State: " + place.state);
                        Console.WriteLine("  State abbreviation: " + place.stateabbreviation);
                        Console.WriteLine("  Latitude: " + place.latitude);
                    }
                    Console.WriteLine("------");


                }
                catch (Exception) 
                {
                    Console.WriteLine("ERROR, Please enter a valid Zip code.");
                }
            }
        }
    }
}