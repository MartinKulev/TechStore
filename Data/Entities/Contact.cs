using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Contact
    {
        public Contact() { }

        public Contact(int contactId, string contactName, string contactEmail, string message) 
        {
            ContactId = contactId;
            ContactName = contactName;
            ContactEmail = contactEmail;
            Message = message;
        }
        public int ContactId { get; set; }

        [StringLength(50)] 
        public string ContactName { get; set; }

        [StringLength(50)] 
        public string ContactEmail { get; set; }

        public string Message { get; set; }
    }
}
