using Palyavalaszto.Data.Entitites;
using System.Collections.Generic;


namespace Palyavalaszto.Interfaces
{
    public interface IUserRepository
    {
        ICollection<user> GetUsers();
        user GetUser(int id);
        ICollection<employee> GetEmployeesByUser(int userId);
        ICollection<employer> GetEmployersByUser(int userId);
        bool UserExists(int id);
        bool CreateUser(user user);
        bool UpdateUser(user user);
        bool DeleteUser(user user);
        bool Save();
    }
}
