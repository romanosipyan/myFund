using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Models
{
    public class SummaryModel
    {
        public int TotalNumbers { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalMarketValue { get; set; }
    }
}
