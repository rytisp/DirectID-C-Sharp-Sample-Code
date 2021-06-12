namespace ExampleSetup.Models
{
    public class Transaction
    {
        public string Date { get; set; }

        public string Description { get; set; }

        public string Amount { get; set; }

        public string Type { get; set; }

        public string Balance { get; set; }

        public string Category { get; set; }

        public string CategoryId { get; set; }

        public string CategoryType { get; set; }

        public string CategorizationKeyword { get; set; }


        public Transaction(
            string date,
            string description,
            string amount,
            string type,
            string balance,
            string category,
            string categoryId,
            string categoryType,
            string categorizationKeyword)

        {
            Date = date;
            Description = description;
            Amount = amount;
            Type = type;
            Balance = balance;
            Category = category;
            CategoryId = categoryId;
            CategoryType = categoryType;
            CategorizationKeyword = categorizationKeyword;
        }
    }
}