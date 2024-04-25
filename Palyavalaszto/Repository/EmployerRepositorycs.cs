using System.Linq;
using Palyavalaszto.Data;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Interfaces;

namespace Palyavalaszto.Repository
{
    public class EmployerRepository : IEmployerRepository
    {
        private MyWorldDbContext _context;
        public EmployerRepository(MyWorldDbContext context)
        {
            _context = context;
        }
        public bool EmployerExists(int id)
        {
            return _context.Employers.Any(e => e.EmployerID == id);
        }

        public bool CreateEmployer(employer employer)
        {
            employer.UserID = _context.users.OrderBy(a => a.UserID).Last().UserID;
            _context.Add(employer);
            return Save();
        }

        public bool DeleteEmployer(employer employer)
        {
            _context.Remove(employer);
            return Save();
        }

        public ICollection<employer> GetEmployers()
        {
            return _context.Employers.ToList();
        }

        public employer GetEmployer(int id)
        {
            return _context.Employers.Where(e => e.EmployerID == id).FirstOrDefault();
        }

        public ICollection<user> GetUsersByEmployer(int employerId)
        {
            return _context.users.Where(u => u.UserID == employerId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployer(employer employer)
        {
            _context.Update(employer);
            return Save();
        }
     

        public ICollection<job> GetJobsByEmployer(int employerId)
        {
            throw new NotImplementedException();
        }
    }
}
