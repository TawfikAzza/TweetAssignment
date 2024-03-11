namespace Domain.DTO;

public class PostTweetDTO
{
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}