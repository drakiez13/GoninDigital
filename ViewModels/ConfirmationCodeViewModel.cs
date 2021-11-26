using GoninDigital.Properties;
using GoninDigital.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    internal class ConfirmationCodeViewModel : BaseViewModel
    {
        #region Properties

        public Action CloseAction { get; set; }

        public ICommand ContinueCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private ResetPasswordViewModel resetPasswordViewModel;

        private Window window;

        private string usr_mail;

        private string SentConfirmationCode;

        private string _confirmationCode;

        public string ConfirmationCode
        {
            get { return _confirmationCode; }
            set
            {
                _confirmationCode = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public ConfirmationCodeViewModel(Window window, string email, string code)
        {
            this.window = window;
            this.SentConfirmationCode = code;
            this.usr_mail = email;
            ContinueCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ContinueCommandExecute(); });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(); });
        }

        #endregion

        #region Private Methods
        private void LoginCommandExecute()
        {
            var loginViewModel = new LoginViewModel(window);
            WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            //if (loginViewModel.CloseAction == null)
            //{
            //    loginViewModel.CloseAction = () => window.Close();
            //}
        }

        private void ContinueCommandExecute()
        {
            if (ConfirmationCode == SentConfirmationCode)
            {
                //    if (!AccountManager.EmailExists(UserModel.Instance.Email)) //for account register
                //    {
                //        AccountManager.RegisterAccount(UserModel.Instance.Email, UserModel.Instance.Password);
                //        var loginViewModel = new LoginViewModel(window);
                //        WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
                //        if (loginViewModel.CloseAction == null)
                //        {
                //            loginViewModel.CloseAction = () => window.Close();
                //        }
                //        return;
                //    }

                resetPasswordViewModel = new ResetPasswordViewModel(window, usr_mail);
                WindowManager.ChangeWindowContent(window, resetPasswordViewModel, Resources.ResetPasswordWindowTitle, Resources.ResetPasswordControlPath);
                if (resetPasswordViewModel.CloseAction == null)
                {
                    resetPasswordViewModel.CloseAction = () => window.Close();
                }
            }
            else
            {
                MessageBox.Show("Incorrect code. Try again!");
            }
        }
        #endregion
    }
}
