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
    public class CeslestialObjectController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CeslestialObjectController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        

    }
}
