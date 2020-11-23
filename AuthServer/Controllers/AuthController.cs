using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthServer.Server.Users;
using ClassModel.Model.Fiber;
using ClassModel.Model.Respond;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AuthController : Controller
    {
        private IOptions<Audience> _settings;
        private IUser _user;
        public AuthController(IOptions<Audience> settings, IUser user)
        {
            this._settings = settings;
            this._user = user;
        }
        [HttpGet("getHashpass")]
        public dynamic getHashpass(string pass)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = _user.uHashPass(pass);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;

        }

        [HttpPost]
        public dynamic Post([FromBody] User user)
        {
            return _user.login(user);
            //DataRespond data = new DataRespond();


            //if (user.UserName == "catcher" && user.PassWord == "123")
            //{
            //    data.success = true;
            //    var now = DateTime.UtcNow;

            //    var claims = new Claim[]
            //    {
            //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            //    };

            //    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));
            //    var tokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = signingKey,
            //        ValidateIssuer = true,
            //        ValidIssuer = _settings.Value.Iss,
            //        ValidateAudience = true,
            //        ValidAudience = _settings.Value.Aud,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        RequireExpirationTime = true,

            //    };

            //    var jwt = new JwtSecurityToken(
            //        issuer: _settings.Value.Iss,
            //        audience: _settings.Value.Aud,
            //        claims: claims,
            //        notBefore: now,
            //        expires: now.Add(TimeSpan.FromMinutes(2)),
            //        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            //    );
            //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //    var responseJson = new
            //    {
            //        access_token = encodedJwt,
            //        expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds
            //    };

            //    data.data = (responseJson);
            //    return data;
            //}
            //else
            //{
            //    data.success = false;
            //    return data;
            //}
        }

    }

    

    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
