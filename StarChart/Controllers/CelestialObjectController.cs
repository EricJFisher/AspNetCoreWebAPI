using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        
        [HttpGet ("{id:int}")]
        public IActionResult GetById(int id)
        {
           var satellite= _context.CelestialObjects.Find(id);

            if(satellite!=null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(String name)
        {
            var result = _context.CelestialObjects.Where(x => x.Name.Equals(name)).Count();

            if(result>0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

    }
}
