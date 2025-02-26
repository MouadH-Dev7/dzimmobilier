using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Types")]
    public class TypeController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TypeData>> GetAllTypes()
        {
            List<TypeData> typesList = type_dz.GetAllTypes();
            if (typesList.Count == 0)
            {
                return NotFound("No types found!");
            }
            return Ok(typesList);
        }

        [HttpGet("{id}", Name = "GetTypeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TypeData> GetTypeById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            type_dz type = type_dz.Find(id);
            if (type == null)
            {
                return NotFound($"Type with ID {id} not found.");
            }

            return Ok(type.TDTO);
        }

        [HttpPost(Name = "AddType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TypeData> AddType(TypeData newType)
        {
            if (newType == null || string.IsNullOrEmpty(newType.Name))
            {
                return BadRequest("Invalid type data.");
            }

            type_dz type = new type_dz(newType);
            type.Save();

            newType.Id = type.ID;
            return CreatedAtRoute("GetTypeById", new { id = newType.Id }, newType);
        }

        [HttpPut("{id}", Name = "UpdateType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TypeData> UpdateType(int id, TypeData updatedType)
        {
            if (id < 1 || updatedType == null || string.IsNullOrEmpty(updatedType.Name))
            {
                return BadRequest("Invalid type data.");
            }

            type_dz type = type_dz.Find(id);
            if (type == null)
            {
                return NotFound($"Type with ID {id} not found.");
            }

            type.Name = updatedType.Name;
            type.Save();

            return Ok(type.TDTO);
        }

        [HttpDelete("{id}", Name = "DeleteType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteType(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (type_dz.DeleteType(id))
            {
                return Ok($"Type with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Type with ID {id} not found.");
            }
        }
    }
}
