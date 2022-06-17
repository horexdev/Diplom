using System;

namespace EmployeeAccounting.Entities;

public struct HistoryRecord
{
    public HistoryRecord(Guid guid, DateTime whenEnter, DateTime whenExit, bool doesLeave)
    {
        Guid = guid;
        WhenEnter = whenEnter;
        WhenExit = whenExit;
        DoesLeave = doesLeave;
    }

    public Guid Guid { get; set; }

    public DateTime WhenEnter { get; set; }

    public DateTime WhenExit { get; set; }

    public bool DoesLeave { get; set; }
}