using GoninDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        #region Properties
        private string searchName;
        public string SearchName
        {
            get { return searchName; }
            set { searchName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<User> list;
        public ObservableCollection<User> List { get { return list; } set { list = value; OnPropertyChanged(); } }
        private User selectedItem;
        public User SelectedItem { get { return selectedItem; } set { selectedItem = value; OnPropertyChanged(); } }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        #endregion
        public UsersViewModel()
        {
            using (var db = new GoninDigitalDBContext())
            {
                list = new ObservableCollection<User>(db.Users);
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
                    var user = db.Users.First(x => x.Id == SelectedItem.Id);
                    user = SelectedItem;
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
                    var user = db.Users.First(x => x.Id == SelectedItem.Id);
                    List.Remove(user);
                    db.Users.Remove(user);
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
                    var user = db.Users.First(x => x.Id == SelectedItem.Id);
                    user = SelectedItem;
                    db.SaveChanges();
                }
                
            });
            #endregion

        }
        #region Methods
        public void SearchChanged()
        {
            if (SearchName == "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    List = new ObservableCollection<User>(db.Users);
                }
            }
        }
        public void SearchUser()
        {
            string s = SearchName.ToLower();
            if (SearchName != "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    List = new ObservableCollection<User>(db.Users);
                }
                int count = 0;
                while (count < List.Count())
                {
                    if (!List[count].UserName.ToLower().Contains(s) & !List[count].FirstName.ToLower().Contains(s) & !List[count].LastName.ToLower().Contains(s))
                        List.RemoveAt(count);
                    else
                        count += 1;
                }
            }
        }
        #endregion
    }
}