using myFund.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Windows;

namespace myFund.ValidationRules
{
    public class StockValidationRule : ValidationRule
    {      
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var model = value as StockTypeModel;
            if (model == null || model.Price == 0 || model.Quantity == 0)
            {
                return new ValidationResult(false,
                  "Please provide correct price and quantity");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
