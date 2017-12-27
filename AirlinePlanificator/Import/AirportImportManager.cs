
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;

namespace AirlinePlanificator.Import
{
    public class AirportImportManager
    {
        private const string ApiUserKey = "a9495ac8df707fcba724989c770ddb86";
        private const string AirportFile = "ImportedAirport.json";
        private const string WebApiUrl = "https://airport.api.aero/";


        public List<Models.Airport> Import()
        {
            return Import(new string[] { "ymy", "ymx" }).Result;
        }
    
        public List<Models.Airport> Import(string airportCodeIATA)
        {
            return Import(new string[]{airportCodeIATA}).Result;
        }

        private async Task<List<Models.Airport>> Import(string[] airportCode)
        {
            List<Models.Airport> airportModel = new List<Models.Airport>();
            Model.ResponseObject result = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (string code in airportCode)
                {
                    // HTTP GET
                    string url = string.Format("airport/{0}?user_key={1}", code, ApiUserKey);
                    HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<Model.ResponseObject>().ConfigureAwait(false);
                    } 
                }
                    
            }

            if (result != null && result.authorisedAPI && result.success)
            {
                //Convert loaded data into Model
                airportModel = result.airports
                    .Where(airport => !string.IsNullOrWhiteSpace(airport.name) && !string.IsNullOrWhiteSpace(airport.country))
                    .Select(airport => new Models.Airport()
                        {
                            CodeIATA = airport.code,
                            Name = airport.name,
                            City = airport.city,
                            Country = airport.country,
                            Latitude = airport.lat,
                            Longitude = airport.lng,
                        })
                    .ToList();
                
                try
                {
                    //Write to file
                    string airportsJSON = Newtonsoft.Json.JsonConvert.SerializeObject(airportModel);
                    System.IO.File.WriteAllText(AirportFile, airportsJSON);
                }
                catch (Exception)
                {
                    throw;
                }
                    
            }
            return airportModel;
        }
    }
}
