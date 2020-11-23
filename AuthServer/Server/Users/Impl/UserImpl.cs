using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthServer.Controllers;
using ClassModel.Connection.Reponsitory.Impl;
using ClassModel.Connection.Sql;
using ClassModel.Hashpass;
using ClassModel.Hashpass.Impl;
using ClassModel.Model.Fiber;
using ClassModel.Model.Respond;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Server.Users.Impl
{
    public class UserImpl:Reponsitory<User>,IUser
    {
        private IConfiguration m_configuration;
        private IHashpass m_hashPass = new HashpassImpl();
        private IOptions<Audience> _settings;
        protected readonly DataContext m_context;
        public UserImpl(DataContext context, IConfiguration configuration, IOptions<Audience> settings) : base(context)
        {
            m_configuration = configuration;
            this._settings = settings;
            m_context = context;
        }
        public dynamic uHashPass(string pass)
        {
            return pass = m_hashPass.hashPass(pass);
        }

        public dynamic login(User users)
        {
            DataRespond data = new DataRespond();
            User us = getAll().FirstOrDefault(m => m.UserName == users.UserName);
            if (us is null)
            {
                data.success = false;
                data.message = "User is not exists";
                return data;
            }
            if (checkUser(users))
            {
                data.success = true;
                data.data = new { token = genToken(us), user = us };
                data.message = "Login success";
            }
            else
            {
                data.success = false;
                data.message = "Password is not correct";
            }
            return data;
        }

        public dynamic updateUserNonePassword(User users)
        {
            DataRespond data = new DataRespond();
            try
            {
                var _User = new User() { 
                    IDUser = users.IDUser, 
                    UserName = users.UserName, 
                    Role = users.Role, 
                    Active = users.Active, 
                    Status = users.Status 
                };
                using (m_context)
                {
                    m_context.Users.Attach(_User);
                    m_context.Entry(_User).Property(X => X.UserName).IsModified = true;
                    m_context.Entry(_User).Property(X => X.Role).IsModified = true;
                    m_context.Entry(_User).Property(X => X.Active).IsModified = true;
                    m_context.Entry(_User).Property(X => X.Status).IsModified = true;
                    m_context.SaveChanges();
                }
                data.success = true;
                data.message = "update success";
            }
            catch (Exception ex)
            {
                data.success = false;
                data.message = ex.ToString();
            }
            return data;
        }

        private Boolean checkUser(User us)
        {
            User user = getAll().Where(m => m.UserName == us.UserName).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            if (m_hashPass.checkPass(user.PassWord, us.PassWord) == true)
            {
                return true;
            }
            return false;
        }
        public Boolean checkUsername(User us)
        {
            User user = getAll().Where(m => m.UserName == us.UserName).FirstOrDefault();
            if (user == null)
                return false;
            else
                return true;
        }
        public dynamic genToken(User user)
        {
        
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = _settings.Value.Iss,
                ValidateAudience = true,
                ValidAudience = _settings.Value.Aud,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromDays(1).TotalSeconds
            };

            return responseJson;
        }
    }
}
