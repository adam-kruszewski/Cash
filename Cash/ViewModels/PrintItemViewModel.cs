namespace Cash.ViewModels
{
    public class PrintItemViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Name} [{Code}] ({Count})";
        }
    }
}
