using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_Form.Models;

namespace WebAPI_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        readonly SofTrust_dbContext db;
        public TopicsController(SofTrust_dbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> Get()
        {
            return await db.Topics.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> Get(int id)
        {
            Topic topic = await db.Topics.FirstOrDefaultAsync(x => x.TopicId == id);
            if (topic == null)
                return NotFound();
            return new ObjectResult(topic);
        }

        [HttpPost]
        public async Task<ActionResult<Topic>> Post(Topic topic)
        {
            if (topic == null)
            {
                return BadRequest();
            }

            db.Topics.Add(topic);
            await db.SaveChangesAsync();
            return Ok(topic);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Topic>> Delete(int id)
        {
            Topic topic = db.Topics.FirstOrDefault(x => x.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }
            db.Topics.Remove(topic);
            await db.SaveChangesAsync();
            return Ok(topic);
        }

    }
}
