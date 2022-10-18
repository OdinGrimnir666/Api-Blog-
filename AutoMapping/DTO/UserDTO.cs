public class UserDTO
{
    public Guid Id {get;set;}

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public DateTime DateTime { get; set; } = DateTime.Now;
}