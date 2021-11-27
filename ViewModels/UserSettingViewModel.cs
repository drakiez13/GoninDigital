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
        #region Properties
        private bool flag;
        public bool Flag
        {
            get { return flag; }
            set { flag = value; OnPropertyChanged("Flag"); }
        }
        private string flag1;
        public string Flag1
        {
            get { return flag1; }
            set { flag1 = value; OnPropertyChanged("Flag1"); }
        }
        private string flag2;
        public string Flag2
        {
            get { return flag2; }
            set { flag2 = value; OnPropertyChanged("Flag2"); }
        }
        private bool flag3;
        public bool Flag3
        {
            get { return flag3; }
            set { flag3 = value; OnPropertyChanged("Flag3"); }
        }
        private List<String> lGender;
        public List<String> LGender
        {
            get { return new List<string>() { "Other", "Female", "Male" }; }
        }
        private List<String> lUserType;
        public List<String> LUserType
        {
            get { return new List<string>() { "Admin", "Vendor", "Customer" }; }
        }
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }
        private string userType_index;
        public string UserType_index
        {
            get { return userType_index; }
            set { userType_index = value; OnPropertyChanged("UserType_index"); }
        }
        private string userType;
        public string UserType
        {
            get { return userType; }
            set { userType = value; OnPropertyChanged("UserType"); }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged("FullName"); }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }
        private string phoneNum;
        public string PhoneNum
        {
            get { return phoneNum; }
            set { phoneNum = value; OnPropertyChanged("PhoneNum"); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }
        private DateTime dob;
        public DateTime DoB
        {
            get { return dob; }
            set { dob = value; OnPropertyChanged("DoB"); }
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
        #endregion
        #region Constructor
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
        }
        #endregion
        #region Private Methods
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
            using (var db = new GoninDigitalDBContext())
            {
                user = db.Users.Where(x => x.UserName == Settings.Default.usrname).First();
            }
            
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
            FullName = FirstName + " " + LastName;
            PhoneNum =user.PhoneNumber.ToString();
            Email = user.Email.ToString();
            Gender = user.Gender.ToString();
            DoB =(DateTime) user.DateOfBirth;
            UserName = user.UserName.ToString();
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
                    bool isEmail;
                    using (var db = new GoninDigitalDBContext())
                    {
                        isEmail = db.Users.Where(x => x.Email == Email).Count() != 0;
                    }
                    if (Email != user.Email.ToString() & isEmail)
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
            using (var db = new GoninDigitalDBContext())
            {
                var t = db.Users.Where(x => x.UserName == Settings.Default.usrname).First();
                t.Gender = byte.Parse(Gender);
                t.FirstName = FirstName;
                t.LastName = LastName;
                t.PhoneNumber = PhoneNum;
                t.Email = Email;
                t.DateOfBirth = DoB;
                _ = db.SaveChanges();
            }
            
            load_page();
        }
        #endregion
    }
}
