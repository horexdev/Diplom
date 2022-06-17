using System.Windows;

namespace EmployeeAccounting.Windows
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();

            Utils.SignInWindow = this;

            SignInButton.Click += SignInButton_Click;

            Closed += SignInWindow_Closed;
        }

        private void SignInWindow_Closed(object? sender, System.EventArgs e)
        {
            Owner.Focus();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            SocketClient.RemoteEvent("Auth:SignIn", login, password);
        }

        public void Authorize(bool success)
        {
            if (success)
            {
                Utils.MainWindow.AccountTypeLabel.Content = "Учётная запись - Расширенная";
                Utils.MainWindow.ExtendedStackPanel.Visibility = Visibility.Visible;

                Close();

                MessageBox.Show("Локальный аккаунт теперь имеет статус - Расширенный", "Успешно");
            }
            else
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
