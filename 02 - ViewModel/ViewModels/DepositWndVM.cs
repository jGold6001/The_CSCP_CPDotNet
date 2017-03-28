using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ViewModel
{
    public class DepositWndVM : ModalWndVM
    {
        #region Properties
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

        private string debt;
        public string Debt
        {
            get
            {
                return debt;
            }
            set
            {
                debt = value;
                RaisePropertyChanged(() => Debt);
            }
        }

        private string deposit;
        public string Deposit
        {
            get
            {
                return deposit;
            }
            set
            {
                deposit = value;
                RaisePropertyChanged(() => Deposit);
            }
        }

        private string minSumm;
        public string MinSumm
        {
            get
            {
                return minSumm;
            }
            set
            {
                minSumm = value;
                RaisePropertyChanged(() => MinSumm);
            }
        }

        private string paySumm;
        public string PaySumm
        {
            get
            {
                return paySumm;
            }
            set
            {
                paySumm = value;
                RaisePropertyChanged(() => PaySumm);
            }
        }

        #endregion
    }
}
