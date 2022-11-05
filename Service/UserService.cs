using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataContext;
using Dto;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class UserService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IMapper mapper, DataBaseContext context, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task RegisterUser(UserRegisterDto userDto)
        {
            if (!CheckLoginExist(userDto.Email))
            {
                await _context.Users.AddAsync(_mapper.Map<UserRegisterDto, User>(userDto));
                await _context.SaveChangesAsync();
            }
        }

        public bool LoginUser(UserLoginDto userDto)
        {
            if (CheckLoginExist(userDto.Email) && CheckPasswordCorrect(userDto.Email, userDto.Password))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Email, userDto.Email)
                };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()).Wait();
                return true;
            }
            return false;
        }

        private bool CheckLoginExist(string email) => 
            _context.Users.Any(user => user.Email == email);

        private bool CheckPasswordCorrect(string email, string password) => 
            _context.Users.FirstOrDefault(user => user.Email == email)?.Password == password;
    }
}