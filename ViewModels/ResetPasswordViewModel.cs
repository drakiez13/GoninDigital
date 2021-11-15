using GoninDigital.Utils;
using GoninDigital.ViewModels.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    class ResetPasswordViewModel : BaseViewModel
    {
        #region Properties
        public Action CloseAction { get; set; }

        private Window window;

        public ICommand ResetCommand { get; set; }
        public ICommand NewPasswordCommand { get; set; }        
        public ICommand ConfirmNewPasswordCommand { get; set; }
        private string usr_mail;
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
        private string _RePassword;
        public string RePassword
        {
            get => _RePassword;
            set
            {
                _RePassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public ResetPasswordViewModel(Window window, string email)
        {
            this.window = window;
            this.usr_mail = email;
            ResetCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetCommandExecute(); });
            NewPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            ConfirmNewPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });
        }
        #endregion

        #region Private Methods
        private void ResetCommandExecute()
        {
            if (Password == null)
            {
                return;
            }
            var Validator = new PersonalAccountValidator().ValidatePassword(Password);

            if (!Validator.IsValid)
            {
                MessageBox.Show(Validator.ValidationMessage);
            }
            else
            {
                AccountManager.ChangePassword(window, this.usr_mail, Password);
                //CloseAction?.Invoke();
            }
        }
        #endregion
    }
}
