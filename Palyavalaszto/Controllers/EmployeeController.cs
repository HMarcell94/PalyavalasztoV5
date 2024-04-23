using Microsoft.AspNetCore.Mvc;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Dto;
using Palyavalaszto.Interfaces;
using Palyavalaszto.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Palyavalaszto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees().Select(e => new EmployeeDto
            {
                EmployeeID = e.EmployeeID,
                UserID = e.UserID,
                First_Name = e.First_Name,
                LastName = e.Last_Name,
                ContactNumber = e.ContactNumber,
                Portfolio = e.Portfolio,
                Resume = e.Resume,
                Language = e.Language,
                Profession = e.Profession,
                Picture = e.Picture,
                HighestEducation = e.HighestEducation,
                NameofSchool = e.NameofSchool,
                Introduction = e.Introduction,
                PreviousWork = e.PreviousWork,
                Address = e.Address,
               Position = e.Position



            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = _employeeRepository.GetEmployee(employeeId);
            var employeeDto = new EmployeeDto
            {
                EmployeeID = employee.EmployeeID,
                UserID = employee.UserID,
                First_Name = employee.First_Name,
                LastName = employee.Last_Name,
                ContactNumber = employee.ContactNumber,
                Portfolio = employee.Portfolio,
                Resume = employee.Resume,
                HighestEducation = employee.HighestEducation,
                Profession = employee.Profession,
                Picture = employee.Picture,
                Language = employee.Language,
                Introduction = employee.Introduction,
                NameofSchool = employee.NameofSchool,
                PreviousWork = employee.PreviousWork,
                Address = employee.Address,
                Position = employee.Position
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employeeDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeCreate)
        {
            if (employeeCreate == null)
                return BadRequest(ModelState);

            var employee = _employeeRepository.GetEmployees()
                    .Where(e => e.EmployeeID == employeeCreate.EmployeeID)
                    .FirstOrDefault();

            if (employee != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeEntity = new employee
            {
                EmployeeID = employeeCreate.EmployeeID,
                UserID = employeeCreate.UserID,
                First_Name = employeeCreate.First_Name,
                Last_Name = employeeCreate.LastName,
                ContactNumber = employeeCreate.ContactNumber,
                Portfolio = employeeCreate.Portfolio,
                Resume = employeeCreate.Resume,
                Picture = employeeCreate.Picture,
                PreviousWork = employeeCreate.PreviousWork,
                Profession = employeeCreate.Profession,
                Introduction = employeeCreate.Introduction,
                Language = employeeCreate.Language,
                NameofSchool = employeeCreate.NameofSchool,
                HighestEducation = employeeCreate.HighestEducation,
                Address = employeeCreate.Address,
                Position = employeeCreate.Position
            };

            if (!_employeeRepository.CreateEmployee(employeeEntity))
            {
                ModelState.AddModelError("", "Something went wrong while saving the employee");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto updatedEmployee)
        {
            if (updatedEmployee == null)
                return BadRequest(ModelState);

            if (employeeId != updatedEmployee.EmployeeID)
                return BadRequest(ModelState);

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var employeeEntity = _employeeRepository.GetEmployee(employeeId);

            employeeEntity.UserID = updatedEmployee.UserID;
            employeeEntity.First_Name = updatedEmployee.First_Name;
            employeeEntity.Last_Name = updatedEmployee.LastName;
            employeeEntity.ContactNumber = updatedEmployee.ContactNumber;
            employeeEntity.Portfolio = updatedEmployee.Portfolio;
            employeeEntity.Resume = updatedEmployee.Resume;
            employeeEntity.Language = updatedEmployee.Language;
            employeeEntity.NameofSchool = updatedEmployee.NameofSchool;
            employeeEntity.PreviousWork = updatedEmployee.PreviousWork;
            employeeEntity.Introduction = updatedEmployee.Introduction;
            employeeEntity.Picture = updatedEmployee.Picture;
            employeeEntity.HighestEducation = updatedEmployee.HighestEducation;
            employeeEntity.Address = updatedEmployee.Address;   
            employeeEntity.Position = updatedEmployee.Position;

            if (!_employeeRepository.UpdateEmployee(employeeEntity))
            {
                ModelState.AddModelError("", "Something went wrong while updating the employee");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }
        [HttpDelete("{EmployeeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser(int EmployeeId)
        {
            if (!_employeeRepository.EmployeeExists(EmployeeId))
                return NotFound();

            var EmployeeEntity = _employeeRepository.GetEmployee(EmployeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeRepository.DeleteEmployee(EmployeeEntity))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record {EmployeeEntity.EmployeeID}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
