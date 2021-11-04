using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using GoninDigital.Models;

namespace GoninDigital.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string text;
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(text);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel()
        {
            
        }
    }
}
