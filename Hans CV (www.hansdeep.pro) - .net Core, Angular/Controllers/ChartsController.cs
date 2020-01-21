using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vc.models;

namespace vc.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ChartsController : ControllerBase
    {
        ChartContext cc = new ChartContext();

        private readonly ILogger<ChartsController> _logger;

        public ChartsController(ILogger<ChartsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("All")]
        public List<Chart> GetChart()
        {
            return cc.Charts.Where(a => a.ChartName == "Bar").ToList();
        }

        [HttpGet("Search/{name}")]
        public Chart hansdeep(string name)
        {
            Chart ch = new Chart();
            ch = cc.Charts.Where(a => a.ChartName == name).SingleOrDefault();
            return cc.Charts.Where(a => a.ChartName == name).SingleOrDefault();
        }

        [HttpPut("PutChart")]
        public async Task<ActionResult<Chart>> PutChart(Chart ch)
        {
            ch.ChartId = 1;
            ch.ChartName = "Pie";
            using (var context = new ChartContext())
            {
                context.Charts.Update(ch); await context.SaveChangesAsync();
            }
            return CreatedAtAction("asdfads", new { id = ch.ChartId}, ch);
        }

        [HttpPost("AddChart")]
        public async Task<ActionResult<Chart>> AddChart(Chart ch)
        {
            ch.ChartName = "Pie";
            using (var context = new ChartContext())
            {
                context.Charts.Add(ch); await context.SaveChangesAsync();
            }
            return CreatedAtAction("asdfads", new { id = ch.ChartId }, ch);
        }

    }
}
