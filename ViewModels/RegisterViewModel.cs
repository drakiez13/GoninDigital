using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GoninDigital.Models;

namespace GoninDigital.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        Window curWindow;
        public Action CloseAction { get; set; }
        enum LGender
        {
            other = 0,
            Female = 1,
            Male = 2
        };
        enum LTypeU
        {
            Admin = 1,
            Saler = 2,
            Customer = 6
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
        private string _Gender;
        public string Gender
        {
            get => _Gender;
            set
            {
                _Gender = value;
                OnPropertyChanged();
            }
        }
        private string _DoB;
        public string DoB
        {
            get => _DoB;
            set
            {
                _DoB = value;
                OnPropertyChanged();
            }
        }
        private string _TypeUser;
        public string TypeUser
        {
            get => _TypeUser;
            set
            {
                _TypeUser = value;
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
        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(UserName) &&
            !string.IsNullOrEmpty(FirstName) &&
            !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(PhoneNumber) &&
            !string.IsNullOrEmpty(DoB) &&
            !string.IsNullOrEmpty(Password) &&
            !string.IsNullOrEmpty(RePassword);

        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand rePasswordChangedCommand { get; set; }
        public RegisterViewModel(Window p)
        {
            curWindow = p;
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterExecute(); });
            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelExecute(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            rePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });
        }
        void RegisterExecute()
        {
            if (!CanRegister)
                MessageBox.Show("Chưa nhập đủ thông tin");
            else
            {
                if (Password != RePassword)
                {
                    MessageBox.Show("Password va Confirm Password không khớp");
                }
                else
                {
                    int checkUsername = DataProvider.Instance.Db.Users.Where(x => x.UserName == UserName).Count();
                    int checkEmail = DataProvider.Instance.Db.Users.Where(x => x.Email == Email).Count();
                    if (checkUsername > 0 || checkEmail > 0)
                        MessageBox.Show("Tên tài khoản hoặc email đã tồn tại");
                    else
                    {
                        MessageBox.Show("Đăng kí thành công");
                        _ = Enum.TryParse(TypeUser, out LTypeU _Usertype);
                        _ = Enum.TryParse(Gender, out LGender _Gendertype);
                        _ = DateTime.TryParse(DoB, out DateTime dmy);

                        User u = new()
                        {
                            Id = 10,
                            UserName = UserName,
                            Password = Password,
                            TypeId = (int)_Usertype,
                            FirstName = FirstName,
                            LastName = LastName,
                            PhoneNumber = PhoneNumber,
                            Email = Email,
                            Gender = (byte)_Gendertype,
                            DateOfBirth = dmy
                        };
                    }
                }
            }
        }
        void CancelExecute()
        {
            curWindow.Close();
        }
    }
}
