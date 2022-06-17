using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EmployeeAccounting.Entities;
using Newtonsoft.Json;

namespace EmployeeAccounting.Windows
{
    /// <summary>
    /// Interaction logic for AttendanceWindow.xaml
    /// </summary>
    public partial class AttendanceWindow : Window
    {
        public AttendanceWindow()
        {
            InitializeComponent();

            Utils.AttendanceWindow = this;

            EmployeesComboBox.ItemsSource = Utils.MainWindow.EmployeesComboBox.ItemsSource;
            EmployeesComboBox.DisplayMemberPath = "Name";

            EmployeesComboBox.SelectedIndex = 0;
            EmployeesComboBox.SelectionChanged += EmployeesComboBox_SelectionChanged;

            LoadSelectedEmployeeAttendance();
        }

        private void EmployeesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadSelectedEmployeeAttendance();
        }

        private void LoadSelectedEmployeeAttendance()
        {
            var employee = EmployeesComboBox.SelectedItem as EmployeeData;
            if (employee == null)
                return;

            SocketClient.RemoteEvent("Attendance:GetHistory", employee.Name);
        }

        public void LoadHistoryOfEmployee(string records)
        {
            var history = JsonConvert.DeserializeObject<List<HistoryRecord>>(records);
            if (history == null)
            {
                MessageBox.Show("Не удалось получить список посещений выбранного сотрудника", "", MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            var list = history
                .Select(record => 
                    new
                    {
                        ЛичныйНомер = record.Guid, 
                        КогдаЗашёл = record.WhenEnter.ToLocalTime(), 
                        КогдаВышел = record.WhenExit.ToLocalTime(), 
                        ВышелЛи = record.DoesLeave
                    }).Cast<dynamic>().ToList();

            AttendanceDataGrid.ItemsSource = list;
        }
    }
}
