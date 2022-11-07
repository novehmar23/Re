using Domain;

namespace BusinessLogicInterfaces
{
    public interface IAdminBusinessLogic : IUserBusinessLogic<Admin>
    {
        Admin Add(Admin newUser);
    }
}

