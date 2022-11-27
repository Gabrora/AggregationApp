using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AggregationApp.Models
{
    public class AggregatedElectricity
    {
        [Key]
        public string? TINKLAS { get; set; }
        public decimal Pplus { get; set; }
        public decimal Pminus { get; set; }
    }
}
