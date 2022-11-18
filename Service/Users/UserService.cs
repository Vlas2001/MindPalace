using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataContext;
using Dto;
using Dto.User;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service.Users;

public class UserService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(DataBaseContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task SignUp(SignUpModel signUpModel)
    {
        var user = _mapper.Map<SignUpModel, User>(signUpModel);
        
        var hashedPassword = PasswordHasher.CalculateHash(signUpModel.Password);
        user.PasswordHash = hashedPassword.HashedPasswordText;
        user.PasswordSalt = hashedPassword.Salt;

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> SignIn(SignInModel signInModel)
    {
        var user = await GetUser(signInModel);    
    
        return user != null ? GenerateJsonWebToken(user) : null;
    }
    
    private string GenerateJsonWebToken(User user)    
    {
        var claims = new[] 
        {    
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),    
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())    
        };    
        
        var keyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var securityKey = new SymmetricSecurityKey(keyBytes);    
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
        
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],    
            _configuration["Jwt:Issuer"],    
            claims,    
            expires: DateTime.Now.AddMinutes(108000),    
            signingCredentials: credentials);    
    
        return new JwtSecurityTokenHandler().WriteToken(token);    
    }    
    
    private async Task<User> GetUser(SignInModel signInModel)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == signInModel.Email);

        return user == null ? null : PasswordHasher.PasswordIsCorrect(
            new HashedPassword
            {
                HashedPasswordText = user.PasswordHash, 
                Salt = user.PasswordSalt
            },
            signInModel.Password) ? user : null;    
    }    
}