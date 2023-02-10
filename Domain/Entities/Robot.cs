namespace Domain.Entities;

public class Robot : Auditable
{
    public int Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
    public List<Weapon> Weapons { get; set; } = new();
}
