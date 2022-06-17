namespace NetkServer.Database.Models;

public class Employee
{
    private Employee() { }

    public Employee(string name, string phone, string address, int positionId, byte[] passportImage, byte[] snilsImage, byte[] insurancePolicyImage)
    {
        Name = name;
        Phone = phone;
        Address = address;
        PositionId = positionId;
        PassportImage = passportImage;
        SnilsImage = snilsImage;
        InsurancePolicyImage = insurancePolicyImage;
        Guid = Guid.NewGuid();
    }

    public int Id { get; set; }

    public int PositionId { get; set; }

    public string Name { get; set; } = "";

    public string Phone { get; set; } = "";

    public string Address { get; set; } = "";

    public Guid Guid { get; set; }

    public byte[]? PassportImage { get; set; }

    public byte[]? SnilsImage { get; set; }

    public byte[]? InsurancePolicyImage { get; set; }

    public virtual Position Position { get; set; }
}