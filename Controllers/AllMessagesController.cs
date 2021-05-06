using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_Form.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllMessagesController : ControllerBase
    {
        readonly SofTrust_dbContext db;
        public AllMessagesController(SofTrust_dbContext context)
        {
            db = context;
        }

        // GET: api/<AllMessageController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllMessage>>> Get()
        {
            return await db.AllMessages.ToListAsync();
        }

        // GET api/<AllMessageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllMessage>> Get(int id)
        {
            AllMessage message = await db.AllMessages.FirstOrDefaultAsync(x => x.MessageId == id);
            if (message == null)
                return NotFound();
            return new ObjectResult(message);
        }

        // POST api/<AllMessageController>
        [HttpPost]
        public async Task<ActionResult<AllMessage>> Post(AllMessage message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            db.AllMessages.Add(message);
            await db.SaveChangesAsync();
            return Ok(message);
        }

        // PUT api/<AllMessageController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AllMessage>> Put(AllMessage message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            if (!db.AllMessages.Any(x => x.MessageId == message.MessageId))
            {
                return NotFound();
            }

            db.Update(message);
            await db.SaveChangesAsync();
            return Ok(message);
        }

        // DELETE api/<AllMessageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AllMessage>> Delete(int id)
        {
            AllMessage message = db.AllMessages.FirstOrDefault(x => x.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }
            db.AllMessages.Remove(message);
            await db.SaveChangesAsync();
            return Ok(message);
        }
    }
}
