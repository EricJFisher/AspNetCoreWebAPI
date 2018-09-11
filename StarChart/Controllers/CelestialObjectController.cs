using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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
            var models = _context.CelestialObjects.ToList();
            foreach (var item in models)
            {
                item.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObject.Id == item.Id).ToList();
            }
            return Ok(models);
        }

        [HttpPost()]
        public IActionResult Create([FromBody]CelestialObject celestialObject)
        {
            _context.CelestialObjects.Add(celestialObject);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var model = _context.CelestialObjects.Find(id);
            if (model == null)
                return NotFound();
            model.Name = celestialObject.Name;
            model.OrbitedObject = celestialObject.OrbitedObject;
            model.OrbitalPeriod = celestialObject.OrbitalPeriod;
            _context.CelestialObjects.Update(model);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var model = _context.CelestialObjects.Find(id);
            if (model == null)
                return NotFound();
            model.Name = name;
            _context.CelestialObjects.Update(model);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
