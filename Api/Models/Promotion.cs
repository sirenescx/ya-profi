namespace Api.Models;

public class Promotion
{
    public Promotion()
    {
        Participants = new List<Participant>();
        Prizes = new List<Prize>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Participant> Participants { get; set; }
    
    public ICollection<Prize> Prizes { get; set; }
}