using System.Net.Sockets;
using NetkServer.Database;
using NetkServer.Database.Models;
using Script;

namespace NetkServer.Api;

public class RemoteApi : ServerScripts
{
    [ServerEvent("TagEmployeeEnter")]
    public void TagEmployeeEnter(Socket handler, Guid guid)
    {
        using var context = new EntryContext();

        var employee = context.Employees.FirstOrDefault(e => e.Guid == guid);
        if (employee == null)
        {
            SocketListener.Send(handler, "TagEmployeeEnter:Error", "ERROR AT FIND EMPLOYEE BY GUID");

            return;
        }

        var record = new EnterExitHistory(employee.Id, employee.Guid, DateTime.Now, new DateTime(0, 0, 0), false);

        context.EnterExitHistory.Add(record);
        context.SaveChanges();

        SocketListener.Send(handler, "TagEmployeeEnter:Success", "ALL DONE");
    }

    [ServerEvent("TagEmployeeExit")]
    public void TagEmployeeExit(Socket handler, Guid guid)
    {
        using var context = new EntryContext();

        var record = context.EnterExitHistory.LastOrDefault(e => e.Guid == guid);
        if (record == null)
        {
            SocketListener.Send(handler, "TagEmployeeEnter:Error", "ERROR AT FIND EMPLOYEE BY GUID");

            return;
        }

        record.DoesLeave = true;
        record.WhenExit = DateTime.Now;

        context.EnterExitHistory.Update(record);
        context.SaveChanges();

        SocketListener.Send(handler, "TagEmployeeEnter:Success", "ALL DONE");
    }
}