namespace Domain;

public class Profile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Bio { get; set; } 
    public List<Tweet> Tweets { get; set; }
  
}