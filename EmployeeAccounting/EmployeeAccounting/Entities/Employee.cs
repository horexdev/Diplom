namespace EmployeeAccounting.Entities;

public class Employee
{
    public Employee(EmployeeData data)
    {
        Data = data;
    }

    public EmployeeData Data { get; set; }
}