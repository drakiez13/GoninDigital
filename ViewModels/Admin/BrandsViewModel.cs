using ABI.Windows.System;
using GoninDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    public class BrandsViewModel : BaseViewModel
    {
        #region Properties
        private string searchName;
        public string SearchName
        { 
            get { return searchName; }
            set { searchName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Brand> list;
        public ObservableCollection<Brand> List { get { return list; } set { list = value; OnPropertyChanged(); } }
        private Brand selectedItem;
        public Brand SelectedItem { get { return selectedItem; } set { selectedItem = value; OnPropertyChanged(); } }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        #endregion
        public BrandsViewModel()
        {
            using (var db = new GoninDigitalDBContext())
            {
                list = new ObservableCollection<Brand>(db.Brands);
            }

            #region UpdateCommand
            UpdateCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                using (var db = new GoninDigitalDBContext())
                {
                    var brand = db.Brands.First(x => x.Id == SelectedItem.Id);

                    brand = SelectedItem;
                    db.SaveChanges();
                }
            });
            #endregion

            #region DeleteCommand
            DeleteCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                using (var db = new GoninDigitalDBContext())
                {
                    var brand = db.Brands.First(x => x.Id == SelectedItem.Id);
                    List.Remove(brand);
                    db.Brands.Remove(brand);
                    db.SaveChanges();
                }
            });
            #endregion

            #region AddCommand
            AddCommand = new RelayCommand<Object>((p) =>
            {
                return false;
            }, (p) =>
            {
                using (var db = new GoninDigitalDBContext())
                {
                    var user = db.Brands.First(x => x.Id == SelectedItem.Id);
                    user = SelectedItem;
                    db.SaveChanges();
                }
            });
        }
        #endregion
        #region Methods
        public void SearchChanged()
            {
                if (SearchName == "")
                {
                    using (var db = new GoninDigitalDBContext())
                    {
                        List = new ObservableCollection<Brand>(db.Brands);
                    }
                }
            }
            public void SearchBrand()
            {
                string s = SearchName.ToLower();
                if(SearchName!="")
                {
                    using (var db = new GoninDigitalDBContext())
                    {
                            List = new ObservableCollection<Brand>(db.Brands);
                    }
                    int count = 0;
                    while(count<List.Count())
                    {
                        if (!List[count].Name.ToLower().Contains(s))
                                List.RemoveAt(count);
                        else
                            count += 1;
                    }
                }
            }
        #endregion
    }
}