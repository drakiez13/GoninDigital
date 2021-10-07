using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

using GoninDigital.Models;
using GoninDigital.Command;
using System.Windows.Controls;

namespace GoninDigital.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties
        private Window window;
        public Action CloseAction { get; set; }
        private string _usrname;
        public string UserName
        {
            get { return _usrname; }
            set
            {
                if (_usrname == value) { return; }
                _usrname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
            }
        }
        private string _passwrd;
        public string Password
        {
            get { return _passwrd; }
            set
            {
                if (_passwrd == value) { return; }
                _passwrd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand Passwordchangedcommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            this.window = window;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(); });
            Passwordchangedcommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { _passwrd = p.Password; });
        }
        #endregion

        #region Private Methods
        private void LoginCommandExecute()
        {
            _usrname = UserName;
            if (_usrname == null || _passwrd == null)
            {
                //MessageBox.Show("Both username and password should be filled in.");
                return;
            }
            int accCount = DataProvider.Instance.Db.Users.Where(x => x.UserName == _usrname && x.Password == _passwrd).Count();
            if (accCount > 0)
            {
                this.window = new Window();
                window.Show();
            }
            else
            {
                //MessageBox.Show("Invalid credentials.");
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
