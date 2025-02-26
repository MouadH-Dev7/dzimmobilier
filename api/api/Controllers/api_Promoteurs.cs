using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Promoteurs")]
    public class PromoteursController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPromoteurs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PromoteurDTO>> GetAllPromoteurs()
        {
            List<PromoteurDTO> promoteursList = PromoteursData.GetPromoteurs();
            if (promoteursList.Count == 0)
            {
                return NotFound("No promoteurs found!");
            }
            return Ok(promoteursList);
        }

        [HttpGet("{id}", Name = "GetPromoteurById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PromoteurDTO> GetPromoteurById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Promoteurs promoteur = Promoteurs.Find(id);
            if (promoteur == null)
            {
                return NotFound($"Promoteur with ID {id} not found.");
            }

            return Ok(promoteur.PDTO);
        }

        [HttpPost(Name = "AddPromoteur")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PromoteurDTO> AddPromoteur(PromoteurDTO newPromoteurDTO)
        {
            if (newPromoteurDTO == null || string.IsNullOrEmpty(newPromoteurDTO.CompanyName))
            {
                return BadRequest("Invalid promoteur data.");
            }

            Promoteurs promoteur = new Promoteurs(newPromoteurDTO);
            promoteur.Save();

            newPromoteurDTO.Id = promoteur.ID;
            return CreatedAtRoute("GetPromoteurById", new { id = newPromoteurDTO.Id }, newPromoteurDTO);
        }

        [HttpPut("{id}", Name = "UpdatePromoteur")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PromoteurDTO> UpdatePromoteur(int id, PromoteurDTO updatedPromoteur)
        {
            if (id < 1 || updatedPromoteur == null || string.IsNullOrEmpty(updatedPromoteur.CompanyName))
            {
                return BadRequest("Invalid promoteur data.");
            }

            Promoteurs promoteur = Promoteurs.Find(id);
            if (promoteur == null)
            {
                return NotFound($"Promoteur with ID {id} not found.");
            }

            promoteur.UserId = updatedPromoteur.UserId;
            promoteur.CompanyName = updatedPromoteur.CompanyName;
            promoteur.RegistrationNumber = updatedPromoteur.RegistrationNumber;
            promoteur.Address = updatedPromoteur.Address;
            promoteur.Save();

            return Ok(promoteur.PDTO);
        }

        [HttpDelete("{id}", Name = "DeletePromoteur")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeletePromoteur(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Promoteurs.DeletePromoteur(id))
            {
                return Ok($"Promoteur with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Promoteur with ID {id} not found.");
            }
        }
    }
}