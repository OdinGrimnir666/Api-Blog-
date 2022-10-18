public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; } 
    public DateTime EditDate { get; set; } 
    public User? User { get; set; }
}
