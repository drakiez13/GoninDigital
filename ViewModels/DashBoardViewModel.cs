using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoninDigital.ViewModels
{
    internal class DashBoardViewModel: BaseViewModel
    {
        private bool hasVendor;
        public bool HasVendor
        {

            get { return hasVendor; }
            set { hasVendor = value; OnPropertyChanged(); }
        }
        public DashBoardViewModel()
        {
            HasVendor = false;
        }
    }
}
