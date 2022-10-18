public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Post, PostDTO>();
        CreateMap<User,UserDTO>();

       
    
    }
}