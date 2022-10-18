
public class MapPost : IMapPost
{
    IMapper _mapper;

    public MapPost()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>());
        _mapper = new Mapper(config);

    }

    public PostDTO GetPostDTO(Post post)
    {
        var postDTO = _mapper.Map<Post, PostDTO>(post);
        return postDTO;
    }

    public List<PostDTO> GetPostsDTO(List<Post> post)
    {

        var PostDto = _mapper.Map<List<Post>, List<PostDTO>>(post);
        return PostDto;


    }
}
