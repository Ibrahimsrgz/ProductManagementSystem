using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductManagementSystem.MauiClient.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public bool IsNotLoadingAndNotError => !IsLoading && !IsError;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoadingAndNotError));
            }
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set
            {
                _isError = value;
                OnPropertyChanged(nameof(IsError));
                OnPropertyChanged(nameof(IsNotLoadingAndNotError));
            }
        }


        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); OnPropertyChanged(nameof(IsError)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
