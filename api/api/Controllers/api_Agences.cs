using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dzbussinis;
using dzdata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Agences")]
    public class api_Agences : ControllerBase
    {
        [HttpGet("All", Name = "GetAllAgences")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AgencesDTO>> GetAllAgences()
        {
            List<AgencesDTO> agencesList = Agences.GetAllAgences();
            if (agencesList.Count == 0)
            {
                return NotFound("No agences found!");
            }
            return Ok(agencesList);
        }

        [HttpGet("{id}", Name = "GetAgenceById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AgencesDTO> GetAgenceById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            Agences agence = Agences.Find(id);
            if (agence == null)
            {
                return NotFound($"Agence with ID {id} not found.");
            }

            return Ok(agence.AgenceDTO);
        }

        [HttpPost(Name = "AddAgence")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AgencesDTO> AddAgence(AgencesDTO newAgenceDTO)
        {
            if (newAgenceDTO == null || string.IsNullOrEmpty(newAgenceDTO.CompanyName))
            {
                return BadRequest("Invalid agence data.");
            }

            Agences agence = new Agences(newAgenceDTO);
            agence.Save();

            newAgenceDTO.Id = agence.ID;
            return CreatedAtRoute("GetAgenceById", new { id = newAgenceDTO.Id }, newAgenceDTO);
        }

        [HttpPut("{id}", Name = "UpdateAgence")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AgencesDTO> UpdateAgence(int id, AgencesDTO updatedAgence)
        {
            if (id < 1 || updatedAgence == null || string.IsNullOrEmpty(updatedAgence.CompanyName))
            {
                return BadRequest("Invalid agence data.");
            }

            Agences agence = Agences.Find(id);
            if (agence == null)
            {
                return NotFound($"Agence with ID {id} not found.");
            }

            agence.CompanyName = updatedAgence.CompanyName;
            agence.Address = updatedAgence.Address;
            agence.Website = updatedAgence.Website;
            agence.Save();

            return Ok(agence.AgenceDTO);
        }

        [HttpDelete("{id}", Name = "DeleteAgence")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteAgence(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid ID: {id}");
            }

            if (Agences.DeleteAgence(id))
            {
                return Ok($"Agence with ID {id} has been deleted.");
            }
            else
            {
                return NotFound($"Agence with ID {id} not found.");
            }
        }
    }
}