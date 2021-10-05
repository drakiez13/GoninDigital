using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GoninDigital.Command;
using GoninDigital.Model;
namespace GoninDigital.ViewModels
{
    class RegisterViewModel: MainViewModel
    {
        public List<String> Gender { get; } = new List<string>() { "Other", "Female", "Male" };
        public List<String> TypeU { get; } = new List<string>() { "Admin", "Seller", "Customer" };
        private string _userName;
        public string userName { get => _userName; set { _userName = value; OnPropertyChanged(nameof(userName)); 
                OnPropertyChanged(nameof(CanRegister)); } }
        private string _passWord;
        public string passWord { get => _passWord; set { _passWord = value; OnPropertyChanged(nameof(passWord));
                OnPropertyChanged(nameof(CanRegister));
            } }
        private string _CpassWord;
        public string CpassWord { get => _CpassWord; set { _CpassWord = value; OnPropertyChanged(nameof(CpassWord));
                OnPropertyChanged(nameof(CanRegister));
            } }
        private string _firstName;
        public string firstName { get => _firstName; set { _firstName = value; OnPropertyChanged(nameof(firstName));
                OnPropertyChanged(nameof(CanRegister));
            } }
        private string _lastName;
        public string lastName { get => _lastName; set { _lastName = value; OnPropertyChanged(nameof(lastName));
                OnPropertyChanged(nameof(CanRegister));
            } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanRegister));
            } }
        private string _phoneNum;
        public string phoneNum { get => _phoneNum; set { _phoneNum = value; OnPropertyChanged(nameof(phoneNum));
                OnPropertyChanged(nameof(CanRegister));
            } }
        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(userName) &&
            !string.IsNullOrEmpty(firstName) &&
            !string.IsNullOrEmpty(lastName) &&
            !string.IsNullOrEmpty(phoneNum) &&
            !string.IsNullOrEmpty(passWord) &&
            !string.IsNullOrEmpty(CpassWord);
        #region Constructor
        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterCommandExecute);
        }
        #endregion
        #region Private Methods
        private void RegisterCommandExecute()
        {
            if (Email)
            {
                MessageBox.Show("Both email and password should be filled in.");
                return;
            }
            if (AccountManager.EmailExists(Email))
            {
                MessageBox.Show(string.Format("The email {0} already exists", Email));
                return;
            }
            var Validator = new PersonalAccountValidator().ValidatePassword(UserModel.Instance.Password);
            if (!Validator.IsValid)
            {
                MessageBox.Show(Validator.ValidationMessage);
                return;
            }
            if (SendEmailCode(window, Resources.RegisterAccountEmailSubject, Resources.GenericEmailContent))
            {
                //CloseAction?.Invoke();
            }
        }
        #endregion
    }
}
