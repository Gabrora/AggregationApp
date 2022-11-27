using AggregationApp.Models;
using System.Data;

namespace AggregationApp.Interfaces
{
    public interface IElectricityService
    {
        public Task InsertData(IEnumerable<AggregatedElectricity> aggregatedElectricityList);
        public Task<DataTable> FetchData(string url);
        public Task ProcessData();
    }
}
