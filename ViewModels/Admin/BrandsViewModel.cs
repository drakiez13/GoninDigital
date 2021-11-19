﻿using ABI.Windows.System;
using GoninDigital.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    public class BrandsViewModel : BaseViewModel
    {
        private ObservableCollection<Brand> _List;
        public ObservableCollection<Brand> List { get { return _List; } set { _List = value; OnPropertyChanged(); } }
        private Brand _SelectedItem;
        public Brand SelectedItem { get { return _SelectedItem; } set { _SelectedItem = value; OnPropertyChanged(); } }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public BrandsViewModel()
        {
            _List = new ObservableCollection<Brand>(DataProvider.Instance.Db.Brands);

            #region UpdateCommand
            UpdateCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                var brand = DataProvider.Instance.Db.Brands.First(x => x.Id == SelectedItem.Id);
                brand = SelectedItem;
                DataProvider.Instance.Db.SaveChanges();
            });
            #endregion

            #region DeleteCommand
            DeleteCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                var brand = DataProvider.Instance.Db.Brands.First(x => x.Id == SelectedItem.Id);
                List.Remove(brand);
                DataProvider.Instance.Db.Brands.Remove(brand);
                DataProvider.Instance.Db.SaveChanges();
            });
            #endregion

            #region AddCommand
            AddCommand = new RelayCommand<Object>((p) =>
            {
                return false;
            }, (p) =>
            {
                var user = DataProvider.Instance.Db.Brands.First(x => x.Id == SelectedItem.Id);
                user = SelectedItem;
                DataProvider.Instance.Db.SaveChanges();
            });
            #endregion

        }
    }
}