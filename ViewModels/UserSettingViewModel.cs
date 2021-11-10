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

namespace GoninDigital.ViewModels
{
    class UserSettingViewModel: BaseViewModel
    {
        private string _usn="chinh";
        public string usn
        {
            get 
            {
                return _usn;
            }
            set
            {
                _usn = value;
                OnPropertyChanged(usn);
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
                OnPropertyChanged(Id);
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
                OnPropertyChanged(Gender);
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
                OnPropertyChanged(FirstName);
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
                OnPropertyChanged(LastName);
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
                OnPropertyChanged(PhoneNum);
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
                OnPropertyChanged(Email);
            }
        }
        private string _dob;
        public string DoB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
                OnPropertyChanged(DoB);
            }
        }
        User user = new User();
        public UserSettingViewModel()
        {
            var t = DataProvider.Instance.Db.Users.Where(x => x.UserName == usn);

            var info= t.Select(s => new { s.Id,s.TypeId,s.FirstName,s.LastName,s.PhoneNumber,s.Email,s.Gender,s.DateOfBirth }).ToList();
            Id = info[0].Id.ToString();
            FirstName =info[0].FirstName.ToString();
            LastName = info[0].LastName.ToString();
            PhoneNum = info[0].PhoneNumber.ToString();
            Email = info[0].Email.ToString();
            Gender = info[0].Gender.ToString();
            DoB = info[0].DateOfBirth.ToString();
        }
       
        
        
    }
}
