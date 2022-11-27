using AggregationApp.Models;
using System.Data;
using System.Globalization;

namespace AggregationApp.Helpers
{
    public class DtHelper
    {
        public static DataTable ChangeColumnType(DataTable dt, int index, Type type)
        {
            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[5].DataType = type;
            foreach (DataRow row in dt.Rows)
                dtCloned.ImportRow(row);
            return dtCloned;
        }

        public static DataTable FilterDataTable(DataTable dt)
        {
            dt = DtHelper.ChangeColumnType(dt, 5, typeof(DateTime));
            var data = Queries.FilterData(dt);
            dt = data.CopyToDataTable<DataRow>();
            return dt;
        }

        public static List<Electricity> ConvertToList(DataTable dt)
        {

            var tableList = new List<Electricity>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Electricity record = new Electricity();
                record.TINKLAS = dt.Rows[i]["TINKLAS"].ToString();
                record.OBT_PAVADINIMAS = dt.Rows[i]["OBT_PAVADINIMAS"].ToString();
                record.OBJ_GV_TIPAS = dt.Rows[i]["OBJ_GV_TIPAS"].ToString();
                record.OBJ_NUMERIS = dt.Rows[i]["OBJ_NUMERIS"].ToString();
                if (dt.Rows[i]["P+"].ToString() == "")
                    record.Pplus = 0;
                else
                    record.Pplus = Convert.ToDecimal(dt.Rows[i]["P+"]);

                record.PL_T = Convert.ToDateTime(dt.Rows[i]["PL_T"]);

                if (dt.Rows[i]["P-"].ToString() == "")
                    record.Pminus = 0;
                else
                    record.Pminus = Convert.ToDecimal(dt.Rows[i]["P-"]);

                tableList.Add(record);
            }

            return tableList;

        }

    }
}
