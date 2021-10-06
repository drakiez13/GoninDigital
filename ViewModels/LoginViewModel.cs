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

namespace GoninDigital.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties
        private Window window;
        public ICommand LoginCommand { get; set; }
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
        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            this.window = window;
            User.Instance.UserName = UserName;
            LoginCommand = new RelayCommand(LoginCommandExecute);
        }
        #endregion

        #region Private Methods
        private void LoginCommandExecute()
        {
            User.Instance.UserName = UserName;
            if (User.Instance.UserName == null || User.Instance.Password == null)
            {
                //MessageBox.Show("Both username and password should be filled in.");
                return;
            }
            if (true)
            {
                this.window = new Window();
                this.window.Show();
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
