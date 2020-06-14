using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mobile.Helpers;
using Xamarin.Forms;
using ZXing;

namespace Mobile.ViewModels
{
    class ScanPageViewModel : BaseViewModel
    {
        private ScanModeEnum _runningScanMode;
        private bool _scannerVisible;
        private bool _scannerIsAnalyzing;
        private string _barcode;

        public ScanModeEnum RunningScanMode
        {
            get => _runningScanMode;
            set => SetProperty(ref _runningScanMode, value, nameof(RunningScanMode), 
                () => ((Command) OnStartScan).ChangeCanExecute());
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
            OnBarcodeDetected = new Command<Result>(BarcodeDetected);
            OnStartScan = new Command<ScanModeEnum>(StartScan, scanMode => RunningScanMode == scanMode ||
                                                                           RunningScanMode == ScanModeEnum.None);
            ScannerVisible = false;
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

                if (result != null)
                    Barcode = $"({result.BarcodeFormat}) {result.Text}";
            });
        }
    }
}
