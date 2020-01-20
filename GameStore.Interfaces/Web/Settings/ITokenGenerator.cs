using System.Collections.Generic;
using System.Security.Claims;

namespace GameStore.Interfaces.Web.Settings
{
    public interface ITokenGenerator
    {
        string Create(IList<Claim> claims);
    }
}