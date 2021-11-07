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
        public ICommand LoadedLoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            LoadedLoginCommand = new RelayCommand<Window>((p)=> { return true; }, (p)=> {
                curWindow = p;
            });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(curWindow); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterCommandExcute(); });
        }
        #endregion

        #region Private Methods
        
        private void LoginCommandExecute(Window p)
        {
            _Usrname = UserName;
            if (_Usrname == null || _Passwrd == null)
            {
                MessageBox.Show("Both username and password should be filled in.");
                return;
            }

            string passEncode = Encode.MD5Hash(Encode.Base64Encode(_Passwrd));
            int accCount = DataProvider.Instance.Db.Users.Where(x => x.UserName == _Usrname && x.Password == _Passwrd).Count();
            if (accCount > 0)
            {
                var homepageViewModel = new MainWindow();
                homepageViewModel.Show();
                p.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }
        private void RegisterCommandExcute()
        {
        }
        #endregion
    }
}