namespace Domain;

public class Profile
{
    public int Id { get; set; }
    public string Usermame { get; set; }
    public string Bio { get; set; } 
    public List<Tweet> Tweets { get; set; }
  
}