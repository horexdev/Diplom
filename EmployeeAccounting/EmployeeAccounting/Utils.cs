using System;
using System.Collections.Generic;
using System.Windows;
using EmployeeAccounting.Entities;
using EmployeeAccounting.Windows;
using Newtonsoft.Json;
using Script;

namespace EmployeeAccounting;

public class Utils : ClientScripts
{
    public static MainWindow MainWindow { get; set; }

    public static SignInWindow SignInWindow { get; set; }

    public static AttendanceWindow? AttendanceWindow { get; set; }

    [ClientEvent("Agent:Answer")]
    public void AgentAnswer()
    {
        SocketClient.NotifyAgent();
    }

    [ClientEvent("GetPositions")]
    public void GetPositions(string json)
    {
        var positions = JsonConvert.DeserializeObject<List<PositionData>>(json);
        if (positions == null)
            throw new NullReferenceException("Positions is null");

        MainWindow.PositionComboBox.Dispatcher.Invoke(() =>
        {
            MainWindow.PositionComboBox.ItemsSource = positions;
            MainWindow.PositionComboBox.DisplayMemberPath = "Name";
        });
    }

    [ClientEvent("GetEmployees")]
    public void GetEmployees(string json)
    {
        var employees = JsonConvert.DeserializeObject<List<EmployeeData>>(json);
        if (employees == null)
            throw new NullReferenceException("Employees is null");

        MainWindow.EmployeesComboBox.Dispatcher.Invoke(() =>
        {
            MainWindow.EmployeesComboBox.ItemsSource = employees;
            MainWindow.EmployeesComboBox.DisplayMemberPath = "Name";
        });
    }

    [ClientEvent("Employee:Created")]
    public void EmployeeCreated(bool created)
    {
        if (created)
            MessageBox.Show("Сотрудник успешно добавлен в базу данных");
        else
            MessageBox.Show("Не удалось добавить сотрудника в базу данных.\nОбратитесь к администратору системы.", "", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    [ClientEvent("Employee:Removed")]
    public void EmployeeRemoved(bool removed)
    {
        if (removed)
            MessageBox.Show("Сотрудник успешно удален из базы данных");
        else
            MessageBox.Show("Не удалось удалить сотрудника из базы данных.\nОбратитесь к администратору системы.", "", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    [ClientEvent("Auth:SignIn")]
    public void AdminAuthorize(bool success) => SignInWindow.Authorize(success);

    [ClientEvent("Attendance:LoadHistory")]
    public void LoadAttendanceHistory(string records) => AttendanceWindow?.LoadHistoryOfEmployee(records);
}