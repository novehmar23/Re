using Domain;
using DTO;

namespace RepositoryInterfaces
{
    public interface ILoginDataAccess
    {
        string VerifyUser(string username, string password);
        void SaveLogin(LoginToken loginToken);
        TokenIdDTO GetIdRoleFromToken(string token);
    }
}
