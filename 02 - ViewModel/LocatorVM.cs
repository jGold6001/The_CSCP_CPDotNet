using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ViewModel
{
    public class LocatorVM
    {
        public LocatorVM()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainWndVM>();
        }

        public MainWndVM Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWndVM>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
