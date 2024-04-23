using Palyavalaszto.Data.Entitites;
using System.Collections.Generic;

namespace Palyavalaszto.Interfaces
{
    public interface IApplicationRepository
    {
        ICollection<applications> GetApplications();
        applications GetApplication(int id);
        ICollection<job> GetJobsByApplication(int applicationId);
        ICollection<employee> GetEmployeesByApplication(int applicationId);
        bool ApplicationExists(int id);
        bool CreateApplication(applications application);
        bool UpdateApplication(applications application);
        bool DeleteApplication(applications application);
        bool Save();
    }
}
