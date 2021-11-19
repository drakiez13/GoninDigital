using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.Utils;
using ModernWpf.Controls;
using GoninDigital.Properties;
using GoninDigital.ViewModels.Validator;

namespace GoninDigital.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        Window curWindow;
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }
        public Action CloseAction { get; set; }
        enum LGender
        {
            Other = 0,
            Female = 1,
            Male = 2
        };
        enum LTypeU
        {
            Admin = 1,
            Seller = 2,
            Customer = 3
        }
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }
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
        private string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        private string _LastName;
        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        private int _Gender = (int)LGender.Male;
        public int Gender
        {
            get => _Gender;
            set
            {
                _Gender = value;
                OnPropertyChanged();
            }
        }
        private DateTime _DoB = DateTime.Now;
        public DateTime DoB
        {
            get => _DoB;
            set
            {
                _DoB = value;
                OnPropertyChanged();
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public RegisterViewModel(Window p)
        {
            art = "/GoninDigital;component/Resources/Images/LoginImage.jpg";
            curWindow = p;
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterExecute(); });
            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelExecute(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        void RegisterExecute()
        {
            if (AccountManager.AccountExists(Email, UserName))
            {
                ContentDialog content = new()
                {
                    Title = "Warning",

                    Content = "Your username or email already exists",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            else
            {
                try
                {
                    var Validator = new PersonalAccountValidator().ValidatePassword(Password);
                    if (!Validator.IsValid)
                    {
                        throw new Exception(Validator.ValidationMessage);
                    }
                    User new_user = new()
                    {
                        UserName = UserName,
                        Password = Cryptography.MD5Hash(Cryptography.Base64Encode(Password)),
                        TypeId = (int)LTypeU.Customer,
                        FirstName = FirstName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        Email = Email,
                        Gender = (byte)Gender,
                        DateOfBirth = DoB
                    };

                    AccountManager.RegisterAccount(new_user);

                    ContentDialog content = new()
                    {
                        Title = "Success",
                        Content = "Congratulations! You are successfully signed up!",
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
                }
                catch (Exception e)
                {
                    ContentDialog content = new()
                    {
                        Title = "Failed",
                        Content = e.Message,
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
                    return;

                }
                //catch () // error database
                //{
                //    ContentDialog content = new()
                //    {
                //        Title = "Failed",
                //        Content = "Sign up failed, please check your information",
                //        PrimaryButtonText = "Ok"
                //    };
                //    content.ShowAsync();
                //}
            }
        }
        void CancelExecute()
        {
            var loginWindow = new LoginViewModel(curWindow);
            WindowManager.ChangeWindowContent(curWindow, loginWindow, Resources.LoginWindowTitle, Resources.LoginControlPath);
        }
    }
}
