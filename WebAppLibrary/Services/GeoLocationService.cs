using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebAppLibrary.Services
{
    public class GeoLocationService
    {
        private ILogger<GeoLocationService> _logger;

        public GeoLocationService(ILogger<GeoLocationService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup(string location) {
            var result = new CoordServiceResult() {     //Value by default in case of error retrieved by BingKey
                Sucess = false,
                Message = "Undetermined failure while looking up coordinates"
            };

            //Lookup coordinates - Use of BingMap Service :)
            var encodedName = WebUtility.UrlEncode(location);               //You can use WebUtility from ASP.NET own framework
            var bingKey = Startup.Configuration["AppSettings:BingKey"];     //Get from config.json file the KeyValue (user environment variable configured on OS)

            //Calling to the own Basic Bing Key through app settings (also you can create Enterprise Key)


            //http://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURIComponent("[Location to geocode]") + "&jsonp=GeocodeCallback&key=YOUR_BING_MAPS_KEY
            //http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={bingKey}
            //El API de Bing requiere 2 parámetros: la versión codificada de la locación y un Key otorgada por Bing (Hay que crear tu propia llave de Bing - Basic Key).
            //https://www.microsoft.com/maps/create-a-bing-maps-key.aspx

            var bingServiceUrl = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={bingKey}";

            var client = new HttpClient();      //Hay 2 paquetes donde elegir, usar el de System.Net. Es el más compatible con ASP.NET 5 y Core CLR

            var json = await client.GetStringAsync(bingServiceUrl);         //Se asume que es JSON, retorna el valor como un string tipo JSON
                                                                            //Se genera un método asíncrono para ejecutar esta llamada http cliente

            //Lee los resultados y parsea
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues) result.Message = $"Could not find '{location}' as a location";
            else {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{location}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Sucess = true;
                    result.Message = "Success";
                }
            }
            return result;
        }

    }
}
