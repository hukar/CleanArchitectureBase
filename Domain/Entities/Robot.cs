namespace Domain.Entities;

public class Robot
{
    public int Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public List<Weapon> Weapons { get; set; } = new();
}
