using AggregationApp.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace AggregationApp.Tests
{
    public class ElectricityControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public ElectricityControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Get_ShouldReturnAggregatedElectricitiesData()
        {
            //Arrange

            //Act
            using var client = _factory.CreateClient();
            var response = await client.GetAsync("/Electricity");
            var contents = await response.Content.ReadAsStringAsync();
            var objectsList = JsonConvert.DeserializeObject<List<AggregatedElectricity>>(contents);


            //Assert
            Assert.NotNull(objectsList);

        }

    }
}
