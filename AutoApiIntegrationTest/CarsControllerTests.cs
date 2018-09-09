using CarDataContract;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace AutoApiIntegrationTest
{
    public class CarsControllerTests
    {
        HttpClient _httpClient;
        public CarsControllerTests()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:54696");
        }

        [Fact]
        public async void Get_Should_ReturnAllCars()
        {
            var httpResult = await _httpClient.GetAsync("/api/odata/Cars");
            httpResult.EnsureSuccessStatusCode();
            var stringResult = await httpResult.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<ODataResponse<Car>>(stringResult).Value;
            Assert.True(cars.Any());
        }

        [Fact]
        public async void Get_Should_ReturnAllCars_OrderByPrice_FilterByPrice20000_Skip1_TakeTop5_WithCount()
        {
            var httpResult = await _httpClient.GetAsync("/api/odata/Cars?$orderby=Price&$filter=Price le 20000&$skip=1&$top=5&$count=true");
            httpResult.EnsureSuccessStatusCode();
            var stringResult = await httpResult.Content.ReadAsStringAsync();
            var carsResponse = JsonConvert.DeserializeObject<ODataResponse<Car>>(stringResult);
            var cars = carsResponse.Value;
            
            Assert.True(cars.All(c => c.Price <= 20000));
            Assert.True(cars.Count() <=5);
            Assert.True(carsResponse.Count > cars.Count); //Because we skip 1
        }

        [Fact]
        public async void Get_Should_ReturnACarWhenPresent()
        {
            int carId = 1;
            var httpResult = await _httpClient.GetAsync("/api/Cars/" + carId);
            httpResult.EnsureSuccessStatusCode();
            var stringResult = await httpResult.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<Car>(stringResult);
            Assert.Equal(car.Id, carId);
        }

        [Fact]
        public async void Post_Should_ReturnACarAfterCreated()
        {
            var json = JsonConvert.SerializeObject(new CarContract { Title = "My Car", FirstRegistration = new DateTime(2018, 02, 01), Fuel = Fuel.Diesel, IsNew = false, Mileage = 80, Price = 17000 });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResult = await _httpClient.PostAsync("/api/Cars/", stringContent);
            httpResult.EnsureSuccessStatusCode();
            var stringResult = await httpResult.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<Car>(stringResult);
            Assert.True(car.Id > 0);
        }

        [Fact]
        public async void Put_Should_Update_WhenACarPresent()
        {
            var json = JsonConvert.SerializeObject(new CarContract { Title = "My Car", FirstRegistration = new DateTime(2018, 02, 01), Fuel = Fuel.Diesel, IsNew = false, Mileage = 80, Price = 17000 });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResult = await _httpClient.PostAsync("/api/Cars/", stringContent);
            var stringResult = await httpResult.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<Car>(stringResult);

            string newTitle = "My New Title";
            int newPrice = 18200;
            car.Title = newTitle;
            car.Price = newPrice;
            json = JsonConvert.SerializeObject(car);
            stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpResult = await _httpClient.PutAsync("/api/Cars/" + car.Id, stringContent);
            httpResult.EnsureSuccessStatusCode();

            httpResult = await _httpClient.GetAsync("/api/Cars/" + car.Id);
            stringResult = await httpResult.Content.ReadAsStringAsync();
            car = JsonConvert.DeserializeObject<Car>(stringResult);
            Assert.Equal(car.Title, newTitle);
            Assert.Equal(car.Price, newPrice);
        }

        [Fact]
        public async void Put_ReturnsNotFound_WhenACarNotPresent()
        {
            var json = JsonConvert.SerializeObject(new Car { Id=1000, Title = "My Car", FirstRegistration = new DateTime(2018, 02, 01), Fuel = Fuel.Diesel, IsNew = false, Mileage = 80, Price = 17000 });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResult = await _httpClient.PutAsync("/api/Cars/" + 1000, stringContent);

            Assert.Equal(HttpStatusCode.NotFound, httpResult.StatusCode);
        }

        [Fact]
        public async void Delete_Should_MarkAdvertisementDeleted()
        {
            int carId = 2;
            var httpResult = await _httpClient.DeleteAsync("/api/Cars/" + carId);
            httpResult.EnsureSuccessStatusCode();
        }
    }
}
