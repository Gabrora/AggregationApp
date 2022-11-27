
using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Models
{
    public class Electricity
    {
        public string? TINKLAS { get; set; }
        public string? OBT_PAVADINIMAS { get; set; }
        public string? OBJ_GV_TIPAS { get; set; }
        public string? OBJ_NUMERIS { get; set; }
        public decimal Pplus { get; set; }
        public DateTime? PL_T { get; set; }
        public decimal Pminus { get; set; }

    }
}
