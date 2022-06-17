using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using EmployeeAccounting.Entities;
using EmployeeAccounting.Windows;
using Microsoft.Win32;

namespace EmployeeAccounting
{
    public partial class MainWindow : Window
    {
        private byte[]? _passportImage;
        private byte[]? _snilsImage;
        private byte[]? _insurancePolicyImage;

        private BitmapImage? _passportBitmap;
        private BitmapImage? _snilsBitmap;
        private BitmapImage? _insurancePolicyBitmap;

        public MainWindow()
        {
            InitializeComponent();

            Utils.MainWindow = this;

            SocketClient.InitiateLoadingScreen(LoadingGrid, LoadingLabel);
            SocketClient.StartAgent();

            LoadEmployees();
            LoadPositions();

            AddEmployeeButton.Click += AddEmployeeButton_Click;
            ViewEmployeeButton.Click += ViewEmployeeButton_Click;
            GoBackButton.Click += GoBackButton_Click;
            CreateEmployeeButton.Click += CreateEmployeeButton_Click;

            PassportLoadButton.Click += PassportLoadButton_Click;
            SnilsLoadButton.Click += SnilsLoadButton_Click;
            InsurancePolicyLoadButton.Click += InsurancePolicyLoadButton_Click;

            EmployeesComboBox.SelectionChanged += EmployeesComboBox_SelectionChanged;

            PassportViewButton.Click += PassportViewButton_Click;
            SnilsViewButton.Click += SnilsViewButton_Click;
            InsurancePolicyViewButton.Click += InsurancePolicyViewButton_Click;

            RemoveEmployeeButton.Click += RemoveEmployeeButton_Click;
            UpdateEmployeeButton.Click += UpdateEmployeeButton_Click;

            SignInButton.Click += SignInButton_Click;
            CheckAttendanceButton.Click += CheckAttendanceButton_Click;
        }

        private void CheckAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (Utils.AttendanceWindow is {IsActive: true}) 
                return;

            var attendanceWindow = new AttendanceWindow()
            {
                Owner = this
            };

            attendanceWindow.Show();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReferenceEquals(AccountTypeLabel.Content, "Учётная запись - Расширенная"))
            {
                var result = MessageBox.Show("Ваш статус аккаунта Расширенный. Хотите выйти?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AccountTypeLabel.Content = "Учётная запись - Стандартная";
                    ExtendedStackPanel.Visibility = Visibility.Collapsed;

                    MessageBox.Show("Статус аккаунт изменен на Стандартный", "Успешно");
                }

                return;
            }

            var signInWindow = new SignInWindow
            {
                Owner = this
            };

