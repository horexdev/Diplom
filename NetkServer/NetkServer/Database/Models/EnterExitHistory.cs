namespace NetkServer.Database.Models;

public class EnterExitHistory
{
    public EnterExitHistory() {}

    public EnterExitHistory(int employeeId, Guid guid, DateTime whenEnter, DateTime whenExit, bool doesLeave)
    {
        Guid = guid;
        WhenEnter = whenEnter;
        WhenExit = whenExit;
        DoesLeave = doesLeave;
    }

    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public Guid Guid { get; set; }

    public DateTime WhenEnter { get; set; }

    public DateTime WhenExit { get; set; }

    public bool DoesLeave { get; set; }

    public virtual Employee Employee { get; set; }
}