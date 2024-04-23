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
        public class ApplicationController : Controller
        {
            private readonly IApplicationRepository _applicationRepository;

            public ApplicationController(IApplicationRepository applicationRepository)
            {
                _applicationRepository = applicationRepository;
            }

            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<applications>))]
            public IActionResult GetApplications()
            {
                var applications = _applicationRepository.GetApplications().Select(a => new ApplicationsDto
                {
                    ApplicationID = a.ApplicationID,
                    JobID = a.JobID,
                    EmployeeID = a.EmployeeID,
                    ApplicationDate = a.ApplicationDate,
                    ApplicationStatus = a.ApplicationStatus

                }).ToList();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(applications);
            }

            [HttpGet("{applicationId}")]
            [ProducesResponseType(200, Type = typeof(applications))]
            [ProducesResponseType(400)]
            public IActionResult GetApplication(int applicationId)
            {
                if (!_applicationRepository.ApplicationExists(applicationId))
                    return NotFound();

                var application = _applicationRepository.GetApplication(applicationId);
                var applicationDto = new ApplicationsDto
                {
                    ApplicationID = application.ApplicationID,
                    JobID = application.JobID,
                    EmployeeID = application.EmployeeID,
                    ApplicationDate = application.ApplicationDate,
                    ApplicationStatus = application.ApplicationStatus
                };

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(applicationDto);
            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateApplication([FromBody] ApplicationsDto applicationCreate)
            {
                if (applicationCreate == null)
                    return BadRequest(ModelState);

                var application = _applicationRepository.GetApplications()
                    .Where(a => a.ApplicationID == applicationCreate.ApplicationID)
                    .FirstOrDefault();

                if (application != null)
                {
                    ModelState.AddModelError("", "Application already exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var applicationEntity = new applications
                {
                    ApplicationID = applicationCreate.ApplicationID,
                    JobID = applicationCreate.JobID,
                    EmployeeID = applicationCreate.EmployeeID,
                    ApplicationDate = applicationCreate.ApplicationDate,
                    ApplicationStatus = applicationCreate.ApplicationStatus
                };

                if (!_applicationRepository.CreateApplication(applicationEntity))
                {
                    ModelState.AddModelError("", "Something went wrong while saving the application");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }

            [HttpPut("{applicationId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult UpdateApplication(int applicationId, [FromBody] ApplicationsDto updatedApplication)
            {
                if (updatedApplication == null)
                    return BadRequest(ModelState);

                if (applicationId != updatedApplication.ApplicationID)
                    return BadRequest(ModelState);

                if (!_applicationRepository.ApplicationExists(applicationId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();

                var applicationEntity = _applicationRepository.GetApplication(applicationId);

                applicationEntity.JobID = updatedApplication.JobID;
                applicationEntity.EmployeeID = updatedApplication.EmployeeID;
                applicationEntity.ApplicationDate = updatedApplication.ApplicationDate;
                applicationEntity.ApplicationStatus = updatedApplication.ApplicationStatus;

                if (!_applicationRepository.UpdateApplication(applicationEntity))
                {
                    ModelState.AddModelError("", "Something went wrong while updating the application");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully updated");
            }
        [HttpDelete("{ApplicationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteApplication(int ApplicationId)
        {
            if (!_applicationRepository.ApplicationExists(ApplicationId))
                return NotFound();

            var ApplicationEntity = _applicationRepository.GetApplication(ApplicationId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_applicationRepository.DeleteApplication(ApplicationEntity))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record {ApplicationEntity.ApplicationID}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }   
    }
    }
