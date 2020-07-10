using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Navigation;

namespace Mobile.ViewModels
{
    public class TabPageBaseViewModel : ViewModelBase
    {
        private string _pageIcon;
        
        public string PageIcon
        {
            get => _pageIcon;
            set => SetProperty(ref _pageIcon, value);
        }

        public TabPageBaseViewModel() : base()
        {
            
        }

        public TabPageBaseViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
