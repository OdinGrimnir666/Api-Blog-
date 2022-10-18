public class WebApiDb : DbContext
{
    public WebApiDb(DbContextOptions<WebApiDb> options) : base(options) { }

    public DbSet<Post> Post => Set<Post>();
    public DbSet<User> Users => Set<User>();
}