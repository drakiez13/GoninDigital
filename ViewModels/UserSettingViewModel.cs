using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using GoninDigital.Models;
using GoninDigital.Utils;
using GoninDigital.Views.DashBoardPages;
using GoninDigital.Properties;

namespace GoninDigital.ViewModels
{
    class UserSettingViewModel : BaseViewModel
    {
        private bool _flag;
        public bool flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
                OnPropertyChanged("flag");
            }

        }
        private string _flag1;
        public string flag1
        {
            get
            {
                return _flag1;
            }
            set
            {
                _flag1 = value;
                OnPropertyChanged("flag1");
            }

        }
        private string _flag2;
        public string flag2
        {
            get
            {
                return _flag2;
            }
            set
            {
                _flag2 = value;
                OnPropertyChanged("flag2");
            }

        }
        private bool _flag3;
        public bool flag3
        {
            get
            {
                return _flag3;
            }
            set
            {
                _flag3 = value;
                OnPropertyChanged("flag3");
            }

        }
        private List<String> _lGender;
        public List<String> lGender
        {
            get
            {
                return new List<string>() { "Other", "Female", "Male" };
            }
        }
        private List<String> _lUserType;
        public List<String> lUserType
        {
            get
            {
                return new List<string>() { "Admin", "Seller", "Customer" };
            }
        }

        private string _usn = "chinh";
        public string usn
        {
            get
            {
                return _usn;
            }
            set
            {
                _usn = value;
                OnPropertyChanged("usn");
            }

        }
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }

        }
        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }

        }
        private string _UserType_index;
        public string UserType_index
        {
            get
            {
                return _UserType_index;
            }
            set
            {
                _UserType_index = value;
                OnPropertyChanged("UserType_index");
            }
        }
        private string _userType;
        public string userType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
                OnPropertyChanged("userType");
            }
        }
        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string _phoneNum;
        public string PhoneNum
        {
            get
            {
                return _phoneNum;
            }
            set
            {
                _phoneNum = value;
                OnPropertyChanged("PhoneNum");
            }
        }
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        private DateTime _dob;
        public DateTime DoB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
                OnPropertyChanged("DoB");
            }
        }
        public bool CanSave => !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(FirstName) &&
            !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(PhoneNum);

        private User user = new User();
        private User user_changed = new User();
        public ICommand EditPCommand { get; set; }
        public ICommand ResetPCommand { get; set; }
        public ICommand SavePCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public UserSettingViewModel()
        {

            flag = true;
            flag1 = "Hidden";
            flag2 = "Visible";
            flag3 = false;
            load_page();

            EditPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditPExecute(); });
            ResetPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetPExecute(); });
            SavePCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { SavePExecute(); });
            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LogOutExcute(); });
        }
        void EditPExecute()
        {
            flag = false;
            flag1 = "Visible";
            flag2 = "Hidden";
            flag3 = true;
        }
        void ResetPExecute()
        {
            load_page();
        }
        private void load_page()
        {
           user = DataProvider.Instance.Db.Users.Where(x => x.UserName == usn).First();
            Id = user.Id.ToString();
            userType = user.TypeId.ToString();
            switch (userType)
            {
                case "1":
                    UserType_index = "0";
                    break;
                case "2":
                    UserType_index = "1";
                    break;
                case "6":
                    UserType_index = "2";
                    break;
            }
            FirstName = user.FirstName.ToString();
            LastName = user.LastName.ToString();
            PhoneNum =user.PhoneNumber.ToString();
            Email = user.Email.ToString();
            Gender = user.Gender.ToString();
            DoB =(DateTime) user.DateOfBirth;
        }
        void SavePExecute()
        {
            if (!CanSave)
            {
                MessageBox.Show("Please enter full information");
            }
            else
            {
                int t;
                if (!int.TryParse(PhoneNum, out t) | PhoneNum[0] != '0')
                {
                    MessageBox.Show("Invalid phone number");
                }
                else
                {
                    if (Email != user.Email.ToString() & DataProvider.Instance.Db.Users.Where(x => x.Email == Email).Count() != 0)
                    {
                        MessageBox.Show("Email already exists");
                    }
                    else
                    {
                        UpdateInfo();
                        MessageBox.Show("Updated Successfully");
                        flag = true;
                        flag1 = "Hidden";
                        flag2 = "Visible";
                        flag3 = false;
                    }
                }
            }
        }
        void UpdateInfo()
        {
            var t = DataProvider.Instance.Db.Users.Where(x => x.UserName == usn).First();
            t.Gender = byte.Parse(Gender);
            t.FirstName = FirstName;
            t.LastName=LastName;
            t.PhoneNumber = PhoneNum;
            t.Email = Email;
            t.DateOfBirth = DoB;
            _ = DataProvider.Instance.Db.SaveChanges();
            load_page();
        }

        void LogOutExcute()
        {
            var loginWindow = new LoginViewModel(Application.Current.MainWindow);
            WindowManager.ChangeWindowContent(Application.Current.MainWindow, loginWindow, Resources.LoginWindowTitle, Resources.LoginControlPath);
        }
    }
}
