using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mobile.Data;
using Mobile.Helpers;
using Mobile.Views;
using Prism.Navigation;
using Xamarin.Forms;
using ZXing;

namespace Mobile.ViewModels
{
    public class ScanPageViewModel : TabPageBaseViewModel
    {
        private ScanModeEnum _runningScanMode;
        private bool _scannerVisible;
        private bool _scannerIsAnalyzing;
        private string _barcode;

        public ScanModeEnum RunningScanMode
        {
            get => _runningScanMode;
            set => SetProperty(ref _runningScanMode, value,
                () => ((Command)OnStartScan).ChangeCanExecute());
        }
        public bool ScannerVisible
        {
            get => _scannerVisible;
            set => SetProperty(ref _scannerVisible, value);
        }
        public bool ScannerIsAnalyzing
        {
            get => _scannerIsAnalyzing;
            set => SetProperty(ref _scannerIsAnalyzing, value);
        }
        public string Barcode
        {
            get => _barcode;
            set => SetProperty(ref _barcode, value);
        }

        public ICommand OnBarcodeDetected { get; }
        public ICommand OnStartScan { get; }


        public ScanPageViewModel()
        {
        }

        public ScanPageViewModel(INavigationService navService) : base(navService)
        {
            OnBarcodeDetected = new Command<Result>(BarcodeDetected);
            OnStartScan = new Command<ScanModeEnum>(StartScan, scanMode => RunningScanMode == scanMode ||
                                                                           RunningScanMode == ScanModeEnum.None);
            ScannerVisible = false;
            Title = "Scannen";
            PageIcon = FontAwesomeIcons.Receipt;
        }

        private void StartScan(ScanModeEnum mode)
        {
            RunningScanMode = mode;
            ScannerVisible = true;
            ScannerIsAnalyzing = true;
        }

        private void BarcodeDetected(Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ScannerVisible = false;
                ScannerIsAnalyzing = false;
                RunningScanMode = ScanModeEnum.None;

                if (result != null) NavigationService.NavigateAsync(nameof(ArticleBasePage));
                //Barcode = $"({result.BarcodeFormat}) {result.Text}";
            });
        }
    }
}
