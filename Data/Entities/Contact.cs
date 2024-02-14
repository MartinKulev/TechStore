using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Contact
    {
        public Contact() { } //We can hardcode the email and phone number on any page for now. If we have time we can implement messaging and other stuff, like
        //being able to change the page's contact info through the admin panel.

        public Contact(int contactId, string contactName, string contactEmail, string message) 
        {
            ContactId = contactId;
            ContactName = contactName;
            ContactEmail = contactEmail;
            Message = message;
        }
        [Key]
        public int ContactId { get; set; }

        [StringLength(50)] 
        public string ContactName { get; set; }

        [StringLength(50)] 
        public string ContactEmail { get; set; }

        public string Message { get; set; }
    }
}
