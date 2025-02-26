using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Properties")]
    public class api_Properties : ControllerBase
    {
        [HttpGet("All", Name = "GetAllProperties")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            List<PropertyDTO> propertiesList = Properties.GetAllProperties();
            if (propertiesList.Count == 0)
            {
                return NotFound("No properties found!");
            }
            return Ok(propertiesList);
        }

        [HttpGet("{id}", Name = "GetPropertyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropertyDTO> GetPropertyById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Properties property = Properties.Find(id);
            if (property == null)
            {
                return NotFound($"Property with ID {id} not found.");
            }

            return Ok(property.PDTO);
        }

        [HttpPost(Name = "AddProperty")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PropertyDTO> AddProperty(PropertyDTO newPropertyDTO)
        {
            if (newPropertyDTO == null || string.IsNullOrEmpty(newPropertyDTO.Title))
            {
                return BadRequest("Invalid property data.");
            }

            Properties property = new Properties(newPropertyDTO);
            property.Save();

            newPropertyDTO.Id = property.ID;
            return CreatedAtRoute("GetPropertyById", new { id = newPropertyDTO.Id }, newPropertyDTO);
        }

        [HttpPut("{id}", Name = "UpdateProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropertyDTO> UpdateProperty(int id, PropertyDTO updatedProperty)
        {
            if (id < 1 || updatedProperty == null || string.IsNullOrEmpty(updatedProperty.Title))
            {
                return BadRequest("Invalid property data.");
            }

            Properties property = Properties.Find(id);
            if (property == null)
            {
                return NotFound($"Property with ID {id} not found.");
            }

            property.Title = updatedProperty.Title;
            property.Description = updatedProperty.Description;
            property.Price = updatedProperty.Price;
            property.Area = updatedProperty.Area;
            property.Bedrooms = updatedProperty.Bedrooms;
            property.Bathrooms = updatedProperty.Bathrooms;
            property.Latitude = updatedProperty.Latitude;
            property.Longitude = updatedProperty.Longitude;
            property.StatusId = updatedProperty.StatusId;
            property.Save();

            return Ok(property.PDTO);
        }

        [HttpDelete("{id}", Name = "DeleteProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteProperty(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Properties.DeleteProperty(id))
            {
                return Ok($"Property with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Property with ID {id} not found.");
            }
        }
    }
}