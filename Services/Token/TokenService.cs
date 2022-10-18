public class TokenService : ITokenService
{

    private TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);
    public string BuildToken(string key, string issuer, UserDTO userDTO)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userDTO.FirstName),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creadentitals = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer,issuer,claims,
        expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: creadentitals);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

    }
}