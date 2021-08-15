namespace App.Domain
{
    public class Product
    {
        public int? Id { get; private set; }
        public int Number { get; private set; }
        public string Name { get; private set; }

        // EF constructor
        private Product() { }

        public Product(int number, string name)
        {
            Number = number;
            Name = name;
        }
    }
}
