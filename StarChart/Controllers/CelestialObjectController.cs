using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var model = _context.CelestialObjects.Find(id);
            if(model == null)
                return NotFound();
            model.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObject.Id == model.Id).ToList();
            return Ok(model);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var models = _context.CelestialObjects.Where(e => e.Name == name);
            if (!models.Any())
                return NotFound();
            foreach (var item in models)
            {
                item.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObject.Id == item.Id).ToList();
            }
            return Ok(models);
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var models = _context.CelestialObjects;
            if (!models.Any())
                return NotFound();
            foreach (var item in models)
            {
                item.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObject.Id == item.Id).ToList();
            }
            return Ok(models);
        }
    }
}
