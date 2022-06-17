namespace EmployeeAccounting.Entities;

public class Position
{
    public Position(PositionData data)
    {
        Data = data;
    }

    public PositionData Data { get; set; }
}