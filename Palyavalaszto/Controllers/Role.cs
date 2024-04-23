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
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<role>))]
        public IActionResult GetRoles()
        {
            var roles = _roleRepository.GetRoles().Select(r => new RoleDto
            {
                RoleID = r.RoleID,
                RoleName = r.RoleName

            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        [ProducesResponseType(200, Type = typeof(role))]
        [ProducesResponseType(400)]
        public IActionResult GetRole(int roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            var role = _roleRepository.GetRole(roleId);
            var roleDto = new RoleDto
            {
                RoleID = role.RoleID,
                RoleName = role.RoleName
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roleDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRole([FromBody] RoleDto roleCreate)
        {
            if (roleCreate == null)
                return BadRequest(ModelState);

            var role = _roleRepository.GetRoles()
                    .Where(r => r.RoleID == roleCreate.RoleID)
                    .FirstOrDefault();

            if (role != null)
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleEntity = new role
            {
                RoleID = roleCreate.RoleID,
                RoleName = roleCreate.RoleName
            };

            if (!_roleRepository.CreateRole(roleEntity))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {roleEntity.RoleName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetRole), new { roleId = roleEntity.RoleID }, roleEntity);
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateRole(int roleId, [FromBody] RoleDto roleUpdate)
        {
            if (roleUpdate == null)
                return BadRequest(ModelState);

            if (roleId != roleUpdate.RoleID)
                return BadRequest(ModelState);

            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            var roleEntity = _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            roleEntity.RoleName = roleUpdate.RoleName;

            if (!_roleRepository.UpdateRole(roleEntity))
            {
                ModelState.AddModelError("", $"Something went wrong updating the record {roleEntity.RoleName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteRole(int roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            var roleEntity = _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roleRepository.DeleteRole(roleEntity))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record {roleEntity.RoleName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
