using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GoninDigital.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    class ManageProductPageViewModel: BaseViewModel
    {
        #region Properties
        private ObservableCollection<Product> l_Product;
        public ObservableCollection<Product> L_Product
        {
            get { return l_Product; }
            set { l_Product = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Product> l_NewProduct;
        public ObservableCollection<Product> L_NewProduct
        {
            get { return l_NewProduct; }
            set { l_NewProduct = value; OnPropertyChanged(); }
        }
        private Product selectedItem;
        public Product SelectedItem { get { return selectedItem; } set { selectedItem = value; OnPropertyChanged(); } }
        private IEnumerable<Product> selectedProducts;
        public IEnumerable<Product> SelectedProducts
        {
            get { return selectedProducts; }
            set { selectedProducts = value; OnPropertyChanged(); }
        }
        private string searchName;
        public string SearchName
        {
            get { return searchName; }
            set
            {
                searchName = value; OnPropertyChanged();
            }
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand RemoveSelectionsCommand { get; set; }
        public ICommand AcceptSelectionsCommand { get; set; }
        #endregion
        #region Constructor
        public ManageProductPageViewModel()
        {
            using(var db=new GoninDigitalDBContext())
            {
                L_Product = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId == (int)Utils.Constants.ProductStatus.ACCEPTED | x.StatusId== (int)Utils.Constants.ProductStatus.UPDATED));
                L_NewProduct = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId == (int)Utils.Constants.ProductStatus.CREATED));
            }
            RemoveCommand = new RelayCommand<Product>(o => true,
               product => { RemoveExec(product); });
            AcceptCommand = new RelayCommand<Product>(o => true, 
                product => { AcceptExec(product); });
            RemoveSelectionsCommand = new RelayCommand<Product>(o => true, SelectedProducts =>
            { RemoveSelectionsExec(selectedProducts); });
            AcceptSelectionsCommand = new RelayCommand<Product>(o => true, SelectedProducts =>
            { AcceptSelectionsExec(selectedProducts); });
            DeleteCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                DeleteExec();
            });
            SearchName = "";
            selectedProducts = new ObservableCollection<Product>();
        }
        #endregion
        #region Methods
        private void RemoveExec(Product product)
        {
            L_NewProduct.Remove(product);
            using (var db = new GoninDigitalDBContext())
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
        private void AcceptExec(Product product)
        {
            L_NewProduct.Remove(product);
            L_Product.Add(product);
            using (var db = new GoninDigitalDBContext())
            {
                db.Products.First(x => x.Id == product.Id).StatusId = (int)Utils.Constants.ProductStatus.ACCEPTED;
                db.SaveChanges();
            }
        }
        private void RemoveSelectionsExec(IEnumerable<Product> selectedProducts)
        {

            using (var db = new GoninDigitalDBContext())
            {
                foreach (Product product in selectedProducts.ToList())
                {
                    L_NewProduct.Remove(product);
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
        }
        private void AcceptSelectionsExec(IEnumerable<Product> selectedProducts)
        {
            if (selectedProducts != null)
            {
                using (var db = new GoninDigitalDBContext())
                {
                    foreach (Product product in selectedProducts.ToList())
                    {
                        L_NewProduct.Remove(product);
                        L_Product.Add(product);
                        db.Products.First(x => x.Id == product.Id).StatusId = (int)Utils.Constants.ProductStatus.ACCEPTED;
                        db.SaveChanges();
                    }
                }
            }
        }
        private void DeleteExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                var product = db.Products.First(x => x.Id == SelectedItem.Id);
                foreach (Product p in L_Product)
                {
                    if (product.Id == p.Id)
                    {
                        L_Product.Remove(p);
                        break;
                    }
                }
                db.Products.First(x => x.Id == product.Id).StatusId = (int)Utils.Constants.ProductStatus.REMOVED;
                db.SaveChanges();
            }
        }
        public void SearchProduct(bool flag)
        {
            string s = SearchName.ToLower();
            if (SearchName != "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    if (flag)
                        L_Product = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId != (int)Utils.Constants.ProductStatus.CREATED));
                    else
                        L_Product = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId == (int)Utils.Constants.ProductStatus.ACCEPTED | x.StatusId == (int)Utils.Constants.ProductStatus.UPDATED));
                }
                int count = 0;
                while (count < L_Product.Count())
                {
                    if (!L_Product[count].Name.ToLower().Contains(s) & !L_Product[count].Vendor.Name.ToLower().Contains(s) & !L_Product[count].Category.Name.ToLower().Contains(s))
                        L_Product.RemoveAt(count);
                    else
                        count += 1;
                }
            }
        }
        public void SearchChanged(bool flag)
        {
            if (SearchName == "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    if (flag)
                        L_Product = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId != (int)Utils.Constants.ProductStatus.CREATED));
                    else
                        L_Product = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId == (int)Utils.Constants.ProductStatus.ACCEPTED | x.StatusId == (int)Utils.Constants.ProductStatus.UPDATED));
                }
            }
        }
        public void ToogleChanged(bool flag)
        {
            using(var db = new GoninDigitalDBContext())
            {
                if(flag)
                    L_Product= new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId != (int)Utils.Constants.ProductStatus.CREATED));
                else
                    L_Product = new ObservableCollection<Product>(db.Products.Include(x => x.Vendor).Include(x => x.Category).Where(x => x.StatusId == (int)Utils.Constants.ProductStatus.ACCEPTED | x.StatusId == (int)Utils.Constants.ProductStatus.UPDATED));
            }
        }
        #endregion
    }
}
