using Palyavalaszto.Data.Entitites;
using System.Collections.Generic;


namespace Palyavalaszto.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<role> GetRoles();
        role GetRole(int id);
        ICollection<user> GetUsersByRole(int roleId);
        bool RoleExists(int id);
        bool CreateRole(role role);
        bool UpdateRole(role role);
        bool DeleteRole(role role);
        bool Save();
    }
}
