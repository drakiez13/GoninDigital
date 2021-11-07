using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using GoninDigital.Views;
using GoninDigital.Models;
using System.Windows.Controls;

namespace GoninDigital.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public List<String> LGender { get; } = new List<string>() { "Other", "Female", "Male" };
        public List<String> LTypeU { get; } = new List<string>() { "Admin", "Saler", "Customer" };
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
        public string PhoneNumer
        {
            get => _PhoneNumber; 
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        public bool CanRegister => !string.IsNullOrEmpty(_Email) &&
            !string.IsNullOrEmpty(_UserName) &&
            !string.IsNullOrEmpty(_FirstName) &&
            !string.IsNullOrEmpty(_LastName) &&
            !string.IsNullOrEmpty(_PhoneNumber) &&
            !string.IsNullOrEmpty(_DoB) &&
            !string.IsNullOrEmpty(_Password) &&
            !string.IsNullOrEmpty(_RePassword);
        private int type_User;
        private int type_Gender;

        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand rePasswordChangedCommand { get; set; }
        public RegisterViewModel()
        {
            //RegisterCommand = new RelayCommand(RegisterExecute);
            //CancelCommand = new RelayCommand(CancelExecute);
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            rePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });
        }
        void RegisterExecute()
        {
            if (!CanRegister)
                MessageBox.Show("Chưa nhập đủ thông tin");
            else
            {
                int a = 0;
                if (_PhoneNumber[0] == '0' & int.TryParse(_PhoneNumber, out a))
                {
                    string t = Email.Substring(Email.Length - 10, 10);
                    if (t != "@gmail.com")
                    {
                        MessageBox.Show("Email không hợp lệ");
                    }

                    else
                    {
                        if (_Password != _RePassword)
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
                                switch (TypeUser)
                                {
                                    case "Admin":
                                        type_User = 1;
                                        break;
                                    case "Saler":
                                        type_User = 2;
                                        break;
                                    case "Customer":
                                        type_User = 6;
                                        break;
                                }
                                switch (Gender)
                                {
                                    case "Female":
                                        type_Gender = 0;
                                        break;
                                    case "Maler":
                                        type_Gender = 1;
                                        break;
                                    case "Others":
                                        type_Gender = 2;
                                        break;
                                }
                                DateTime dmy;
                                DateTime.TryParse(DoB, out dmy);
                                User u = new()
                                {
                                    Id = 10,
                                    UserName = _UserName,
                                    Password = _Password,
                                    TypeId = type_User,
                                    FirstName = _FirstName,
                                    LastName = _LastName,
                                    PhoneNumber = _PhoneNumber,
                                    Email = _Email,
                                    Gender = (byte)type_Gender,
                                    DateOfBirth = dmy
                                };

                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("SDT không hợp lệ");
                }
            }
        }
        void CancelExecute()
        {
        }
    }
}