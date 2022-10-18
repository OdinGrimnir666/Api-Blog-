
public class UserMap : IUserMap
{
    IMapper _mapper;
    public UserMap(IMapper mapper)
    {
        _mapper = mapper;


    }
    public UserDTO GetUserDTO(User user)
    {
        return  _mapper.Map<UserDTO>(user);
    }
}
