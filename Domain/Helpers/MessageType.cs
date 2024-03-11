namespace Domain.Helpers;

public class UserMessage : ITMessage
{
    public string MessageType { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Bio { get; set; }
}

public class TweetMessage : ITMessage
{
    public string MessageType { get; set; } = "Tweet";
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}