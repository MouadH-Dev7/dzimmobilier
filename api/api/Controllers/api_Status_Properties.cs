using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Status_Properties")]
    public class Status_PropertiesController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStatusProperties")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Status_PropertiesDTO>> GetAllStatusProperties()
        {
            List<Status_PropertiesDTO> propertiesList = Status_Properties.GetAllStatusProperties();
            if (propertiesList.Count == 0)
            {
                return NotFound("No status properties found!");
            }
            return Ok(propertiesList);
        }

        [HttpGet("{id}", Name = "GetStatusPropertyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Status_PropertiesDTO> GetStatusPropertyById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Status_Properties statusProperty = Status_Properties.Find(id);
            if (statusProperty == null)
            {
                return NotFound($"Status property with ID {id} not found.");
            }

            return Ok(statusProperty.SPDTO);
        }

        [HttpPost(Name = "AddStatusProperty")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Status_PropertiesDTO> AddStatusProperty(Status_PropertiesDTO newPropertyDTO)
        {
            if (newPropertyDTO == null || string.IsNullOrEmpty(newPropertyDTO.Name))
            {
                return BadRequest("Invalid status property data.");
            }

            Status_Properties statusProperty = new Status_Properties(newPropertyDTO);
            statusProperty.Save();

            newPropertyDTO.Id = statusProperty.ID;
            return CreatedAtRoute("GetStatusPropertyById", new { id = newPropertyDTO.Id }, newPropertyDTO);
        }

        [HttpPut("{id}", Name = "UpdateStatusProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Status_PropertiesDTO> UpdateStatusProperty(int id, Status_PropertiesDTO updatedProperty)
        {
            if (id < 1 || updatedProperty == null || string.IsNullOrEmpty(updatedProperty.Name))
            {
                return BadRequest("Invalid status property data.");
            }

            Status_Properties statusProperty = Status_Properties.Find(id);
            if (statusProperty == null)
            {
                return NotFound($"Status property with ID {id} not found.");
            }

            statusProperty.Name = updatedProperty.Name;
            statusProperty.Save();

            return Ok(statusProperty.SPDTO);
        }

        [HttpDelete("{id}", Name = "DeleteStatusProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStatusProperty(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Status_Properties.DeleteStatusProperty(id))
            {
                return Ok($"Status property with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Status property with ID {id} not found.");
            }
        }
    }
}
