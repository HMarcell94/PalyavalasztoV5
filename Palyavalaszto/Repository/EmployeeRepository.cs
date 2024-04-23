using System.Linq;
using Microsoft.EntityFrameworkCore;
using Palyavalaszto.Data;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Interfaces;


namespace Palyavalaszto.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MyWorldDbContext _context;
        public EmployeeRepository(MyWorldDbContext context)
        {
            _context = context;
        }
        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }

        public bool CreateEmployee(employee employee)
        {
            _context.Employees.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(employee employee)
        {
            _context.Employees.Remove(employee);
            return Save();
        }

        public ICollection<employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public employee GetEmployee(int id)
        {   
            return _context.Employees.Where(e => e.EmployeeID == id).FirstOrDefault();
        }

        public ICollection<user> GetUsersByEmployee(int employeeId)
        {
            return _context.users.Where(u => u.UserID == employeeId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployee(employee employee)
        {
            _context.Employees.Update(employee);
            return Save();
        }
        public ICollection<applications> GetApplicationsByEmployee(int employeeId)
        {
            return _context.application.Where(a => a.EmployeeID == employeeId).ToList();
        }
    }
}
