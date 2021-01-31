using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MES.Service.Services
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserServices : User.UserBase
    {
        private readonly ILogger<UserServices> _logger;
        public UserServices(ILogger<UserServices> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UserModel> Login(LoginModel request, ServerCallContext context)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "Jeffcky"),
                new Claim(JwtRegisteredClaimNames.Email, "2752154844@qq.com"),
                new Claim(JwtRegisteredClaimNames.Sub, "D21D099B-B49B-4604-A247-71B0518A0B1C"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456"));

            var token = new JwtSecurityToken(
                issuer: "MES",
                audience: "MES",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserModel()
            {
                UserName="123",
                Password="123",
                Domain="456",
                Token=jwtToken

            };
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Authorize]
        public override async Task<UserModel> Logout(LogoutModel request, ServerCallContext context)
        {
            return null;
        }

    }
}
