namespace Cash.Logic
{
    public class ProductCodeToPrint
    {
        public ProductCodeToPrint(string code, int count, string description = null)
        {
            Code = code;
            Count = count;
            Description = description;
        }

        public string Code { get; private set; }

        public int Count { get; private set; }

        public string Description { get; private set; }

        public override string ToString()
        {
            return $"{Code}({Count})";
        }
    }
}