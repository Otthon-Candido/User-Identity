namespace User.Domain.Models
{

    public class Message
    {
        public List<string> Recipient { get; set; }

        public string About { get; set; }

        public string Content { get; set; }

        public Message(IEnumerable<string> recipient, string about, string userId, string code, string content)
        {
            Recipient = new List<string>(recipient);
            About = about;
            Content = content;
        }
    }

}