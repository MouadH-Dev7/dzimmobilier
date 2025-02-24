using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> usersList = Users.GetAllUsers();
            if (usersList.Count == 0)
            {
                return NotFound("No users found!");
            }
            return Ok(usersList);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Users user = Users.Find(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> AddUser(UserDTO newUserDTO)
        {
            if (newUserDTO == null || string.IsNullOrEmpty(newUserDTO.Name) || string.IsNullOrEmpty(newUserDTO.Email) || string.IsNullOrEmpty(newUserDTO.Password))
            {
                return BadRequest("Invalid user data.");
            }

            Users user = new Users(newUserDTO);
            user.Save();

            newUserDTO.Id = user.ID;
            return CreatedAtRoute("GetUserById", new { id = newUserDTO.Id }, newUserDTO);
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdateUser(int id, UserDTO updatedUser)
        {
            if (id < 1 || updatedUser == null || string.IsNullOrEmpty(updatedUser.Name) || string.IsNullOrEmpty(updatedUser.Email))
            {
                return BadRequest("Invalid user data.");
            }

            Users user = Users.Find(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.Phone = updatedUser.Phone;
            user.RoleId = updatedUser.RoleId;
            user.Save();

            return Ok(user.UDTO);
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Users.DeleteUser(id))
            {
                return Ok($"User with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"User with ID {id} not found.");
            }
        }

        [HttpPost("Login", Name = "LoginUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserDTO> Login([FromBody] UserDTO loginData)
        {
            if (string.IsNullOrEmpty(loginData.Email) || string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("Invalid login data.");
            }

            UserDTO user = Users.Login(loginData.Email, loginData.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(user);
        }

        [HttpGet("CheckEmail/{email}", Name = "CheckEmailExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> CheckEmailExists(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email.");
            }

            bool exists = Users.IsEmailExists(email);
            return Ok(exists);
        }

        [HttpGet("CheckPhone/{phone}", Name = "CheckPhoneExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> CheckPhoneExists(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return BadRequest("Invalid phone number.");
            }

            bool exists = Users.IsPhoneExists(phone);
            return Ok(exists);
        }
    }
}
