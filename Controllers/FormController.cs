using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

            int number;
            bool isTopicIdNumber = Int32.TryParse(form.TopicId.ToString(), out number);

            if (isTopicIdNumber == false)
            {
                ModelState.AddModelError("TopicId", "TopicId is not a number. DataType int is awaited");
            }

            var topicsInDBWithThisId = Topic.ifTopicExistInDataBase(form.TopicId);

            if(topicsInDBWithThisId.Count == 0)
            {
                ModelState.AddModelError("TopicId", "Темы с таким Id не существует");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Form currentForm = Form.ManageForm(form);

            await dataBase.SaveChangesAsync();

            return Ok(currentForm);
        }
    }
}
