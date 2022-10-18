public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

    public DateTime CreateDate {get;set;} = DateTime.Now;
    public DateTime EditDate {get;set;} =DateTime.Now;

    public Guid UserId {get;set;}
    public User? User {get;set;}
}