using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : TabPageBase
    {
        public ScanPage()
        {
            InitializeComponent();
        }
    }
}