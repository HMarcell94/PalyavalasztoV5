using System.Linq;
using Palyavalaszto.Data;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Interfaces;


namespace Palyavalaszto.Repository
{
    public class JobRepository : IJobRepository
    {
        private MyWorldDbContext _context;
        public JobRepository(MyWorldDbContext context)
        {
            _context = context;
        }
        public bool JobExists(int id)
        {
            return _context.Jobs.Any(j => j.JobID == id);
        }

        public bool CreateJob(job job)
        {
            _context.Add(job);
            return Save();
        }

        public bool DeleteJob(job job)
        {
            _context.Remove(job);
            return Save();
        }

        public ICollection<job> GetJobs()
        {
            return _context.Jobs.ToList();
        }

        public job GetJob(int id)
        {
            return _context.Jobs.Where(j => j.JobID == id).FirstOrDefault();
        }

        public ICollection<employer> GetEmployersByJob(int jobId)
        {
            return _context.Jobs.Where(j => j.JobID == jobId).Select(j => j.employer).ToList();
        }

        public ICollection<applications> GetApplicationsByJob(int jobId)
        {
            return _context.application.Where(a => a.JobID == jobId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateJob(job job)
        {
            _context.Update(job);
            return Save();
        }
    }
}
