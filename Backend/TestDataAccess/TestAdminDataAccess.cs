using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;

namespace TestDataAccess
{
    [TestClass]
    public class TestAdminDataAccess : TestUserDataAccess<Admin>
    {
        public TestAdminDataAccess() : base()
        {
            userDataAccess = new AdminDataAccess(bugManagerContext);
            user = new Admin();
            users = bugManagerContext.Admins;
            userDifferentRole = new Tester();
            role = Roles.Admin;
        }

    }
}
