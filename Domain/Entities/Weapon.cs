namespace Domain.Entities;

public class Weapon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RobotId { get; set; }
}
