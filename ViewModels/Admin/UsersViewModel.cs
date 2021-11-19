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
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get { return _List; } set { _List = value; OnPropertyChanged(); } }
        private User _SelectedItem;
        public User SelectedItem { get { return _SelectedItem; } set { _SelectedItem = value; OnPropertyChanged(); } }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public UsersViewModel()
        {
            _List = new ObservableCollection<User>(DataProvider.Instance.Db.Users);

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
                var user = DataProvider.Instance.Db.Users.First(x => x.Id == SelectedItem.Id);
                user = SelectedItem;
                DataProvider.Instance.Db.SaveChanges();
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
                var user = DataProvider.Instance.Db.Users.First(x => x.Id == SelectedItem.Id);
                List.Remove(user);
                DataProvider.Instance.Db.Users.Remove(user);
                DataProvider.Instance.Db.SaveChanges();
            });
            #endregion

            #region AddCommand
            AddCommand = new RelayCommand<Object>((p) =>
            {
                return false;
            }, (p) =>
            {
                var user = DataProvider.Instance.Db.Users.First(x => x.Id == SelectedItem.Id);
                user = SelectedItem;
                DataProvider.Instance.Db.SaveChanges();
            });
            #endregion

        }


    }
}