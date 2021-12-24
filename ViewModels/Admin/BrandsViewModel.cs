using ABI.Windows.System;
using GoninDigital.Models;
using GoninDigital.SharedControl;
using ModernWpf.Controls;
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
        private string brandName;
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Brand> list;
        public ObservableCollection<Brand> List { get { return list; } set { list = value; OnPropertyChanged(); } }
        private Brand selectedBrand;
        public Brand SelectedBrand { get { return selectedBrand; } set { selectedBrand = value; OnPropertyChanged(); } }
        private ContentDialog addBrandDialog;
        public ContentDialog AddBrandDialog
        {
            get { return addBrandDialog; }
            set { addBrandDialog = value; }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        #endregion
        #region Constructor
        public BrandsViewModel()
        {
            using (var db = new GoninDigitalDBContext())
            {
                list = new ObservableCollection<Brand>(db.Brands);
            }

            #region UpdateCommand
            UpdateCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedBrand != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                UpdateExec();
            });
            #endregion

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
        private void UpdateExec()
        {
            if (SelectedBrand.Name == "")
            {
                ContentDialog content = new()
                {
                    Title = "Warning",
                    Content = "No cell is allowed to be left blank",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            else
            {
                using (var db = new GoninDigitalDBContext())
                {
                    db.Brands.Update(SelectedBrand);
                    _ = db.SaveChanges();
                }
                ContentDialog content = new()
                {
                    Title = "Complete",
                    Content = "Updated Successfully",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
        }
        public void AddBrand()
        {
            if(BrandName!="")
            {
                using(var db = new GoninDigitalDBContext())
                {
                    if(db.Brands.Where(x=>x.Name==BrandName).Count()>0)
                    {
                        ContentDialog content = new()
                        {
                            Title = "Warning",
                            Content = "Brand already exists",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                    }
                    else
                    {
                        Brand brand = new Brand();
                        brand.Name = BrandName;
                        db.Brands.Add(brand);
                        _=db.SaveChanges();
                        List = new ObservableCollection<Brand>(db.Brands);
                        ContentDialog content = new()
                        {
                            Title = "Complete",
                            Content = "Added successfully",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                    }
                }
            }
        }
        #endregion
    }
}