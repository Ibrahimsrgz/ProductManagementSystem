using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.MauiClient.Pages
{
    public partial class ProductListPage : ContentPage, INotifyPropertyChanged
    {

        // ObservableCollection ile verileri UI'ye bağlıyoruz
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set { _isError = value; OnPropertyChanged(); }
        }

        public bool IsNotLoadingAndNotError => !IsLoading && !IsError;

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ProductListPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadMockData(); // Mock datayı yükle
        }

        private async void LoadMockData()
        {
            IsLoading = true;
            IsError = false;

            try
            {
                // MOCK DATA kullanımı - Gerçek HTTP isteği yerine elle yazılmış veri
                await Task.Delay(1000); // Simülasyon için gecikme

                var mockItems = new List<Item>
                {
                    new Item { Name = "Mock Item 1" },
                    new Item { Name = "Mock Item 2" },
                    new Item { Name = "Mock Item 3" },
                    new Item { Name = "Mock Item 4" }
                };

                foreach (var item in mockItems)
                {
                    Items.Add(item);
                }

                ErrorMessage = string.Empty; // Hata yoksa mesajı sıfırla
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                IsError = true;
                ErrorMessage = $"Error loading mock data: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /*
        // Eğer gerçek veri ile çalışacaksanız bu bölümü kullanabilirsiniz:
        private async void LoadData()
        {
            IsLoading = true;
            IsError = false;

            try
            {
                string url = "https://api.example.com/items";
                var response = await _httpClient.GetStringAsync(url);

                var items = JsonConvert.DeserializeObject<List<Item>>(response);

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
                else
                {
                    IsError = true;
                    ErrorMessage = "No data returned from server.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                IsError = true;
                ErrorMessage = $"HTTP Error: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
        */

        // INotifyPropertyChanged implementasyonu
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Mock veri modeli
    public class Item
    {
        public string Name { get; set; } // Örnek item özelliği
    }
}
