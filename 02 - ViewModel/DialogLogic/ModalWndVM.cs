using _03___Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ViewModel
{
    abstract public class ModalWndVM : ViewModelBase, IUserDialogViewModel
    {
        public bool IsModal { get; protected set; }
        EFClientOp efClient = new EFClientOp();

        public ModalWndVM()
        {
            IsModal = true;
        }

        #region Commands
        public Action<ModalWndVM> OnOk { get; set; }
        public RelayCommand OkCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.OnOk != null)
                        this.OnOk(this);
                    else
                        Close();
                });
            }
        }

        public Action<ModalWndVM> OnCancel { get; set; }
        public RelayCommand CancelCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.OnCancel != null)
                        this.OnCancel(this);
                    else
                        Close();
                });
            }
        }

        public Action<ModalWndVM> OnCloseRequest { get; set; }

        public void RequestClose()
        {
            if (this.OnCloseRequest != null)
                this.OnCloseRequest(this);
            else
                Close();
        }

        public event EventHandler DialogClosing;
        public void Close()
        {
            if (this.DialogClosing != null)
                this.DialogClosing(this, new EventArgs());
        }
        #endregion
    }
}
