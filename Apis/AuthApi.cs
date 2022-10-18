
public class AuthApi : IApi
{

    public void Register(WebApplication app)
    {
        app.MapPost("/login", Login);

        app.MapPost("/register", Registerr);
    }



    [AllowAnonymous]
    private IResult Registerr([FromBody] User user, HttpContext context,
    ITokenService tokenService, IUserRepository userRepository, IAuthUserService authUserService)
    {
        user.Password =  authUserService.GetHashPassword(user.Password);

        userRepository.InsertUserAsync(user);
        userRepository.SaveAsync();
        return Results.StatusCode(StatusCodes.Status201Created);
    }


    [AllowAnonymous]
    
    private IResult Login([FromBody] User user, HttpContext context,
    ITokenService tokenService, IUserRepository userRepository, IAuthUserService authUserService,
    IConfiguration configuration,IUserMap mapper)
    {
        user.Password = authUserService.GetHashPassword(user.Password);
        user = userRepository.GetUserAsync(user).Result;
        if (user == null) return Results.Unauthorized();
        var UserDTO = mapper.GetUserDTO(user);
        var token = tokenService.BuildToken(configuration["Jwt:Key"],
            configuration["Jwt:Issuer"], UserDTO);

        return Results.Ok(authUserService.GetResultAuth(UserDTO, token));
    }
}