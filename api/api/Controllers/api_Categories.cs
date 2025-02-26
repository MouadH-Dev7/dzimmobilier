using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    public class api_Categories : ControllerBase
    {
        [HttpGet("All", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            List<CategoryDTO> categoriesList = Categories.GetAllCategories();
            if (categoriesList.Count == 0)
            {
                return NotFound("No categories found!");
            }
            return Ok(categoriesList);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDTO> GetCategoryById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Categories category = Categories.Find(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return Ok(category.CDTO);
        }

        [HttpPost(Name = "AddCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDTO> AddCategory(CategoryDTO newCategoryDTO)
        {
            if (newCategoryDTO == null || string.IsNullOrEmpty(newCategoryDTO.Name))
            {
                return BadRequest("Invalid category data.");
            }

            Categories category = new Categories(newCategoryDTO);
            category.Save();

            newCategoryDTO.Id = category.ID;
            return CreatedAtRoute("GetCategoryById", new { id = newCategoryDTO.Id }, newCategoryDTO);
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryDTO updatedCategory)
        {
            if (id < 1 || updatedCategory == null || string.IsNullOrEmpty(updatedCategory.Name))
            {
                return BadRequest("Invalid category data.");
            }

            Categories category = Categories.Find(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            category.Name = updatedCategory.Name;
            category.Save();

            return Ok(category.CDTO);
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Categories.DeleteCategory(id))
            {
                return Ok($"Category with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Category with ID {id} not found.");
            }
        }
    }
}