public interface IAuthUserService
{
    string GetHashPassword(string password);

    Dictionary<string, string> GetResultAuth(UserDTO userDTO, string token);
}