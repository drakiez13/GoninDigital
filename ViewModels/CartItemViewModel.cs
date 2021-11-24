using GoninDigital.SharedControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    class CartItemViewModel:BaseViewModel
    {
        private object _Title;
        public object Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged(); }
        }
        private object _Image;
        public object Image
        {
            get { return _Image; }
            set { _Image = value; OnPropertyChanged(); }
        }

        private object _Price;
        public object Price
        {
            get { return _Price; }
            set { _Price = value; OnPropertyChanged(); }
        }
        private object _TotalPrice;
        public object TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; OnPropertyChanged(); }
        }


       
        public ICommand RemoveCartItem;
        public CartItemViewModel()
        {
            RemoveCartItem = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 MessageBox.Show("adsad");
             });
        }
    }
}
