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

        // GET: api/<TopicsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> Get()
        {
            return await db.Topics.ToListAsync();
        }

        // GET api/<TopicsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> Get(int id)
        {
            Topic topic = await db.Topics.FirstOrDefaultAsync(x => x.TopicId == id);
            if (topic == null)
                return NotFound();
            return new ObjectResult(topic);
        }

        // POST api/<TopicsController>
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

        // PUT api/<TopicsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Topic>> Put(Topic topic)
        {
            if (topic == null)
            {
                return BadRequest();
            }
            if (!db.Topics.Any(x => x.TopicId == topic.TopicId))
            {
                return NotFound();
            }

            db.Update(topic);
            await db.SaveChangesAsync();
            return Ok(topic);
        }

        // DELETE api/<TopicsController>/5
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
