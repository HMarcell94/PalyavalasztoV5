using Palyavalaszto.Data.Entitites;
using System.Collections.Generic;


namespace Palyavalaszto.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<employee> GetEmployees();
        employee GetEmployee(int id);
        ICollection<user> GetUsersByEmployee(int employeeId);
        ICollection<applications> GetApplicationsByEmployee(int employeeId);
        bool EmployeeExists(int id);
        bool CreateEmployee(employee employee);
        bool UpdateEmployee(employee employee);
        bool DeleteEmployee(employee employee);
        bool Save();
    }
}
