using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Service
{
    public interface IAuthenticationService
    {
        bool Authentication(string name, string password);
    }
}
