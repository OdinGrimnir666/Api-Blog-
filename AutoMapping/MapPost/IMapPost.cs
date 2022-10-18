public interface IMapPost
{
        List<PostDTO> GetPostsDTO(List<Post> post);

        PostDTO GetPostDTO(Post post);
}
