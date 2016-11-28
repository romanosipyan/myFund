using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Models
{
    public class StockBaseModel : Observable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
                this.NotifyAddCommandChange();
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
                this.NotifyAddCommandChange();
            }
        }

        protected virtual void NotifyAddCommandChange() { }
    }
}
