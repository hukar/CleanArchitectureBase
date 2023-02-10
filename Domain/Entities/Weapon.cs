namespace Domain.Entities;

public class Weapon : ISaveableInDb
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
