public class PostRepository : IPostRepository
{
    private readonly WebApiDb _context;

    public PostRepository(WebApiDb context)
    {
        this._context = context;
    }
    public async Task<List<Post>> GetPostsAsync()
    {
        var answer = await _context.Post.Select(x =>
        new Post()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Text = x.Text,
            CreateDate = x.CreateDate,
            EditDate = x.EditDate,
            User = new User()
            {
                FirstName = x.User.FirstName,
                LastName = x.User.LastName
            }
        }).ToListAsync();
        return answer;

    }



    public async Task<Post> GetPostAsync(int postId) {

        var answer = await _context.Post.Where(x=>x.Id==postId).Select(x =>
        new Post()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Text = x.Text,
            CreateDate = x.CreateDate,
            EditDate = x.EditDate,
            User = new User()
            {
                FirstName = x.User.FirstName,
                LastName = x.User.LastName
            }
        }).FirstAsync();
        return answer;

    }

    public async Task InsertPostAsync(Post post) => await _context.Post.AddAsync(post);

    public async Task UpdatePostAsync(Post post)
    {
        var PostFromDb = await _context.Post.FindAsync(new object[] { post.Id });
        if (PostFromDb == null) return;
        PostFromDb.Text = post.Text;
        PostFromDb.Description = post.Description;
    }

    public async Task DeletePostAsync(int postId)
    {
        var PostFromDb = await _context.Post.FindAsync(new object[] { postId });
        if (PostFromDb == null) return;
        _context.Post.Remove(PostFromDb);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

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