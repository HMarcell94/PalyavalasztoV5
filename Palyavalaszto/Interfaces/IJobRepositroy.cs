using System.Collections.Generic;
using Palyavalaszto.Data.Entitites;

namespace Palyavalaszto.Interfaces
{
    public interface IJobRepository
    {
        ICollection<job> GetJobs();
        job GetJob(int id);
        ICollection<employer> GetEmployersByJob(int jobId);
        ICollection<applications> GetApplicationsByJob(int jobId);
        bool JobExists(int id);
        bool CreateJob(job job);
        bool UpdateJob(job job);
        bool DeleteJob(job job);
        bool Save();
    }
}
