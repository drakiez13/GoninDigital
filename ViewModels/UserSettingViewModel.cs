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
using ModernWpf.Controls;

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
        private static string[] userTypes = new string[3] { "Admin", "Vendor", "Customer" };
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string avatar;
        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; OnPropertyChanged("Avatar"); }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }
        private int userType_index;
        public int UserType_index
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
            set { firstName = value; FullName = value + " " + lastName; OnPropertyChanged("FirstName"); }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; FullName = firstName + " " + value; OnPropertyChanged("LastName"); }
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
        public ICommand CancelPCommand { get; set; }
        #endregion
        #region Constructor
        public UserSettingViewModel()
        {
            Flag = true;
            Flag1 = "Hidden";
            Flag2 = "Visible";
            Flag3 = false;
            load_page();
            EditPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditPExecute(); });
            ResetPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetPExecute(); });
            SavePCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { SavePExecute(); });
            CancelPCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelPExecute(); });
        }
        #endregion
        #region Private Methods
        void EditPExecute()
        {
            Flag = false;
            Flag1 = "Visible";
            Flag2 = "Hidden";
            Flag3 = true;
        }
        void CancelPExecute()
        {
            Flag = true;
            Flag1 = "Hidden";
            Flag2 = "Visible";
            Flag3 = false;
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
            UserType_index = user.TypeId;
            switch (UserType_index)
            {
                case 1:
                    UserType_index = 0;
                    break;
                case 2:
                    UserType_index = 1;
                    break;
                case 6:
                    UserType_index = 2;
                    break;
            }
            FirstName = user.FirstName.ToString();
            LastName = user.LastName.ToString();
            PhoneNum =user.PhoneNumber.ToString();
            Email = user.Email.ToString();
            Gender = user.Gender.ToString();
            DoB =(DateTime) user.DateOfBirth;
            UserName = user.UserName.ToString();
            UserType = userTypes[UserType_index-1];
            if(user.Avatar != null)
                Avatar = user.Avatar.ToString();


        }
        void SavePExecute()
        {
            if (!CanSave)
            {
                ContentDialog content = new()
                {
                    Title = "Warning",

                    Content = "Please enter full information",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            else
            {
                int t;
                if (!int.TryParse(PhoneNum, out t) | PhoneNum[0] != '0')
                {
                    ContentDialog content = new()
                    {
                        Title = "Warning",

                        Content = "Invalid phone number",
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
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
                        ContentDialog content = new()
                        {
                            Title = "Warning",

                            Content = "Email already exists",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                    }
                    else
                    {
                        UpdateInfo();
                        ContentDialog content = new()
                        {
                            Title = "Complete",

                            Content = "Updated Successfully",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                        Flag = true;
                        Flag1 = "Hidden";
                        Flag2 = "Visible";
                        Flag3 = false;
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
