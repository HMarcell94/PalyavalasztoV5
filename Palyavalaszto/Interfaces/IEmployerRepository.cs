
using Palyavalaszto.Data.Entitites;
using System.Collections.Generic;


namespace Palyavalaszto.Interfaces
{
    public interface IEmployerRepository
    {
        ICollection<employer> GetEmployers();
        employer GetEmployer(int id);
        ICollection<user> GetUsersByEmployer(int employerId);
        ICollection<job> GetJobsByEmployer(int employerId);
        bool EmployerExists(int id);
        bool CreateEmployer(employer employer);
        bool UpdateEmployer(employer employer);
        bool DeleteEmployer(employer employer);
        bool Save();
    }
}
