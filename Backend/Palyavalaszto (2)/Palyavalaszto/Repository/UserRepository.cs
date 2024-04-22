    using System.Linq;
    using Palyavalaszto.Data;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Interfaces;


    namespace Palyavalaszto.Repository
    {
        public class UserRepository : IUserRepository
        {
            private MyWorldDbContext _context;
            public UserRepository(MyWorldDbContext context)
            {
                _context = context;
            }
            public bool UserExists(int id)
            {
                return _context.users.Any(u => u.UserID == id);
            }

            public bool CreateUser(user user)
            {
                _context.Add(user);
                return Save();
            }

            public bool DeleteUser(user user)
            {
                _context.Remove(user);
                return Save();
            }

            public ICollection<user> GetUsers()
            {
                return _context.users.ToList();
            }

            public user GetUser(int id)
            {
                return _context.users.Where(u => u.UserID == id).FirstOrDefault();
            }

            public ICollection<employee> GetEmployeesByUser(int userId)
            {
                return _context.Employees.Where(e => e.UserID == userId).ToList();
            }

            public ICollection<employer> GetEmployersByUser(int userId)
            {
                return _context.Employers.Where(e => e.UserID == userId).ToList();
            }

            public bool Save()
            {
                var saved = _context.SaveChanges();
                return saved > 0 ? true : false;
            }

            public bool UpdateUser(user user)
            {
                _context.Update(user);
                return Save();
            }
        }
    }
