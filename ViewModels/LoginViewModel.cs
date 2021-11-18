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
using GoninDigital.Views;
using ModernWpf.Controls;
using GoninDigital.Properties;

namespace GoninDigital.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        #region Properties
        Window curWindow;
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }
        public Action CloseAction { get; set; }
        private string _Usrname;
        public string UserName
        {
            get { return _Usrname; }
            set
            {
                _Usrname = value;
                OnPropertyChanged(UserName);
            }
        }
        private string _Passwrd;
        public string Password
        {
            get { return _Passwrd; }
            set
            {
                _Passwrd = value;
                OnPropertyChanged(Password);
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            art = "/GoninDigital;component/Resources/Images/LoginImage.jpg";
            curWindow = window;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterCommandExcute(); });
            ResetCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetCommandExcute(); });
        }
        #endregion

        #region Private Methods
        
        private void LoginCommandExecute()
        {
            if (UserName == null || Password == null)
            {
                var content = new ContentDialog();
                content.Title = "Warning";
                content.Content = "Both username and password should be filled in.";
                content.PrimaryButtonText = "Ok";
                content.ShowAsync();
                return;
            }

            string passEncode = Cryptography.MD5Hash(Cryptography.Base64Encode(Password));
            var isExist = DataProvider.Instance.Db.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == passEncode);
            if (isExist != default)
            {
                var dashboardWindow = new DashBoard();
                if (isExist.TypeId == 1) //admin
                {
                    WindowManager.ChangeWindowContent(curWindow, dashboardWindow, "", "GoninDigital.Views.AdminView");
                }
                else //user
                {
                    WindowManager.ChangeWindowContent(curWindow, dashboardWindow, "", "GoninDigital.Views.DashBoard");
                }
            }
            else
            {
                var content = new ContentDialog();
                content.Title = "Warning";
                content.Content = "Invalid credentials.";
                content.PrimaryButtonText = "Ok";
                content.ShowAsync();
            }
        }
        private void RegisterCommandExcute()
        {
            var registerWindow = new RegisterViewModel(curWindow);
            WindowManager.ChangeWindowContent(curWindow, registerWindow, Resources.RegisterAccountWindowTitle, Resources.RegisterAccountControlPath);
            if (registerWindow.CloseAction == null)
            {
                registerWindow.CloseAction = () => curWindow.Close();
            }
        }

        private void ResetCommandExcute()
        {
            var forgotpasswordWindow = new ForgotPasswordViewModel(curWindow);
            WindowManager.ChangeWindowContent(curWindow, forgotpasswordWindow, Resources.ForgotPasswordWindowTitle, Resources.ForgotPasswordControlPath);
            if (forgotpasswordWindow.CloseAction == null)
            {
                forgotpasswordWindow.CloseAction = () => curWindow.Close();
            }
        }
        #endregion
    }
}