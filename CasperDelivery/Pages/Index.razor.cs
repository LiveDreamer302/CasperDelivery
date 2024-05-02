using CasperDelivery.WebModels;
using Microsoft.AspNetCore.Components;
using System.Timers;

namespace CasperDelivery.Pages
{
    public partial class Index 
    {
        private ContactForm Contact = new ContactForm();
        private ValidationMessages validationMessages = new ValidationMessages();
        private string submitMessage = "";
        private async Task SubmitForm()
        {
            if (string.IsNullOrWhiteSpace(Contact.Name))
                validationMessages.Name = "Please enter your name.";
            if (string.IsNullOrWhiteSpace(Contact.Email))
                validationMessages.Email = "Please enter your email.";
            if (string.IsNullOrWhiteSpace(Contact.Message))
                validationMessages.Message = "Please enter your message.";
            if (validationMessages.HasErrors())
                return;
            submitMessage = "Your message has been submitted successfully!";
        }

        private class ValidationMessages
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }

            public bool HasErrors()
            {
                return !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Message);
            }
        }
    }
}