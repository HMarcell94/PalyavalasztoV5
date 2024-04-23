using System.Linq;
using Palyavalaszto.Data;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Interfaces;


namespace Palyavalaszto.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private MyWorldDbContext _context;
        public ApplicationRepository(MyWorldDbContext context)
        {
            _context = context;
        }
        public bool ApplicationExists(int id)
        {
            return _context.application.Any(a => a.ApplicationID == id);
        }

        public bool CreateApplication(applications application)
        {
            _context.Add(application);
            return Save();
        }

        public bool DeleteApplication(applications application)
        {
            _context.Remove(application);
            return Save();
        }

        public ICollection<applications> GetApplications()
        {
            return _context.application.ToList();
        }

        public applications GetApplication(int id)
        {
            return _context.application.Where(a => a.ApplicationID == id).FirstOrDefault();
        }

        public ICollection<job> GetJobsByApplication(int applicationId)
        {
            return _context.Jobs.Where(j => j.JobID == applicationId).ToList();
        }

        public ICollection<employee> GetEmployeesByApplication(int applicationId)
        {
            return _context.Employees.Where(e => e.EmployeeID == applicationId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateApplication(applications application)
        {
            _context.Update(application);
            return Save();
        }
    }
}
