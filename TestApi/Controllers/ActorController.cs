using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        private readonly ActorRepository _aR;

        public ActorController(ActorRepository aR)
        {
            _aR = aR;
        }

        // GET: api/Actor
        [HttpGet]
        public ActionResult GetActors()
        {
            var actors = _aR.GetAll().ToList();
            return Ok(actors);
        }
        // GET: api/Actor/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult GetById(int id)
        {
            return Ok(_aR.GetByID(id));
        }

        // POST: api/Actor
        [HttpPost]
        public ActionResult PostActor([FromBody]Actor a)
        {
            _aR.AddActor(a.FirstName, a.LastName);
            return Ok();  
        }

        // DELETE: api/Actor/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _aR.DeleteActor(id);
            return Ok();
        }
    }
}
