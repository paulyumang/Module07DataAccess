using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//After creating PersonalViewModel Add this
using Module07DataAccess.Model;
using Module07DataAccess.Services;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Module07DataAccess.ViewModel
{
        public class PersonalViewModel : INotifyPropertyChanged
        {
            private readonly PersonalService _personalService;
            public ObservableCollection<Personal> PersonalList { get; set; }

            private bool _isBusy;
            public bool IsBusy
            {
                get => _isBusy;
                set
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }

            private string _statusMessage;
            public string StatusMessage
            {
                get => _statusMessage;
                set
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }

            private string _textColor;
            public string TextColor
            {
                get => _textColor;
                set
                {
                    _textColor = value;
                    OnPropertyChanged();
                }
            }

            public ICommand LoadDataCommand { get; }

            // PersonalViewModel Constructor
            public PersonalViewModel()
            {
                _personalService = new PersonalService();
                PersonalList = new ObservableCollection<Personal>();
                LoadDataCommand = new Command(async () => await LoadData());

                LoadData();
            }

            public async Task LoadData()
            {
                if (IsBusy) return;
                IsBusy = true;
                StatusMessage = "Loading Personal data...";
                TextColor = "#3498db"; // Blue while loading

                try
                {
                    var personals = await _personalService.GetAllPersonalsAsync();
                    PersonalList.Clear();
                    foreach (var personal in personals)
                    {
                        PersonalList.Add(personal);
                    }

                    StatusMessage = "Data Loaded successfully!";
                    TextColor = "#27ae60"; // Green on success
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Failed to load data: {ex.Message}";
                    TextColor = "#e74c3c"; // Red on failure
                }
                finally
                {
                    IsBusy = false;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class PersonalViewModel : INotifyPropertyChanged
    {
        private readonly PersonalService _personalService;
        public ObservableCollection<Personal> PersonalList { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        private string _textColor;
        public string TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }

        // PersonalViewModel Constructor
        public PersonalViewModel()
        {
            _personalService = new PersonalService();
            PersonalList = new ObservableCollection<Personal>();
            LoadDataCommand = new Command(async () => await LoadData());

            LoadData();
        }

        public async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
            StatusMessage = "Loading Personal data...";
            TextColor = "#3498db"; // A neutral color (e.g., blue) while loading

            try
            {
                var personals = await _personalService.GetAllPersonalsAsync();
                PersonalList.Clear();
                foreach (var personal in personals)
                {
                    PersonalList.Add(personal);
                }

                StatusMessage = "Data Loaded successfully!";
                TextColor = "#27ae60"; // Green if successful
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to load data: {ex.Message}";
                TextColor = "#e74c3c"; // Red if there is an error
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
}
