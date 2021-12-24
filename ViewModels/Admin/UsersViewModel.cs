using GoninDigital.Models;
using GoninDigital.SharedControl;
using ModernWpf.Controls;
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
        private ContentDialog addUserDialog;
        public ContentDialog AddUserDialog
        {
            get { return addUserDialog; }
            set { addUserDialog = value; }
        }
        private ContentDialog deleteUserDialog;
        public ContentDialog DeleteUserDialog
        {
            get { return deleteUserDialog; }
            set { deleteUserDialog = value; }
        }

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
                    db.Users.Update(selectedItem);
                    _= db.SaveChanges();
                }
                ContentDialog content = new()
                {
                    Title = "Success",

                    Content = "Updated Successfully",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();

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
                try
                {
                    DeleteUserDialog = new ContentDialog()
                    {

                        CloseButtonText = "Close",
                        Content = new DeleteUserDialog(SelectedItem.Id),
                        Title = "Add User",

                    };
                    DeleteUserDialog.ShowAsync();
                    for (int i = 0; i < List.Count(); i++)
                        if (List[i].Id == SelectedItem.Id)
                        {
                            List.RemoveAt(i);
                            break;
                        }
                }
                catch
                {
                    ContentDialog content = new()
                    {
                        Title = "Warning",
                        Content = "Unexpected Exception",
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
                }
            });
            #endregion

            #region AddCommand
            AddCommand = new RelayCommand<Object>((p) => true, (p) =>{ AddExec(); });
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
        public void AddExec()
        {
            AddUserDialog = new ContentDialog()
            {

                CloseButtonText = "Close",
                Content = new AddUserDialog(),
                Title = "Add User",

            };
            AddUserDialog.ShowAsync();
            AddUserDialog.CloseButtonClick += AddUserDialog_CloseButtonClick;

        }

        private void AddUserDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            using (var db = new GoninDigitalDBContext())
            {
                List = new ObservableCollection<User>(db.Users);
            }
        }
        #endregion
    }
}