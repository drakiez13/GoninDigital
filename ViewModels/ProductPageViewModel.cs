using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoninDigital.ViewModels
{
    class ProductPageViewModel : BaseViewModel
    {
        private int productId;
        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(); }
        }

        public ProductPageViewModel()
        {

        }
    }
}
