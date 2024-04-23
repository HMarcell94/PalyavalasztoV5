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
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<user>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers().Select(u => new UserDto
            {
                UserID = u.UserID,         
                Password = u.Password,
                Email = u.Email,
                RoleID = u.RoleID,   

            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(user))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _userRepository.GetUser(userId);
            var userDto = new UserDto
            {
                UserID = user.UserID,

                Password = user.Password,
                Email = user.Email,
              
                RoleID = user.RoleID,
            
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUsers()
                .Where(u => u.Email.Trim().ToUpper() == userCreate.Email.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEntity = new user
            {
                UserID = userCreate.UserID,
             
                Password = userCreate.Password,
                Email = userCreate.Email,
               
                RoleID = userCreate.RoleID,
            
            };

            if (!_userRepository.CreateUser(userEntity))
            {
                ModelState.AddModelError("", "Something went wrong while saving the user");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.UserID)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userEntity = _userRepository.GetUser(userId);
         
            userEntity.Email = updatedUser.Email;
            userEntity.Password = updatedUser.Password;
        
            userEntity.RoleID = updatedUser.RoleID;
      

            if (!_userRepository.UpdateUser(userEntity))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var userEntity = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userEntity))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record {userEntity.Email}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
