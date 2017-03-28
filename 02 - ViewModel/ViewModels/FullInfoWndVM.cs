using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ViewModel
{
    public class FullInfoWndVM  : ModalWndVM
    {
        #region Properties
        private string numPlace;
        public string NumPlace
        {
            get
            {
                return numPlace;
            }
            set
            {
                numPlace = value;
                RaisePropertyChanged(() => NumPlace);
            }
        }

        private string client;
        public string Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                RaisePropertyChanged(() => Client);
            }
        }

        private string car;
        public string Car
        {
            get
            {
                return car;
            }
            set
            {
                car = value;
                RaisePropertyChanged(() => Car);
            }
        }

        private string tariff;
        public string Tariff
        {
            get
            {
                return tariff;
            }
            set
            {
                tariff = value;
                RaisePropertyChanged(() => Tariff);
            }
        }
        #endregion

    }
}
