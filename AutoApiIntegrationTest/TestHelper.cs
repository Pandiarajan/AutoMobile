using CarDataContract;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoApiIntegrationTest
{
    public static class TestHelper
    {
        public static async Task<ODataResponse<Car>> GetOdataResponse(this Task<HttpResponseMessage> httpResponseMessage)
        {
            var response = await httpResponseMessage;
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ODataResponse<Car>>(stringResult);
        }

        public static async Task<Car> GetResponse(this Task<HttpResponseMessage> httpResponseMessage)
        {            
            var response = await httpResponseMessage;
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Car>(stringResult);
        }

        public static StringContent GetContract()
        {
            var json = JsonConvert.SerializeObject(new CarContract { Title = "My Car", FirstRegistration = new DateTime(2018, 02, 01), Fuel = Fuel.Diesel, IsNew = false, Mileage = 80, Price = 17000 });
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static StringContent GetCar()
        {
            var json = JsonConvert.SerializeObject(new Car { Id = 1000, Title = "My Car", FirstRegistration = new DateTime(2018, 02, 01), Fuel = Fuel.Diesel, IsNew = false, Mileage = 80, Price = 17000 });
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
