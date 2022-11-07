using DTO;

namespace BusinessLogicInterfaces
{
    public interface ILoginBusinessLogic
    {
        public LoginResponseDTO Login(string username, string password);
        TokenIdDTO GetIdRoleFromToken(string token);
    }
}

