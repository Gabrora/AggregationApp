using AggregationApp.Models;
using System.Data;

namespace AggregationApp.Helpers
{
    public class Queries
    {

        public static IEnumerable<AggregatedElectricity> AggregateData(List<Electricity> electricities)
        {

            var query = electricities.GroupBy(x => x.TINKLAS).Select(g => new
            {
                TINKLAS = g.Key,
                Pplus = g.Sum(s => s.Pplus),
                Pminus = g.Sum(s => s.Pminus)
            });

            var aggregatedList = query.ToList();

            var aggregatedElectricityList = aggregatedList
                            .Select(x => new AggregatedElectricity()
                            {
                                TINKLAS = x.TINKLAS,
                                Pplus = x.Pplus,
                                Pminus = x.Pminus
                            })
                            .ToList();


            return aggregatedElectricityList;

        }

        public static IEnumerable<DataRow> FilterData(DataTable dt)
        {

            object maxDateObj = dt.Compute("MAX(PL_T)", null);
            DateTime maxDate = Convert.ToDateTime(maxDateObj);

            IEnumerable<DataRow> data = from myRow in dt.AsEnumerable()
                                        where myRow.Field<DateTime>("PL_T").Month <= maxDate.Month &&
                                        myRow.Field<DateTime>("PL_T").Month >= maxDate.AddMonths(-3).Month &&
                                        myRow.Field<string>("OBT_PAVADINIMAS") == "Butas"
                                        select myRow;


            return data;
        }

    }
}
