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

namespace GoninDigital.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        #region Properties
        Window curWindow;
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
        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            curWindow = window;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterCommandExcute(); });
        }
        #endregion

        #region Private Methods
        
        private void LoginCommandExecute()
        {
            if (UserName == null || Password == null)
            {
                MessageBox.Show("Both username and password should be filled in.");
                return;
            }

            string passEncode = Encode.MD5Hash(Encode.Base64Encode(Password));
            var isExist = DataProvider.Instance.Db.Users.First(x => x.UserName == UserName && x.Password == passEncode);
            if (isExist != null)
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
                MessageBox.Show("Invalid credentials.");
            }
        }
        private void RegisterCommandExcute()
        {
            var registerWindow = new RegisterViewModel(curWindow);
            WindowManager.ChangeWindowContent(curWindow, registerWindow, "GoninDigital", "GoninDigital.Views.RegisterView");
            if (registerWindow.CloseAction == null)
            {
                registerWindow.CloseAction = () => curWindow.Close();
            }
        }
        #endregion
    }
}