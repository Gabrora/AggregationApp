using AggregationApp.Helpers;
using CsvHelper;
using System.Data;
using System.Globalization;
using AggregationApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using AggregationApp.Data;
using AggregationApp.Models;

namespace AggregationApp.Services
{
    public class ElectricityService : IElectricityService
    {
        private readonly DataContext _context;
        private readonly ILogger<ElectricityService> _logger;


        public ElectricityService(DataContext context, ILogger<ElectricityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertData(IEnumerable<AggregatedElectricity> aggregatedElectricityList)
        {
            try
            {
                await _context.AggregatedElectricities.AddRangeAsync(aggregatedElectricityList);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Aggregated Electricity data was successfully added to the database");
            }
            catch (Exception ex)
            {
                _logger.LogError("error occurred while inserting data to database - " + ex.Message);
                throw;
            }

        }

        public async Task ProcessData()
        {
            if (!await _context.AggregatedElectricities.AnyAsync())
            {
                try
                {
                    _logger.LogInformation("AggregatedElectricities datatable is empty, starting data fetching process...");

                    var dt = new DataTable();
                    var urlList = JsonHelper.LoadUrls();

                    foreach (var url in urlList)
                        dt.Merge(await FetchData(url));

                    var resultDt = DtHelper.FilterDataTable(dt);
                    _logger.LogInformation("Electricity data filtered successfully");

                    var dtList = DtHelper.ConvertToList(resultDt);

                    var aggregatedElectricityList = Queries.AggregateData(dtList);
                    _logger.LogInformation("Electricity data aggregated successfully");

                    await InsertData(aggregatedElectricityList);
                }
                catch (Exception ex)
                {
                    _logger.LogError("error occurred while processing Electricity data - " + ex.Message);
                    throw;
                }
            }
            else
                _logger.LogInformation("AggregatedElectrcities data ready for API call");
        }

        public async Task<DataTable> FetchData(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var httpResult = await httpClient.GetAsync(url);
                _logger.LogInformation("data fetched from: " + url);

                using var resultStream = await httpResult.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(resultStream);
                var dt = new DataTable();

                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                using (var dr = new CsvDataReader(csv))
                    dt.Load(dr);
                return dt;

            }
            catch (Exception ex)
            {
                _logger.LogError("error occured while processing the url: " + url + ex.Message);
                throw;
            }

        }


    }

}
