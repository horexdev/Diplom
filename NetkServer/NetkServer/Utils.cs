using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using NetkServer.Data;
using NetkServer.Database;
using NetkServer.Database.Models;
using Newtonsoft.Json;
using Script;

namespace NetkServer;

public class Utils : ServerScripts
{
    [ServerEvent("Agent:Refresh")]
    public void AgentRefresh(Socket handler)
    {
        SocketListener.Send(handler, "Agent:Answer");
    }

    [ServerEvent("Position:Load")]
    public void PositionGet(Socket handler)
    {
        using var context = new EntryContext();
        var positions = context.Positions.ToList();
        var json = JsonConvert.SerializeObject(positions);

        SocketListener.Send(handler, "GetPositions", $"{json}");
    }

    [ServerEvent("Employee:Load")]
    public void EmployeeLoad(Socket handler)
    {
        using var context = new EntryContext();
        var employees = context.Employees
            .Include(e => e.Position).ToList();

        var employeesData = employees.
            Select(employee => new EmployeeData(employee.Name, employee.Phone, employee.Address, employee.Position.Name,
                employee.PassportImage?.Select(b => (int) b).ToArray(),
                employee.SnilsImage?.Select(b => (int) b).ToArray(),
                employee.InsurancePolicyImage?.Select(b => (int) b).ToArray())).ToList();
        var json = JsonConvert.SerializeObject(employeesData);

        SocketListener.Send(handler, "GetEmployees", $"{json}");
    }

    [ServerEvent("Employee:Create")]
    public async void EmpoyeeCreate(Socket handler, string name, string phone, string address, string position, object passport, object snils, object insurance)
    {
        await using var context = new EntryContext();

        var positionId = context.Positions.FirstOrDefault(p => p.Name == position)?.Id ?? 0;

        var passportInt = JsonConvert.DeserializeObject<int[]>(passport.ToString() ?? throw new InvalidOperationException());
        if (passportInt == null)
        {
            SocketListener.Send(handler, "Employee:Created", false);

            return;
        }

        var passportBytes = passportInt.Select(i => (byte)i).ToArray();

        var snilsInt = JsonConvert.DeserializeObject<int[]>(snils.ToString() ?? throw new InvalidOperationException());
        if (snilsInt == null)
        {
            SocketListener.Send(handler, "Employee:Created", false);

            return;
        }

        var snilsBytes = snilsInt.Select(i => (byte)i).ToArray();

        var insuranceInt = JsonConvert.DeserializeObject<int[]>(insurance.ToString() ?? throw new InvalidOperationException());
        if (insuranceInt == null)
        {
            SocketListener.Send(handler, "Employee:Created", false);

            return;
        }

        var insuranceBytes = insuranceInt.Select(i => (byte)i).ToArray();

        var newEmployee = new Employee(name, phone, address, positionId, passportBytes, snilsBytes,
            insuranceBytes);
        context.Employees.Add(newEmployee);
        await context.SaveChangesAsync();

        SocketListener.Send(handler, "Employee:Created", true);
    }

    [ServerEvent("Employee:Remove")]
    public async void EmployeeRemove(Socket handler, string name)
    {
        await using var context = new EntryContext();
        var employee = context.Employees.FirstOrDefault(e => e.Name == name);
        if (employee == null)
        {
            SocketListener.Send(handler, "Employee:Removed", false);

            return;
        }

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();

        SocketListener.Send(handler, "Employee:Removed", true);
    }

    [ServerEvent("Auth:SignIn")]
    public void AuthSignIn(Socket handler, string login, string password)
    {
        if (login != "admin" || password != "admin")
        {
            SocketListener.Send(handler, "Auth:SignIn", false);

            return;
        }

        SocketListener.Send(handler, "Auth:SignIn", true);
    }

    [ServerEvent("Attendance:GetHistory")]
    public void AttendanceGetHistory(Socket handler, string employeeName)
    {
        using var context = new EntryContext();

        var employee = context.Employees.FirstOrDefault(e => e.Name == employeeName);
        if (employee == null)
            return;

        var historyRecordsList = context.EnterExitHistory
            .Select(exh => new HistoryRecord(exh.Guid, exh.WhenEnter, exh.WhenExit, exh.DoesLeave))
            .ToList();
        var json = JsonConvert.SerializeObject(historyRecordsList);

        SocketListener.Send(handler, "Attendance:LoadHistory", json);
    }
}