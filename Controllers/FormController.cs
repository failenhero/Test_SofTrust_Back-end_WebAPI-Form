﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI_Form.Models;

namespace WebAPI_Form.Controllers
{
    [ApiController]
    [Route("api/sendingForm")]
    public class FormController : ControllerBase
    {
        readonly SofTrust_dbContext dataBase;
        public FormController(SofTrust_dbContext db)
        {
            dataBase = db;
        }

        [HttpPost]
        public async Task<ActionResult<Form>> Post(Form form)
        {
            if (form.Name == "admin")
            {
                ModelState.AddModelError("Name", "Недопустимое имя пользователя - admin");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Form currentForm = Form.ManageForm(form);

            await dataBase.SaveChangesAsync();

            return Ok(currentForm);
        }
    }
}
