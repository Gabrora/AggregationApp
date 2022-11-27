using AggregationApp.Helpers;
using AggregationApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregationApp.Tests
{
    public class QueriesTests
    {
        [Fact]
        public void AggregateData_ShouldReturnGroupedValuesWithSums()
        {
            //Arrange
            var electricitiesList = new List<Electricity>()
            {
                new Electricity()
                {
                    TINKLAS = "name 1",
                    Pplus = 0.15M,
                    Pminus = 0.05M
                },
                new Electricity()
                {
                    TINKLAS = "name 1",
                    Pplus = 0.85M,
                    Pminus = 0.95M
                },new Electricity()
                {
                    TINKLAS = "name 2",
                    Pplus = 0.1M,
                    Pminus = 0.33M
                },new Electricity()
                {
                    TINKLAS = "name 2",
                    Pplus = 0.9M,
                    Pminus = 0.67M
                },new Electricity()
                {
                    TINKLAS = "name 3",
                    Pplus = 1M,
                    Pminus = 1M
                },
            };
            var GroupedElementsShoulBe = 3;


            //Act
            var aggregatedElectricites = Queries.AggregateData(electricitiesList).ToList();


            //Assert
            Assert.True(GroupedElementsShoulBe == aggregatedElectricites.Count());

            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(1, aggregatedElectricites[i].Pplus);
                Assert.Equal(1, aggregatedElectricites[i].Pminus);
            }
        }


        [Fact]
        public void FilterData_ShouldFiterByMonthsAndApartments()
        {
            //Arrange
            var dt = new DataTable();
            dt.Columns.Add("TINKLAS", typeof(string));
            dt.Columns.Add("OBT_PAVADINIMAS", typeof(string));
            dt.Columns.Add("OBJ_GV_TIPAS", typeof(string));
            dt.Columns.Add("OBJ_NUMERIS", typeof(string));
            dt.Columns.Add("P+", typeof(decimal));
            dt.Columns.Add("PL_T", typeof(DateTime));
            dt.Columns.Add("p-", typeof(decimal));

            dt.Rows.Add(new object[] { "TINKLAS1", "Butas", "", "", 1M, DateTime.Now.AddMonths(-3), 2M }); //should stay
            dt.Rows.Add(new object[] { "TINKLAS2", "notbutas", "", "", 1M, DateTime.Now.AddMonths(-8), 2M });
            dt.Rows.Add(new object[] { "TINKLAS3", "notbutas", "", "", 1M, DateTime.Now.AddMonths(-4), 2M });
            dt.Rows.Add(new object[] { "TINKLAS4", "Butas", "", "", 1M, DateTime.Now.AddMonths(-7), 2M });
            dt.Rows.Add(new object[] { "TINKLAS5", "Butas", "", "", 1M, DateTime.Now.AddMonths(-1), 2M }); //should stay
            var numberOfRows = 2;
            var departmentsName = "Butas";

            //Act

            var filteredDt = Queries.FilterData(dt).ToList();

            //Assert

            Assert.Equal(numberOfRows, filteredDt.Count());

            foreach (var item in filteredDt)
            {
                Assert.Equal(departmentsName, item["OBT_PAVADINIMAS"]);
                Assert.True(DateTime.Now.AddMonths(-4) <= Convert.ToDateTime(item["PL_T"]));
            }

        }
    }
}
