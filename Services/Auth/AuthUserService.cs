using Microsoft.AspNetCore.Cryptography.KeyDerivation;


public class AuthUserService : IAuthUserService
{
    public string GetHashPassword(string password)
    {
        byte[] salt = new byte[] { 3, 4, 5, 6, 2, 4, 10, 4, 3, 2, 4, 5, 3, 4, 5, 6 };
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password!,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8));
        return hashed.Trim();
    }


    public Dictionary<string, string> GetResultAuth(UserDTO userDTO, string token)
    {
        var answer =  new Dictionary<string, string>() {{ "Id", userDTO.Id.ToString() }, { "Token", token },
        { "UserName", userDTO.FirstName }, { "UserEmail", userDTO.Email }};
        Console.WriteLine(answer);
        return answer;
    }
}