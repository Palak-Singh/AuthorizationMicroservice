using AuthorizationService.Models;
using AuthorizationService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        //Check if user exist
        public bool Authentication(string name, string password)
        {
            return _authenticationRepository.Authentication(name, password);
        }
    }
}
