using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public interface IUserInterface
    {
        Object Login(UserDTO user);
        Result GetUsers();
        Object GetUserType(int userTypeId);
        Object SaveUser(UserDTO user);
        Object UpdateUser(UserDTO user);

        #region JWT Token
        bool IsAnExistingUser(string userName);
        bool IsValidUserCredentials(string userName, string password);
        string GetUserRole(string userName);
        UserDTO getUser(string userName, string password);
        #endregion
    }
}
