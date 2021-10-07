using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using GoninDigital.Command;
using System.ComponentModel;
using GoninDigital.Views;
namespace GoninDigital.ViewModels
{
    class RegisterViewModel: MainViewModel
    {
        private Window window;
        public List<String> LGender { get; } = new List<string>() { "Other", "Female", "Male" };
        public List<String> LTypeU { get; } = new List<string>() { "Admin", "Seller", "Customer" };
        private string _userName;
        public string userName { get => _userName; set { _userName = value; OnPropertyChanged(nameof(userName)); 
                OnPropertyChanged(nameof(CanRegister)); } }
        private string _passWord;
        public string Password { get => _passWord; set { _passWord = value; OnPropertyChanged(nameof(Password));
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
        private string _Gender;
        public string Gender
        {
            get => _Gender; set
            {
                _Gender = value; OnPropertyChanged(nameof(Gender));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        private string _DoB;
        public string DoB
        {
            get => _DoB; set
            {
                _DoB = value; OnPropertyChanged(nameof(DoB));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        private string _TypeUser;
        public string TypeUser
        {
            get => _TypeUser; set
            {
                _TypeUser = value; OnPropertyChanged(nameof(TypeUser));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
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
            !string.IsNullOrEmpty(DoB);
          //  !string.IsNullOrEmpty(Password) &&
          //!string.IsNullOrEmpty(CpassWord);
        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public RegisterViewModel()
        {
            this.window = window;
            RegisterCommand = new RelayCommand(RegisterExecute);
            CancelCommand = new RelayCommand(CancelExecute);
        }
        void RegisterExecute()
        {
            if (!CanRegister)
                MessageBox.Show("Chưa nhập đủ thông tin");
            else
            {
                int a=0;
                if (phoneNum[0] == '0' & int.TryParse(phoneNum, out a))
                {
                    string t = Email.Substring(Email.Length - 10, 10);
                    if (t!="@gmail.com")
                    {
                        MessageBox.Show("Email không hợp lệ");
                    }

                    else
                    {
                        if(Password!=CpassWord)
                        {
                            MessageBox.Show("Password va Confirm Password không khớp");
                        }
                        else
                        {
                            MessageBox.Show("Đăng kí thành công");
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
            this.window.Close();
        }

       
        
    }
}
