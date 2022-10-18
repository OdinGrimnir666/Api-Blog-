public interface IUserRepository : IDisposable
{

    Task<User> GetUserAsync(User userModel);
    Task InsertUserAsync(User user);
    Task SaveAsync();


}