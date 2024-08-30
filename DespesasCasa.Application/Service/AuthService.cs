using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DespesasCasa.Application.Security;
using DespesasCasa.Domain.Config;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Enum;
using DespesasCasa.Domain.Exceptions;
using DespesasCasa.Domain.Interface.Repository;
using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DespesasCasa.Application.Service;

public class AuthService(IUnitOfWork _unitOfWork, IOptions<ApplicationConfig> _config) : IAuthService
{
    public async Task<AuthDto?> LoginAsync(LoginDto loginDto)
    {
        loginDto.Password = EncryptUtils.EncryptPassword(loginDto.Password ?? string.Empty);

        var user = await _unitOfWork.UserRepository.GetAsync(false, x => x.Email == loginDto.Email && x.Password == loginDto.Password);

        if (user == null)
        {
            throw new AppDomainException("Invalid email or password", ErrorCodeEnum.InvalidEmailPassword);
        }

        return new AuthDto
        {
            Token = GenerateToken(user),
            Email = user.Email
        };
    }

    public string GenerateToken(User user)
    {
        var issuer = _config.Value.JWT?.Issuer;
        var audience = _config.Value.JWT?.Audience;
        var key = _config.Value.JWT?.Key;
        var expirationMinutes = _config.Value.JWT?.ExpirationMinutes;

        if (issuer == null || audience == null || key == null || expirationMinutes == null)
        {
            throw new AppDomainException("JWT configuration is invalid", ErrorCodeEnum.InvalidJWTConfig);
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = HandleClaims(user);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirationMinutes.Value),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> HandleClaims(User user)
    {
        // var claims = new List<Claim>
        // {
        //     new Claim(ClaimTypes.Sid, user.Id.ToString()),
        //     new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
        //     new Claim(ClaimTypes.Role, user.UserGroup?.Name ?? string.Empty),
        //     new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        // };

        // if (user.UserGroup != null && user.UserGroup.UserGroupPermissions != null)
        // {
        //     foreach (var permission in user.UserGroup.UserGroupPermissions)
        //     {
        //         claims.Add(new Claim("Permission", permission.PermissionId));
        //     }
        // }

        // return claims;
        return new List<Claim>();
    }
}
