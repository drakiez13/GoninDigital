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
using GoninDigital.Command;
using GoninDigital.Utils;
using GoninDigital.Views;

namespace GoninDigital.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        Window curWindow;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Properties
        public Action CloseAction { get; set; }
        private string _Usrname;
        public string UserName
        {
            get { return _Usrname; }
            set
            {
                if (_Usrname == value) { return; }
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
                if (_Passwrd == value) { return; }
                _Passwrd = value;
                OnPropertyChanged(Password);
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoadedLoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            LoadedLoginCommand = new RelayCommand<Window>((p)=> { return true; }, (p)=> {
                curWindow = p;
                //p.Hide();
                //Window ps = new();
                //ps.ShowDialog();
                //p.Show();
            });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(curWindow); });
            //RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterCommandExcute(); });
        }
        #endregion

        #region Private Methods
        private void LoginCommandExecute(Window p)
        {
            _Usrname = UserName;
            if (_Usrname == null || _Passwrd == null)
            {
                //MessageBox.Show("Both username and password should be filled in.");
            }
            int accCount = DataProvider.Instance.Db.Users.Where(x => x.UserName == _Usrname && x.Password == _Passwrd).Count();
            if (accCount > 0)
            {
                var homepageViewModel = new MainWindow();
                homepageViewModel.Show();
                curWindow.Close();

                //WindowManager.ChangeWindowContent(p, homepageViewModel, "Homepage GoninDigital", "GoninDigital.Views.DashboardView");
            }
            else
            {
                //MessageBox.Show("Invalid credentials.");
            }
        }
        private void RegisterCommandExcute()
        {
        }
        #endregion
    }
}
