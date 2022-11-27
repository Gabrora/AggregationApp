using AggregationApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace AggregationApp.Tests
{
    public class ElectricityServiceTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public ElectricityServiceTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        
        [Theory]
        [InlineData("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv")]
        [InlineData("https://data.gov.lt/dataset/1975/download/10765/2022-04.csv")]
        [InlineData("https://data.gov.lt/dataset/1975/download/10764/2022-03.csv")]
        [InlineData("https://data.gov.lt/dataset/1975/download/10763/2022-02.csv")]


        public async void FetchData_ShouldReturnElectricityData(string url)
        {
            //Arrange
            var columnNamesList = new List<string>()
            {
                "TINKLAS",
                "OBT_PAVADINIMAS",
                "OBJ_GV_TIPAS",
                "OBJ_NUMERIS",
                "P+",
                "PL_T",
                "P-"
            };

            //Act
            var data = new DataTable();
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IElectricityService>();
                data = await context.FetchData(url);
            }
            int rowCount = data.Rows.Count;


            //Assert
            Assert.True(rowCount > 0);
            foreach (var column in data.Columns)          
                Assert.Equal(column.ToString(), columnNamesList[data.Columns.IndexOf(column.ToString())]);
            

        }

    }
}
