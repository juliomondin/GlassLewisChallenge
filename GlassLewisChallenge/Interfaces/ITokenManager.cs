using GlassLewisChallenge.Authentication;
using System.Collections.Generic;


namespace GlassLewisChallenge.Interfaces
{
    public interface ITokenManager
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
