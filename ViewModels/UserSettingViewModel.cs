using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using GoninDigital.Models;
using GoninDigital.Utils;
using GoninDigital.Views.DashBoardPages;
using GoninDigital.Properties;
using ModernWpf.Controls;
using Microsoft.Win32;
using System.IO;
using GoninDigital.Utils;

namespace GoninDigital.ViewModels
{
    class UserSettingViewModel : BaseViewModel
    {
        #region Properties
        private bool flag;
        public bool Flag
        {
            get { return flag; }
            set { flag = value; OnPropertyChanged("Flag"); }
        }
        private string flag1;
        public string Flag1
        {
            get { return flag1; }
            set { flag1 = value; OnPropertyChanged("Flag1"); }
        }
        private string flag2;
        public string Flag2
        {
            get { return flag2; }
            set { flag2 = value; OnPropertyChanged("Flag2"); }
        }
        private bool flag3;
        public bool Flag3
        {
            get { return flag3; }
            set { flag3 = value; OnPropertyChanged("Flag3"); }
        }
        private List<String> lGender = new List<string>() { "Other", "Female", "Male" };
        public List<String> LGender
        {
            get { return lGender; }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(); }
        }
        public bool CanSave => !string.IsNullOrEmpty(User.Email) &&
            !string.IsNullOrEmpty(User.FirstName) &&
            !string.IsNullOrEmpty(User.LastName) &&
            !string.IsNullOrEmpty(User.PhoneNumber);

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(); }
        }
        private string userType;
        public string UserType
        {
            get { return userType; }
            set { userType = value; OnPropertyChanged(); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private User user_changed = new User();
        public ICommand EditPCommand { get; set; }
        public ICommand ResetPCommand { get; set; }
        public ICommand SavePCommand { get; set; }
        public ICommand CancelPCommand { get; set; }
        public ICommand EditAvatarCommand { get; set; }
        #endregion
        #region Constructor
        public UserSettingViewModel()
        {
            Flag = true;
            Flag1 = "Hidden";
            Flag2 = "Visible";
            Flag3 = false;
            load_page();
            EditPCommand = new RelayCommand<Window>((p) => { return Flag; }, (p) => { EditPExecute(); });
            ResetPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetPExecute(); });
            SavePCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { SavePExecute(); });
            CancelPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelPExecute(); });
            EditAvatarCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditAvatarExecute(); });
        }
        #endregion
        #region Private Methods
        public async void EditAvatarExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Image..";

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var linkAvatar = await ImageUploader.UploadAsync(openFileDialog.FileName);
                using (var db = new GoninDigitalDBContext())
                {
                    User.Avatar = linkAvatar;
                    db.Users.Update(User);
                    _ = db.SaveChanges();
                }
            }

        }
        void EditPExecute()
        {
            Flag = false;
            Flag1 = "Visible";
            Flag2 = "Hidden";
            Flag3 = true;
        }
        void CancelPExecute()
        {
            Flag = true;
            Flag1 = "Hidden";
            Flag2 = "Visible";
            Flag3 = false;
        }
        void ResetPExecute()
        {
            load_page();
        }
        private void load_page()
        {
            using (var db = new GoninDigitalDBContext())
            {
                user = db.Users.Where(x => x.UserName == Settings.Default.usrname).First();
                userType = db.UserTypes.First(x => x.Id == user.TypeId).Name;
                gender = user.Gender.ToString();
                Email = user.Email;
            }


        }
        void SavePExecute()
        {
            if (!CanSave)
            {
                ContentDialog content = new()
                {
                    Title = "Warning",

                    Content = "Please enter full information",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            else
            {
                int t;
                if (!int.TryParse(User.PhoneNumber, out t) | User.PhoneNumber[0] != '0')
                {
                    ContentDialog content = new()
                    {
                        Title = "Warning",

                        Content = "Invalid phone number",
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
                }
                else
                {
                    bool isEmail;
                    using (var db = new GoninDigitalDBContext())
                    {
                        isEmail = db.Users.Where(x => x.Email == User.Email).Count() != 0;
                    }
                    if (Email != user.Email.ToString() & isEmail)
                    {
                        ContentDialog content = new()
                        {
                            Title = "Warning",

                            Content = "Email already exists",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                    }
                    else
                    {
                        UpdateInfo();
                        ContentDialog content = new()
                        {
                            Title = "Complete",

                            Content = "Updated Successfully",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                        Flag = true;
                        Flag1 = "Hidden";
                        Flag2 = "Visible";
                        Flag3 = false;
                    }
                }
            }
        }
        void UpdateInfo()
        {
            using (var db = new GoninDigitalDBContext())
            {
                db.Users.Update(User);
                _ = db.SaveChanges();
            }
            load_page();
        }
        #endregion
    }
}
