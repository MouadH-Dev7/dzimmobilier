using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Roles")]
    public class RolesController : ControllerBase
    {
        // جلب جميع الأدوار
        [HttpGet("All", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoleDTO>> GetAllRoles()
        {
            List<RoleDTO> rolesList = Roles.GetAllRoles();
            if (rolesList.Count == 0)
            {
                return NotFound("No roles found!");
            }
            return Ok(rolesList);
        }

        // جلب دور معين حسب ID
        [HttpGet("{id}", Name = "GetRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RoleDTO> GetRoleById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Roles role = Roles.Find(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }

            return Ok(role.RDTO);
        }

        // إضافة دور جديد
        [HttpPost(Name = "AddRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoleDTO> AddRole(RoleDTO newRoleDTO)
        {
            if (newRoleDTO == null || string.IsNullOrEmpty(newRoleDTO.Name))
            {
                return BadRequest("Invalid role data.");
            }

            Roles role = new Roles(newRoleDTO);
            role.Save();

            newRoleDTO.Id = role.ID;
            return CreatedAtRoute("GetRoleById", new { id = newRoleDTO.Id }, newRoleDTO);
        }

        // تحديث بيانات دور
        [HttpPut("{id}", Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RoleDTO> UpdateRole(int id, RoleDTO updatedRole)
        {
            if (id < 1 || updatedRole == null || string.IsNullOrEmpty(updatedRole.Name))
            {
                return BadRequest("Invalid role data.");
            }

            Roles role = Roles.Find(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }

            role.Name = updatedRole.Name;
            role.Save();

            return Ok(role.RDTO);
        }

    
        [HttpDelete("{id}", Name = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteRole(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Roles.DeleteRole(id))
            {
                return Ok($"Role with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Role with ID {id} not found.");
            }
        }
    }
}
