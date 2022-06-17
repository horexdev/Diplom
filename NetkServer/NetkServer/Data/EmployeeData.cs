namespace NetkServer.Data;

public class EmployeeData
{
    public EmployeeData(string name, string phone, string address, string position, int[]? passportImage, int[]? snilsImage, int[]? insurancePolicyImage)
    {
        Name = name;
        Phone = phone;
        Address = address;
        Position = position;
        PassportImage = passportImage;
        SnilsImage = snilsImage;
        InsurancePolicyImage = insurancePolicyImage;
    }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Position { get; set; }

    public int[]? PassportImage { get; set; }

    public int[]? SnilsImage { get; set; }

    public int[]? InsurancePolicyImage { get; set; }
}