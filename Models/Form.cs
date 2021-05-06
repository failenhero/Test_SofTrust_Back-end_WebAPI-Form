using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI_Form.Models
{
    public class Form
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int TopicId { get; set; }
        public string Message { get; set; }

        public static Form ManageForm (Form currentForm)
        {
            using (SofTrust_dbContext dataBase = new SofTrust_dbContext())
            {
                List<Contact> checkIfContactExists()
                {
                    return dataBase
                            .Contacts
                            .FromSqlInterpolated($"SELECT * FROM contacts WHERE CONVERT(VARCHAR, contact_name) = {currentForm.Name} AND CONVERT(VARCHAR, contact_email) = {currentForm.Email} AND CONVERT(VARCHAR, contact_phone) = {currentForm.Phone}")
                            .ToList();
                }

                void AddNewContact()
                {
                    dataBase.Database
                        .ExecuteSqlInterpolated($"INSERT INTO contacts(contact_name, contact_email, contact_phone) VALUES ({currentForm.Name}, {currentForm.Email}, {currentForm.Phone})");
                }

                void AddNewMessage(int contactId)
                {
                    dataBase.Database
                        .ExecuteSqlInterpolated($"INSERT INTO allMessages(rf_contact_id, rf_topic_id, message_text) VALUES ({contactId}, {currentForm.TopicId}, {currentForm.Message})");
                }

                var contactFromDataBase = checkIfContactExists();

                if (contactFromDataBase.Count == 0)
                {
                    AddNewContact();

                    List<Contact> newContact = checkIfContactExists();

                    int contactId = newContact[0].ContactId;

                    AddNewMessage(contactId);
                }
                else
                {
                    int contactId = contactFromDataBase[0].ContactId;

                    AddNewMessage(contactId);
                }

                return currentForm;

            }

        }
    }
}
