using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _pageIcon;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string PageIcon
        {
            get => _pageIcon;
            set => SetProperty(ref _pageIcon, value);
        }

        protected bool SetProperty<T>(ref T backingField, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
