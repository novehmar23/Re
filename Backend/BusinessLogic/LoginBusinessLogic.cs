using BusinessLogicInterfaces;
using Domain;
using DTO;
using RepositoryInterfaces;
using System;
using System.Security.Authentication;

namespace BusinessLogic
{
    public class LoginBusinessLogic : ILoginBusinessLogic
    {
        public ILoginDataAccess loginDataAccess { get; set; }

        public LoginBusinessLogic(ILoginDataAccess newLoginDataAccess)
        {
            loginDataAccess = newLoginDataAccess;
        }

        public LoginResponseDTO Login(string username, string password)
        {
            string validCredentials = loginDataAccess.VerifyUser(username, password);
            if (validCredentials == null)
                throw new AuthenticationException();

            string token = Guid.NewGuid().ToString();
            LoginToken tokenSave = new LoginToken
            {
                Token = token,
                Username = username
            };
            loginDataAccess.SaveLogin(tokenSave);

            LoginResponseDTO response = new LoginResponseDTO
            {
                Token = token,
                Role = validCredentials
            };
            return response;
        }

        public TokenIdDTO GetIdRoleFromToken(string token)
        {
            return loginDataAccess.GetIdRoleFromToken(token);
        }
    }


}