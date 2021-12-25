using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GoninDigital.Models;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class ManageCategoryViewModel:BaseViewModel
    {
        #region Properties
        private string searchName;
        public string SearchName
        {
            get { return searchName; }
            set { searchName = value; OnPropertyChanged(); }
        }
        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ProductCategory> list;
        public ObservableCollection<ProductCategory> List { get { return list; } set { list = value; OnPropertyChanged(); } }
        private ProductCategory selectedCategory;
        public ProductCategory SelectedCategory { get { return selectedCategory; } set { selectedCategory = value; OnPropertyChanged(); } }

        public ICommand UpdateCommand { get; set; }
        #endregion
        #region Constructor
        public ManageCategoryViewModel()
        {
            using (var db = new GoninDigitalDBContext())
            {
                list = new ObservableCollection<ProductCategory>(db.ProductCategories);
            }

            #region UpdateCommand
            UpdateCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedCategory != null)
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
                    List = new ObservableCollection<ProductCategory>(db.ProductCategories);
                }
            }
        }
        public void SearchCategory()
        {
            string s = SearchName.ToLower();
            if (SearchName != "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    List = new ObservableCollection<ProductCategory>(db.ProductCategories);
                }
                int count = 0;
                while (count < List.Count())
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
            if (SelectedCategory.Name == "")
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
                    db.ProductCategories.Update(SelectedCategory);
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
        public void AddCategory()
        {
            if (CategoryName != "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    if (db.ProductCategories.Where(x => x.Name == CategoryName).Count() > 0)
                    {
                        ContentDialog content = new()
                        {
                            Title = "Warning",
                            Content = "Category already exists",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                    }
                    else
                    {
                        ProductCategory category = new ProductCategory();
                        category.Name = CategoryName;
                        db.ProductCategories.Add(category);
                        _ = db.SaveChanges();
                        List = new ObservableCollection<ProductCategory>(db.ProductCategories);
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