            signInWindow.Show();
        }

        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
        }

        private void RemoveEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveEmployee();
        }

        private void InsurancePolicyViewButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewImageWindow(_insurancePolicyBitmap)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.Show();
        }

        private void SnilsViewButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewImageWindow(_snilsBitmap)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.Show();
        }

        private void PassportViewButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewImageWindow(_passportBitmap)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.Show();
        }

        private void EmployeesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var employee = EmployeesComboBox.SelectedItem as EmployeeData;
            if (employee == null)
                return;

            SelectEmployee(employee);
        }

        private void InsurancePolicyLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = OpenLoadImageDialog();
            if (string.IsNullOrEmpty(imagePath))
                return;

            _insurancePolicyImage = ImageConverter.ConvertImageToBinary(imagePath);

            MessageBox.Show("Фотография полиса загружена");
        }

        private void SnilsLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = OpenLoadImageDialog();
            if (string.IsNullOrEmpty(imagePath))
                return;

            _snilsImage = ImageConverter.ConvertImageToBinary(imagePath);

            MessageBox.Show("Фотография снилса загружена");
        }

        private void PassportLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = OpenLoadImageDialog();
            if (string.IsNullOrEmpty(imagePath))
                return;

            _passportImage = ImageConverter.ConvertImageToBinary(imagePath);

            MessageBox.Show("Фотография паспорта загружена");
        }

        private void CreateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeGrid.Visibility = Visibility.Hidden;
            ViewEmployeesGrid.Visibility = Visibility.Hidden;
            GoBackButton.Visibility = Visibility.Hidden;
            AdditionalButtons.Visibility = Visibility.Hidden;

            MainGroupBox.Header = "Панель управления";
            ButtonsStackPanel.Visibility = Visibility.Visible;

            ClearAddEmployeeFields();
        }

        private void ViewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            MainGroupBox.Header = "Просмотр сотрудников";
            ButtonsStackPanel.Visibility = Visibility.Hidden;
            AdditionalButtons.Visibility = Visibility.Hidden;

            AddEmployeeGrid.Visibility = Visibility.Visible;
            ViewEmployeesGrid.Visibility = Visibility.Visible;
            GoBackButton.Visibility = Visibility.Visible;

            var employee = EmployeesComboBox.SelectedItem as EmployeeData;
            if (employee == null)
                return;

            SelectEmployee(employee);
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            MainGroupBox.Header = "Добавление сотрудника";
            ButtonsStackPanel.Visibility = Visibility.Hidden;

            AddEmployeeGrid.Visibility = Visibility.Visible;
            GoBackButton.Visibility = Visibility.Visible;
            AdditionalButtons.Visibility = Visibility.Visible;
        }

        private void SelectEmployee(EmployeeData employee)
        {
            NameTextBox.Text = employee.Name;
            PhoneTextBox.Text = employee.Phone;
            AddressTextBox.Text = employee.Address;

            var index = PositionComboBox.ItemsSource
                .OfType<PositionData?>()
                .TakeWhile(position => position?.Name != employee.Position).Count();

            PositionComboBox.SelectedIndex = index;

            if (employee.PassportImage == null ||
                employee.SnilsImage == null ||
                employee.InsurancePolicyImage == null)
                return;

            var passportBytes = employee.PassportImage.Select(i => (byte)i).ToArray();
            var snilsBytes = employee.SnilsImage.Select(i => (byte)i).ToArray();
            var insuranceBytes = employee.InsurancePolicyImage.Select(i => (byte)i).ToArray();

            _passportBitmap = ImageConverter.ConvertBinaryToImage(passportBytes);
            _snilsBitmap = ImageConverter.ConvertBinaryToImage(snilsBytes);
            _insurancePolicyBitmap = ImageConverter.ConvertBinaryToImage(insuranceBytes);
        }

        private void CreateEmployee()
        {
            if (PositionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            var name = NameTextBox.Text;
            var phone = PhoneTextBox.Text;
            var address = AddressTextBox.Text;
            var position = PositionComboBox.SelectedItem.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address) || position == null ||
                _passportImage == null || _snilsImage == null || _insurancePolicyImage == null)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            SocketClient.RemoteEvent("Employee:Create", name, phone, address, position,
                _passportImage.Select(b => (int)b).ToArray().ToList(),
                _snilsImage.Select(b => (int)b).ToArray().ToList(),
                _insurancePolicyImage.Select(b => (int)b).ToArray().ToList());

            ClearAddEmployeeFields();
            LoadEmployees();
        }

        private void LoadPositions()
        {
            SocketClient.RemoteEvent("Position:Load");
        }

        private void LoadEmployees()
        {
            SocketClient.RemoteEvent("Employee:Load");
        }

        private string OpenLoadImageDialog()
        {
            var openFileDialog = new OpenFileDialog();
            var avatarPath = "";
            if (openFileDialog.ShowDialog() == true)
                avatarPath = openFileDialog.FileName;

            return avatarPath;
        }

        private void ClearAddEmployeeFields()
        {
            NameTextBox.Text = "";
            PhoneTextBox.Text = "";
            AddressTextBox.Text = "";

            _passportImage = null;
            _snilsImage = null;
            _insurancePolicyImage = null;
            _passportBitmap = null;
            _snilsBitmap = null;
            _insurancePolicyBitmap = null;
        }

        private void RemoveEmployee()
        {
            var employee = EmployeesComboBox.SelectedItem as EmployeeData;
            if (employee == null)
            {
                MessageBox.Show("Не удалось найти сотрудника. Обратитесь к администратору системы", "", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            SocketClient.RemoteEvent("Employee:Remove", employee.Name);

            ClearAddEmployeeFields();
            LoadEmployees();
        }

        private void UpdateEmployee()
        {
            var employee = EmployeesComboBox.SelectedItem as EmployeeData;
            if (employee == null)
            {
                MessageBox.Show("Не удалось найти сотрудника. Обратитесь к администратору системы", "", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            var name = NameTextBox.Text;
            var phone = PhoneTextBox.Text;
            var address = AddressTextBox.Text;
            var position = PositionComboBox.SelectedItem as PositionData;
            if (position == null)
            {
                MessageBox.Show("Не удалось найти должность. Обратитесь к администратору системы", "", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            SocketClient.RemoteEvent("Employee:Update", employee);

            MessageBox.Show("Сотрудник успешно обновлен");

            LoadEmployees();

            NameTextBox.Text = employee.Name;
            PhoneTextBox.Text = employee.Phone;
            AddressTextBox.Text = employee.Address;
            PositionComboBox.SelectedIndex = position.Id;

            if (employee.PassportImage == null ||
                employee.SnilsImage == null ||
                employee.InsurancePolicyImage == null)
                return;

            var passportBytes = new byte[employee.PassportImage.Length * sizeof(int)];
            Buffer.BlockCopy(employee.PassportImage, 0, passportBytes, 0, passportBytes.Length);
            var snilsBytes = new byte[employee.SnilsImage.Length * sizeof(int)];
            Buffer.BlockCopy(employee.SnilsImage, 0, snilsBytes, 0, snilsBytes.Length);
            var insuranceBytes = new byte[employee.InsurancePolicyImage.Length * sizeof(int)];
            Buffer.BlockCopy(employee.InsurancePolicyImage, 0, insuranceBytes, 0, insuranceBytes.Length);

            _passportBitmap = ImageConverter.ConvertBinaryToImage(passportBytes);
            _snilsBitmap = ImageConverter.ConvertBinaryToImage(snilsBytes);
            _insurancePolicyBitmap = ImageConverter.ConvertBinaryToImage(insuranceBytes);
        }
    }
}
