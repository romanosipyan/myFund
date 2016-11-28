using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Domain.Models
{
    public interface IStockType
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}
