using System.Collections.Generic;

namespace ExampleSetup.Models
{
    public class AccountDetails
    {
        public string AccountName { get; set; }

        public string AccountHolder { get; set; }

        public string AccountType { get; set; }

        public string ActivityAvailableFrom { get; set; }

        public string AccountNumber { get; set; }

        public string SortCode { get; set; }

        public string Balance { get; set; }

        public string BalanceFormatted { get; set; }

        public string OpeningBalance { get; set; }

        public string OpeningBalanceFormatted { get; set; }

        public List<Transaction> Transactions { get; set; }

        public string CurrencyCode { get; set; }

        public string VerifiedOn { get; set; }

        public AccountDetails(string accountName, 
            string accountHolder,
            string accountType, 
            string activityAvailableFrom,
            string accountNumber,
            string sortCode, 
            string balance,
            string balanceFormatted, 
            string openingBalance,
            string openingBalanceFormatted, 
            string currencyCode,
            string verifiedOn, 
            List<Transaction> transactions)
        {
            AccountName = accountName;
            AccountHolder = accountHolder;
            AccountType = accountType;
            ActivityAvailableFrom = activityAvailableFrom;
            AccountNumber = accountNumber;
            SortCode = sortCode;
            Balance = balance;
            BalanceFormatted = balanceFormatted;
            OpeningBalance = openingBalance;
            OpeningBalanceFormatted = openingBalanceFormatted;
            CurrencyCode = currencyCode;
            VerifiedOn = verifiedOn;
            Transactions = transactions;
        }
    }
}