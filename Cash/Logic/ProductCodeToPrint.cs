namespace Cash.Logic
{
    public class ProductCodeToPrint
    {
        public ProductCodeToPrint(string code, int count)
        {
            Code = code;
            Count = count;
        }

        public string Code { get; private set; }

        public int Count { get; private set; }

        public override string ToString()
        {
            return $"{Code}({Count})";
        }
    }
}