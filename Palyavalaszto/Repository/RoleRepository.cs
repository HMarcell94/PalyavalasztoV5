using System.Linq;
using Palyavalaszto.Data;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Interfaces;


namespace Palyavalaszto.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private MyWorldDbContext _context;
        public RoleRepository(MyWorldDbContext context)
        {
            _context = context;
        }
        public bool RoleExists(int id)
        {
            return _context.Roles.Any(r => r.RoleID == id);
        }

        public bool CreateRole(role role)
        {
            _context.Add(role);
            return Save();
        }

        public bool DeleteRole(role role)
        {
            _context.Remove(role);
            return Save();
        }

        public ICollection<role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public role GetRole(int id)
        {
            return _context.Roles.Where(r => r.RoleID == id).FirstOrDefault();
        }

        public ICollection<user> GetUsersByRole(int roleId)
        {
            return _context.users.Where(u => u.RoleID == roleId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRole(role role)
        {
            _context.Update(role);
            return Save();
        }
    }
}
