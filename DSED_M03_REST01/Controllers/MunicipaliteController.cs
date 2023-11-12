using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSED_M03_REST01.Models;
using M01_Srv_Municipalite;
using MunicipaliteModel = DSED_M03_REST01.Models.MunicipaliteModel;
using MunicipaliteSRVM = M01_Srv_Municipalite.Municipalite;
using Newtonsoft.Json;

namespace DSED_M03_REST01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipaliteController : ControllerBase
    {
        private IDepotMunicipalites m_depot;

        public MunicipaliteController(IDepotMunicipalites p_depot)
        {
            m_depot = p_depot;
        }

        // GET: api/MunicipaliteModel
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<MunicipaliteModel>> Get()
        {
            IEnumerable<MunicipaliteModel> municipalitesModel = m_depot.ListerMunicipalitesActives().Select(m => new MunicipaliteModel(m)).ToList();

            return Ok(municipalitesModel);
        }

        // GET: api/MunicipaliteModel/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<MunicipaliteModel> Get(int id)
        {
            var municipalite = m_depot.ChercherMunicipaliteParCodeGeographique(id);
            
            if (municipalite is not null)
            {
                var municipaliteModel = new MunicipaliteModel(municipalite);
                return Ok(municipaliteModel);
            }
            return NotFound();
        }

        // POST: api/MunicipaliteModel
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<MunicipaliteModel> Post([FromBody] MunicipaliteModel p_municipalite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            m_depot.AjouterMunicipalite(p_municipalite.VersEntite());

            return CreatedAtAction(nameof(Get), new { id = p_municipalite.MunicipaliteID }, p_municipalite);
        }

        // PUT: api/MunicipaliteModel/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id, [FromBody] MunicipaliteModel p_municipalite)
        {
            if (!ModelState.IsValid || p_municipalite.MunicipaliteID != id)
            {
                return BadRequest(ModelState);
            }

            var municipaliteExistante = m_depot.ChercherMunicipaliteParCodeGeographique(id);

            if (municipaliteExistante is null)
            {
                return NotFound();
            }

            m_depot.MAJMunicipalite(p_municipalite.VersEntite());

            return NoContent();
        }

        // DELETE: api/MunicipaliteModel/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            var municipaliteExistante = m_depot.ChercherMunicipaliteParCodeGeographique(id);

            if (municipaliteExistante is null)
            {
                return NotFound();
            }

            m_depot.DesactiverMunicipalite(municipaliteExistante);

            return NoContent();
        }
  
    }
}
