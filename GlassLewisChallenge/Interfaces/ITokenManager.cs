using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using GlassLewisChallenge.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace GlassLewisChallenge.Interfaces
{
    public interface ITokenManager
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
