
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;

    public DateTime DateTime { get; set; } = DateTime.Now;

    public List<Post>? Post { get; set; } = new();
}