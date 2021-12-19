using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Properties;
using Microsoft.EntityFrameworkCore;
using GoninDigital.SharedControl;
using ModernWpf.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using GoninDigital.Utils;
using System.Windows.Controls;
using System.Windows.Media;

namespace GoninDigital.ViewModels
{
    class UserSettingViewModel : BaseViewModel
    {
        #region Properties
        private bool flag;
        public bool Flag
        {
            get { return flag; }
            set { flag = value; OnPropertyChanged(); }
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
        private ContentDialog changePassDialog;
        public ContentDialog ChangePassDialog
        {
            get { return changePassDialog; }
            set { changePassDialog = value;}
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
        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value; OnPropertyChanged(); }
        }
        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; OnPropertyChanged(); }
        }
        private string confirmNewPassword;
        public string ConfirmNewPassword
        {
            get { return confirmNewPassword; }
            set { confirmNewPassword = value; OnPropertyChanged(); }
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
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand CancelDialogCommand { get; set; }
        #endregion
        #region Constructor
        public UserSettingViewModel()
        {
            Flag = true;
            load_page();
            EditPCommand = new RelayCommand<Window>((p) => { return Flag; }, (p) => { EditPExecute(); });
            ResetPCommand = new RelayCommand<Window>((p) => { return !Flag; }, (p) => { load_page(); });
            SavePCommand = new RelayCommand<Window>((p) => { return !Flag; }, (p) => { SavePExecute(); });
            CancelPCommand = new RelayCommand<Window>((p) => { return !Flag; }, (p) => { CancelPExecute(); });
            EditAvatarCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditAvatarExecute(); });
            ChangePasswordCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ChangePasswordExecute(); });
            CancelDialogCommand = new RelayCommand<object>((p) => { return true; }, (p) => { changePassDialog.Hide(); });
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
                    User = db.Users.Where(x => x.UserName == Settings.Default.usrname).First();
                }
            }

        }
        public void ChangePasswordExecute()
        {
            if (changePassDialog == null)
            {
                changePassDialog = new ContentDialog()
                {

                    CloseButtonText = "Close",
                    Content = new ChangePasswordDialog(),
                    Title = "Change Password",
                };
            }
            changePassDialog.ShowAsync();
        }
        void EditPExecute()
        {
            Flag = false;
        }
        void CancelPExecute()
        {

            Flag = true;
        }
        private void load_page()
        {
            using (var db = new GoninDigitalDBContext())
            {
                User = db.Users.Where(x => x.UserName == Settings.Default.usrname).First();
                UserType = db.UserTypes.First(x => x.Id == user.TypeId).Name;
                Gender = user.Gender.ToString();
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
