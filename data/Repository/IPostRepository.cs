public interface IPostRepository : IDisposable
{
    Task<List<Post>> GetPostsAsync();
    Task<Post> GetPostAsync(int postId);
    Task InsertPostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task DeletePostAsync(int postId);
    Task SaveAsync();
}