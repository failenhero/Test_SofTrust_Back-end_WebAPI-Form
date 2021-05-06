using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_Form.Models;

namespace WebAPI_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        readonly SofTrust_dbContext db;
        public ContactsController(SofTrust_dbContext context)
        {
            db = context;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await db.Contacts.ToListAsync();
        }
 
        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            Contact contact = await db.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
            if (contact == null)
                return NotFound();
            return new ObjectResult(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<ActionResult<Contact>> Post(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            db.Contacts.Add(contact);
            await db.SaveChangesAsync();
            return Ok(contact);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> Put(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            if (!db.Contacts.Any(x => x.ContactId == contact.ContactId))
            {
                return NotFound();
            }

            db.Update(contact);
            await db.SaveChangesAsync();
            return Ok(contact);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Delete(int id)
        {
            Contact contact = db.Contacts.FirstOrDefault(x => x.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return Ok(contact);
        }
    }
}
