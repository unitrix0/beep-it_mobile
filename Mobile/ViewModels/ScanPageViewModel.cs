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
        private ScanModeEnum _scanMode;
        private bool _scannerVisible;
        private bool _scannerIsScanning;
        private bool _scannerIsAnalyzing;
        private string _barcode;

        public ScanModeEnum ScanMode
        {
            get => _scanMode;
            set => SetProperty(ref _scanMode, value);
        }

        public bool ScannerVisible
        {
            get => _scannerVisible;
            set => SetProperty(ref _scannerVisible, value);
        }

        public bool ScannerIsScanning
        {
            get => _scannerIsScanning;
            set => SetProperty(ref _scannerIsScanning, value);
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
            Title = FontAwesomeIcons.AngleDoubleDown;
            OnBarcodeDetected = new Command<Result>(BarcodeDetected);
            OnStartScan = new Command<ScanModeEnum>(StartScan);
        }

        private void StartScan(ScanModeEnum mode)
        {
            ScanMode = mode;
            ScannerVisible = true;
            ScannerIsScanning = true;
            ScannerIsAnalyzing = true;
        }

        private void BarcodeDetected(Result result)
        {
            Barcode = $"({result.BarcodeFormat}) {result.Text}";
            ScannerVisible = false;
            ScannerIsScanning = false;
            ScannerIsAnalyzing = false;
        }
    }
}
