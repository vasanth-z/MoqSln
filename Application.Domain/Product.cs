using System;

namespace Application.Domain
{
    public class Product
    {
        public ProductIdentifier Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Product(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public class InvalidProductIdException : Exception
    {
    }
}

namespace Application.Domain
{
    public class ProductIdentifier
    {
        public int RawValue { get; set; }
    }
}