using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Graphics;
using Module07DataAccess.Model;
using Module07DataAccess.Services;

namespace Module07DataAccess.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private string _statusMessage;
        private Color _statusMessageColor;

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public Color StatusMessageColor
        {
            get => _statusMessageColor;
            set
            {
                _statusMessageColor = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Employee> EmployeeList { get; set; }

        public ICommand LoadDataCommand { get; }

        public EmployeeViewModel()
        {
            _employeeService = new EmployeeService();
            EmployeeList = new ObservableCollection<Employee>();
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            StatusMessageColor = Colors.Red;  // Default to red for errors or initial state
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Fetch data from the EmployeeService
                var employees = await _employeeService.GetAllEmployeesAsync();

                // Clear existing data
                EmployeeList.Clear();

                // Add fetched employees to the ObservableCollection
                foreach (var employee in employees)
                {
                    EmployeeList.Add(employee);
                }

                StatusMessage = "Data Loaded successfully!";
                StatusMessageColor = Colors.Green; // Green for success
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to load data: {ex.Message}";
                StatusMessageColor = Colors.Red;  // Red for failure
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
