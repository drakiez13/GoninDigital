using GoninDigital.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GoninDigital.Properties;
using GoninDigital.SharedControl;

namespace GoninDigital.ViewModels
{
    internal class ForgotPasswordViewModel : EmailNotification, INotifyPropertyChanged
    {
        #region Properties
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value;}
        }
        public Action CloseAction { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private Window window;

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
            }
        }
        #endregion

        #region Constructor
        public ForgotPasswordViewModel(Window window)
        {
            art = "/GoninDigital;component/Resources/Images/ForgotPass.jpg";
            this.window = window;
            SendCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { SendCommandExecute(); });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(); });
        }

        #endregion

        #region Private Methods

        private void LoginCommandExecute()
        {
            var loginViewModel = new LoginViewModel(window);
            WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            if (loginViewModel.CloseAction == null)
            {
                loginViewModel.CloseAction = () => window.Close();
            }
        }

        private void SendCommandExecute()
        {
            if (Email == null)
            {
                MessageBox.Show(string.Format("The email must be filled in.", Email), "Warning");
                return;
            }
            if (!AccountManager.EmailExists(Email))
            {
                MessageBox.Show(string.Format("The email {0} does not match any existing account. You must create an account.", Email), "Warning");
                return;
            }
            if (SendEmailCode(window, Email, Resources.RegisterAccountEmailSubject, Resources.GenericEmailContent))
            {
                //CloseAction?.Invoke();
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
