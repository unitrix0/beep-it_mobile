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
        private bool _isScanning;

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
        public bool IsScanning
        {
            get => _isScanning;
            set => SetProperty(ref _isScanning, value);
        }
        public string Barcode
        {
            get => _barcode;
            set => SetProperty(ref _barcode, value);
        }

        public ICommand OnBarcodeDetected { get; }
        public ICommand OnStartScan { get; }


        public ScanPageViewModel() { }

        public ScanPageViewModel(INavigationService navService) : base(navService)
        {
            OnBarcodeDetected = new Command<Result>(BarcodeDetected);
            OnStartScan = new Command<ScanModeEnum>(StartScan, scanMode => RunningScanMode == scanMode ||
                                                                           RunningScanMode == ScanModeEnum.None);
            Title = "Scannen";
            PageIcon = FontAwesomeIcons.Receipt;
            ScannerVisible = true;
        }

        private void StartScan(ScanModeEnum mode)
        {
            if (RunningScanMode == mode)
            {
                RunningScanMode = ScanModeEnum.None;
                ScannerVisible = false;
                IsScanning = false;
                ScannerIsAnalyzing = false;
                NavigationService.NavigateAsync(nameof(ArticleBasePage));
            }
            else
            {
                RunningScanMode = mode;
                ScannerVisible = true;
                IsScanning = true;
                ScannerIsAnalyzing = true;
            }
        }

        private void BarcodeDetected(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ScannerVisible = false;
                ScannerIsAnalyzing = false;
                IsScanning = false;
                RunningScanMode = ScanModeEnum.None;
                //Barcode = $"({result.BarcodeFormat}) {result.Text}";

                if (result != null)
                {
                    INavigationResult r = await NavigationService.NavigateAsync(nameof(ArticleBasePage),
                        new NavigationParameters {{"barcode", result.Text}});
                }
            });
           

        }
    }
}
