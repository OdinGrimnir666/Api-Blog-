public class UserRepository : IUserRepository
{
    private readonly WebApiDb _context;


    public UserRepository(WebApiDb context)
    {
        this._context = context;

    }
    public async Task<User> GetUserAsync(User userModel)
    {
        var User = await _context.Users.Where(u =>
        string.Equals(u.FirstName, userModel.FirstName) &&
        string.Equals(u.Password, userModel.Password)).FirstAsync();
        return User;

    }

    public async Task InsertUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


}