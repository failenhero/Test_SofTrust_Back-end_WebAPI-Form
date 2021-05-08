using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Form.Models
{
    public class Form
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [EmailAddress(ErrorMessage = "Введён некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан Id темы обращения")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Не указан текст обращения")]
        public string Message { get; set; }

        public static Form ManageForm (Form currentForm)
        {
            using SofTrust_dbContext dataBase = new SofTrust_dbContext();

            List<Contact> checkIfContactExists()
            {
                //using ADO.NET below:

                //string SqlQuery = $"SELECT * FROM contacts WHERE contact_name LIKE '{currentForm.Name}' AND contact_email LIKE '{currentForm.Email}' AND contact_phone LIKE '{currentForm.Phone}'";
                //
                //return Connect.ExecuteSelectContacts(SqlQuery);

                //using Entity Framework below:

                List<Contact> contacts = dataBase
                                            .Contacts
                                            .Where(con => con.ContactName == currentForm.Name && con.ContactEmail == currentForm.Email && con.ContactPhone == currentForm.Phone)
                                            .ToList();

                return contacts;

                //List<Contact> contacts = Connect.ExecuteSelectContacts(SqlQuery);
                //return contacts;
                //return dataBase
                //        .Contacts
                //        .FromSqlInterpolated($"SELECT * FROM contacts WHERE CONVERT(VARCHAR, contact_name) = {currentForm.Name} AND CONVERT(VARCHAR, contact_email) = {currentForm.Email} AND CONVERT(VARCHAR, contact_phone) = {currentForm.Phone}")
                //        .ToList();
            }

            void AddNewContact()
            {
                //using ADO.NET below:

                //string SqlQuery = $"INSERT INTO contacts(contact_name, contact_email, contact_phone) VALUES ('{currentForm.Name}', '{currentForm.Email}', '{currentForm.Phone}')";
                //
                //Connect.ExecuteInsert(SqlQuery);

                //using Entity Framework below:

                dataBase.Contacts.Add(new Contact { 
                    ContactName = currentForm.Name,
                    ContactEmail = currentForm.Email,
                    ContactPhone = currentForm.Phone
                });

                dataBase.SaveChanges();

                //dataBase.Database
                //    .ExecuteSqlInterpolated($"INSERT INTO contacts(contact_name, contact_email, contact_phone) VALUES ({currentForm.Name}, {currentForm.Email}, {currentForm.Phone})");
            }

            void AddNewMessage(int contactId)
            {
                //using ADO.NET below:

                //string SqlQuery = $"INSERT INTO allMessages(rf_contact_id, rf_topic_id, message_text) VALUES ('{contactId}', '{currentForm.TopicId}', '{currentForm.Message}')";
                //
                //Connect.ExecuteInsert(SqlQuery);

                //using Entity Framework below:

                dataBase.AllMessages.Add(new AllMessage {
                    RfContactId = contactId,
                    RfTopicId = currentForm.TopicId,
                    MessageText = currentForm.Message
                });

                dataBase.SaveChanges();

                //dataBase.Database
                //    .ExecuteSqlInterpolated($"INSERT INTO allMessages(rf_contact_id, rf_topic_id, message_text) VALUES ({contactId}, {currentForm.TopicId}, {currentForm.Message})");
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
