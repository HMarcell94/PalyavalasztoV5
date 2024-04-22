using Microsoft.AspNetCore.Mvc;
using Palyavalaszto.Data.Entitites;
using Palyavalaszto.Dto;
using Palyavalaszto.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace Palyavalaszto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : Controller
    {
        private readonly IEmployerRepository _employerRepository;

        public EmployerController(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<employer>))]
        public IActionResult GetEmployers()
        {
            var employers = _employerRepository.GetEmployers().Select(e => new EmployerDto
            {
                EmployerID = e.EmployerID,
                UserID = e.UserID,
                CompanyName = e.CompanyName,
                CompanyDescription = e.CompanyDescription,
                CompanyLogo = Convert.ToBase64String(System.IO.File.ReadAllBytes(e.CompanyLogo)),
                CompanyLocation = e.CompanyLocation,
                size = e.size,
                enterprisetype = e.enterprisetype,

            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employers);
        }
        [HttpGet("{employerId}")]
        [ProducesResponseType(200, Type = typeof(employer))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployer(int employerId)
        {
            if (!_employerRepository.EmployerExists(employerId))
                return NotFound();

            var employer = _employerRepository.GetEmployer(employerId);
            var employerDto = new EmployerDto
            {
                EmployerID = employer.EmployerID,
                UserID = employer.UserID,
                CompanyName = employer.CompanyName,
                CompanyDescription = employer.CompanyDescription,
                CompanyLogo = employer.CompanyLogo,
                CompanyLocation = employer.CompanyLocation,
                 size = employer.size,
                enterprisetype = employer.enterprisetype

            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employerDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployer([FromBody] EmployerDto employerCreate)
        {
            if (employerCreate == null)
                return BadRequest(ModelState);

            var employer = _employerRepository.GetEmployers()
                    .Where(e => e.EmployerID == employerCreate.EmployerID)
                    .FirstOrDefault();

            if (employer != null)
            {
                ModelState.AddModelError("", "Employer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employerEntity = new employer
            {
                EmployerID = employerCreate.EmployerID,
                UserID = employerCreate.UserID,
                CompanyName = employerCreate.CompanyName,
                CompanyDescription = employerCreate.CompanyDescription,
                CompanyLogo = employerCreate.CompanyLogo,
                CompanyLocation = employerCreate.CompanyLocation,
                size = employerCreate.size,
                enterprisetype = employerCreate.enterprisetype
            };

            if (!_employerRepository.CreateEmployer(employerEntity))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {employerEntity.CompanyName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetEmployer), new { employerId = employerEntity.EmployerID }, employerEntity);
        }

        [HttpPut("{employerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateEmployer(int employerId, [FromBody] EmployerDto employerUpdate)
        {
            if (employerUpdate == null)
                return BadRequest(ModelState);

            if (employerId != employerUpdate.EmployerID)
                return BadRequest(ModelState);

            if (!_employerRepository.EmployerExists(employerId))
                return NotFound();

            var employerEntity = _employerRepository.GetEmployer(employerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            employerEntity.UserID = employerUpdate.UserID;
            employerEntity.CompanyName = employerUpdate.CompanyName;
            employerEntity.CompanyDescription = employerUpdate.CompanyDescription;
            employerEntity.CompanyLogo = employerUpdate.CompanyLogo;
            employerEntity.CompanyLocation = employerUpdate.CompanyLocation;
            employerEntity.size = employerUpdate.size;
            employerEntity.enterprisetype = employerUpdate.enterprisetype;

            if (!_employerRepository.UpdateEmployer(employerEntity))
            {
                ModelState.AddModelError("", $"Something went wrong updating the record {employerEntity.CompanyName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{EmployerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser(int EmployerId)
        {
            if (!_employerRepository.EmployerExists(EmployerId))
                return NotFound();

            var EmployerEntity = _employerRepository.GetEmployer(EmployerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employerRepository.DeleteEmployer(EmployerEntity))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record {EmployerEntity.EmployerID}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
