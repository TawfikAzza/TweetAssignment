namespace Domain;

public class Profile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Bio { get; set; } 
    public List<ProfileTweet> Tweets { get; set; }
  
}
public class ProfileTweet
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
}