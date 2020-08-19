using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mobile.Abstractions;
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
        private readonly IArticleService _articles;
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

        public ScanPageViewModel(INavigationService navService, IArticleService articles) : base(navService)
        {
            _articles = articles;
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
                if(RunningScanMode == ScanModeEnum.None) return;

                RunningScanMode = ScanModeEnum.None;
                ScannerVisible = false;
                ScannerIsAnalyzing = false;
                IsScanning = false;
                //Barcode = $"({result.BarcodeFormat}) {result.Text}";
                IsBusy = true;
                if (result != null)
                {
                    Console.WriteLine("------------------------------- Barcode found -------------------------------\n");
                    INavigationResult r = await NavigationService.NavigateAsync($"{nameof(ArticleBasePage)}",
                        new NavigationParameters { { "barcode", result.Text } });
                }
            });
        }
    }
}
