using System.Collections.Generic;

namespace ExampleSetup.Models
{
    public class IndividualDetails
    {
        public string Reference { get; set; }

        public string Provider { get; set; }

        public List<AccountDetails> Accounts { get; set; }

        public IndividualDetails(string reference,
            string provider,
            List<AccountDetails> accounts)
        {
            Reference = reference;
            Provider = provider;
            Accounts = accounts;
        }
    }
}