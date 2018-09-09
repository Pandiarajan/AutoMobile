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
            var cars = await _httpClient.GetAsync("/api/odata/Cars").GetOdataResponse();            
            Assert.True(cars.Value.Any());
        }

        [Fact]
        public async void Get_Should_ReturnAllCars_OrderByPrice_FilterByPrice20000_Skip1_TakeTop5_WithCount()
        {
            var carsResponse = await _httpClient.GetAsync("/api/odata/Cars?$orderby=Price&$filter=Price le 20000&$skip=1&$top=5&$count=true").GetOdataResponse();
            
            Assert.True(carsResponse.Value.All(c => c.Price <= 20000));
            Assert.True(carsResponse.Value.Count() <=5);
            Assert.True(carsResponse.Count > carsResponse.Value.Count); //Because we skip 1
        }

        [Fact]
        public async void Get_Should_ReturnACarWhenPresent()
        {
            int carId = 1;
            var car = await _httpClient.GetAsync("/api/Cars/" + carId).GetResponse();
            Assert.Equal(car.Id, carId);
        }

        [Fact]
        public async void Post_Should_ReturnACarAfterCreated()
        {
            var json = JsonConvert.SerializeObject(new CarContract { Title = "My Car", FirstRegistration = new DateTime(2018, 02, 01), Fuel = Fuel.Diesel, IsNew = false, Mileage = 80, Price = 17000 });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var car = await _httpClient.PostAsync("/api/Cars/", stringContent).GetResponse();
            Assert.True(car.Id > 0);
        }

        [Fact]
        public async void Put_Should_Update_WhenACarPresent()
        {
            var stringContent = TestHelper.GetContract();
            var car = await _httpClient.PostAsync("/api/Cars/", stringContent).GetResponse();

            string newTitle = "My New Title";
            int newPrice = 18200;
            car.Title = newTitle;
            car.Price = newPrice;
            var json = JsonConvert.SerializeObject(car);
            stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResult = await _httpClient.PutAsync("/api/Cars/", stringContent);
            httpResult.EnsureSuccessStatusCode();

            car = await _httpClient.GetAsync("/api/Cars/" + car.Id).GetResponse();
            Assert.Equal(car.Title, newTitle);
            Assert.Equal(car.Price, newPrice);
        }

        [Fact]
        public async void Put_ReturnsNotFound_WhenACarNotPresent()
        {            
            var httpResult = await _httpClient.PutAsync("/api/Cars/", TestHelper.GetCar());
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
