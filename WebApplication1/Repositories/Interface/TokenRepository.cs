﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace WebApplication1.Repositories.Interface
{

    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
           

            //Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //JWT security Token Perameters

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt: Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), //will expire after 15 minutes
                signingCredentials: credentials);

            //Return tokens

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
