using MimeKit;

namespace User.Domain.Models
{

    public class Message
    {
        public List<MailboxAddress> Recipient {get; set;}

        public string About { get; set; }

        public string Content { get; set; }

        public Message(IEnumerable<string> recipient, string about, string userId, string code, string content)
        {
             Recipient = new List<MailboxAddress>(); 
             Recipient.AddRange(recipient.Select(d=> new MailboxAddress(d)));
             About = about;
             Content = content;
        }
    }

}