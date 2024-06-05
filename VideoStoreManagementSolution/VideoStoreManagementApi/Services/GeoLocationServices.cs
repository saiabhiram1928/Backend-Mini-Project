using Google.Apis.Services;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.DistanceMatrix.Request;
using GoogleMapsApi.Entities.DistanceMatrix.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using VideoStoreManagementApi.Interfaces.Services;

namespace VideoStoreManagementApi.Services
{
    public class GeoLocationServices : IGeoLocationServices
    {
        private string _apiKey;
        private const string GoogleMapsApiUrl = "https://maps.googleapis.com/maps/api/geocode/json";

        public GeoLocationServices(IConfiguration configuration)
        {
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        public async Task<double> GetDistanceAsync(string origin, string destination)
        {
            string requestUri = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={_apiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(requestUri);
                var jsonResponse = JObject.Parse(response);

                if (jsonResponse["status"].Value<string>() != "OK")
                {
                    return -1;
                }

                var elements = jsonResponse["rows"][0]["elements"][0];

                if (elements["status"].Value<string>() != "OK")
                {
                    return -1;
                }

                var distance = elements["distance"]["value"].Value<double>(); 
                return distance;
            }
           
        }
        public async Task<bool> ValidateAddress(string address)
        {
            try
            {
                var url = $"{GoogleMapsApiUrl}?address={Uri.EscapeDataString(address)}&key={_apiKey}";
                var httpClient = new HttpClient();  
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    if (data == null) return false;

                    if (data.status == "OK" && data.results.Count > 0)
                    {
                        
                        return true;
                    }
                }


                return false;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error validating address: {ex.Message}");
                return false;
            }
        }


        }
}
