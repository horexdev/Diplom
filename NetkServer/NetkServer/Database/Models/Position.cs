namespace NetkServer.Database.Models;

public class Position
{
    private Position() { }

    public Position(string name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = "";
}