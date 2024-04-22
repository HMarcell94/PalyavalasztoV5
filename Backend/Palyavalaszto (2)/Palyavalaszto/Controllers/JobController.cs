using Microsoft.AspNetCore.Authorization;
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
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<job>))]
        public IActionResult GetJobs()
        {
            var jobs = _jobRepository.GetJobs().Select(j => new JobDto
            {
                JobID = j.JobID,
                EmployerID = j.EmployerID,
                JobTitle = j.JobTitle,
                JobDescription = j.JobDescription,
                JobRequirements = j.JobRequirements,
                JobLocation = j.JobLocation,
                MinSalary = j.MinSalary,
                MaxSalary = j.MaxSalary,    
                Short_Summary = j.Short_Summary,
                picture = j.picture,
                Extras = j.Extras
            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobs);
        }

        [HttpGet("{jobId}")]
        [ProducesResponseType(200, Type = typeof(job))]
        [ProducesResponseType(400)]
        public IActionResult GetJob(int jobId)
        {
            if (!_jobRepository.JobExists(jobId))
                return NotFound();

            var job = _jobRepository.GetJob(jobId);
            var jobDto = new JobDto
            {
                JobID = job.JobID,
                EmployerID = job.EmployerID,
                JobTitle = job.JobTitle,
                JobDescription = job.JobDescription,
                JobRequirements = job.JobRequirements,
                JobLocation = job.JobLocation,
                MinSalary = job.MinSalary,
                MaxSalary = job.MaxSalary,
            Short_Summary = job.Short_Summary,
            picture = job.picture,
            Extras = job.Extras
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobDto);
        }
        [HttpPost]
        [Authorize(Roles = "1")] // Csak az 1-es RoleId-vel rendelkező felhasználók férhetnek hozzá
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJob([FromBody] JobDto jobCreate)
        {
            if (jobCreate == null)
                return BadRequest(ModelState);

            var job = _jobRepository.GetJobs()
                    .Where(j => j.JobID == jobCreate.JobID)
                    .FirstOrDefault();

            if (job != null)
            {
                ModelState.AddModelError("", "Job already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobEntity = new job
            {
                JobID = jobCreate.JobID,
                EmployerID = jobCreate.EmployerID,
                JobTitle = jobCreate.JobTitle,
                JobDescription = jobCreate.JobDescription,
                JobRequirements = jobCreate.JobRequirements,
                JobLocation = jobCreate.JobLocation,
                MinSalary = jobCreate.MinSalary,
                MaxSalary = jobCreate.MaxSalary,
                Short_Summary = jobCreate.Short_Summary,
                picture = jobCreate.picture,
                Extras = jobCreate.Extras
            };

            if (!_jobRepository.CreateJob(jobEntity))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {jobEntity.JobTitle}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetJob), new { jobId = jobEntity.JobID }, jobEntity);
        }


        [HttpPut("{jobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateJob(int jobId, [FromBody] JobDto jobUpdate)
        {
            if (jobUpdate == null)
                return BadRequest(ModelState);

            if (jobId != jobUpdate.JobID)
                return BadRequest(ModelState);

            if (!_jobRepository.JobExists(jobId))
                return NotFound();

            var jobEntity = _jobRepository.GetJob(jobId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            jobEntity.EmployerID = jobUpdate.EmployerID;
            jobEntity.JobTitle = jobUpdate.JobTitle;
            jobEntity.JobDescription = jobUpdate.JobDescription;
            jobEntity.JobRequirements = jobUpdate.JobRequirements;
            jobEntity.JobLocation = jobUpdate.JobLocation;
            jobEntity.MinSalary = jobUpdate.MinSalary;
            jobEntity.MaxSalary = jobUpdate.MaxSalary;
            jobEntity.Short_Summary = jobUpdate.Short_Summary;
            jobEntity.picture = jobUpdate.picture;
            jobEntity.Extras = jobUpdate.Extras;

            if (!_jobRepository.UpdateJob(jobEntity))
            {
                ModelState.AddModelError("", $"Something went wrong updating the record {jobEntity.JobTitle}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser(int JobId)
        {
            if (!_jobRepository.JobExists(JobId))
                return NotFound();

            var JobEntity = _jobRepository.GetJob(JobId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_jobRepository.DeleteJob(JobEntity))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record {JobEntity.JobID}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
