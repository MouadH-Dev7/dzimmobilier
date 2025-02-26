using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/PropertyImages")]
    public class api_PropertyImages : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPropertyImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PropertyImageDTO>> GetAllPropertyImages()
        {
            List<PropertyImageDTO> imagesList = PropertyImages.GetAllPropertyImages();
            if (imagesList.Count == 0)
            {
                return NotFound("No property images found!");
            }
            return Ok(imagesList);
        }

        [HttpGet("{id}", Name = "GetPropertyImageById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropertyImageDTO> GetPropertyImageById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            PropertyImages propertyImage = PropertyImages.Find(id);
            if (propertyImage == null)
            {
                return NotFound($"Property image with ID {id} not found.");
            }

            return Ok(propertyImage.PDTO);
        }

        [HttpPost(Name = "AddPropertyImage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PropertyImageDTO> AddPropertyImage(PropertyImageDTO newImageDTO)
        {
            if (newImageDTO == null || newImageDTO.PropertyId < 1 || string.IsNullOrEmpty(newImageDTO.ImageUrl))
            {
                return BadRequest("Invalid property image data.");
            }

            PropertyImages propertyImage = new PropertyImages(newImageDTO);
            propertyImage.Save();

            newImageDTO.Id = propertyImage.ID;
            return CreatedAtRoute("GetPropertyImageById", new { id = newImageDTO.Id }, newImageDTO);
        }

        [HttpPut("{id}", Name = "UpdatePropertyImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropertyImageDTO> UpdatePropertyImage(int id, PropertyImageDTO updatedImage)
        {
            if (id < 1 || updatedImage == null || updatedImage.PropertyId < 1 || string.IsNullOrEmpty(updatedImage.ImageUrl))
            {
                return BadRequest("Invalid property image data.");
            }

            PropertyImages propertyImage = PropertyImages.Find(id);
            if (propertyImage == null)
            {
                return NotFound($"Property image with ID {id} not found.");
            }

            propertyImage.PropertyID = updatedImage.PropertyId;
            propertyImage.ImageUrl = updatedImage.ImageUrl;
            propertyImage.Save();

            return Ok(propertyImage.PDTO);
        }

        [HttpDelete("{id}", Name = "DeletePropertyImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeletePropertyImage(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (PropertyImages.DeletePropertyImage(id))
            {
                return Ok($"Property image with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Property image with ID {id} not found.");
            }
        }
    }
}
