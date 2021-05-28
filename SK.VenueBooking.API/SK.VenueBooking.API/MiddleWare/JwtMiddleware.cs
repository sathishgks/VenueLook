using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using SK.VenueBooking.Service;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace SK.VenueBooking.API.MiddleWare
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
           
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var configManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{_appSettings.Issuer}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());

                var openidconfig = configManager.GetConfigurationAsync().Result;

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = _appSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = openidconfig.SigningKeys,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = true,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = (jwtToken.Claims.First(x => x.Type == "unique_name").Value).ToString();
                var name = (jwtToken.Claims.First(x => x.Type == "name").Value).ToString();
                
                context.Items["User"] = username;
                var claimsIdentity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, name));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name,username));
                context.User.AddIdentity(claimsIdentity);
            }
            catch(Exception ex)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
