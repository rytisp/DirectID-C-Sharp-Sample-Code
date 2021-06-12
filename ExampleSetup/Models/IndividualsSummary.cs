namespace ExampleSetup.Models
{
    public class IndividualsSummary
    {
        public string Reference { get; set; }

        public string Link { get; set; }

        public string Timestamp { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string UserID { get; set; }

        public IndividualsSummary(
            string reference,
            string timestamp,
            string name,
            string emailAddress,
            string userId,
            string link)
        {
            Reference = reference;
            Timestamp = timestamp;
            Name = name;
            EmailAddress = emailAddress;
            UserID = userId;
            Link = link;
        }
    }
}