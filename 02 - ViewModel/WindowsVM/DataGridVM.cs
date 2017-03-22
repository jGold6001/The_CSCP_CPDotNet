using _03___Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ViewModel
{
    public class DataGridVM : EFPropertyChanged
    {
        private EFClientOp efClient;
        private ObservableCollection<Record> listRecords;

        public DataGridVM()
        {
            efClient = new EFClientOp();            
        }

        public ObservableCollection<Record> ListRecords
        {
            get
            {
                listRecords = db.Places.Select(p => new Record()
                {
                    NumberPLace = p.Number,
                    ClientLastName = p.Client.LastName,
                    CarBrand = p.Car.Brand,
                    DateRegistred = p.Client.DateRegistred,
                    DatePayment = p.Tariff.DatePayment,
                    Deposit = p.Tariff.Deposit,
                    Debt = p.Tariff.Debt
                });
                return listRecords;
            }

            set
            {
                listRecords = value;
                OnPropertyChanged("ListRecords");
            }
        }
        
    }
}
