namespace Application.Domain
{
    public interface IProductRepository
    {
        void Save(Product product);
    }

    public interface IProductIdBuilder
    {
        ProductIdentifier BuildProductIdentifier();
        ProductIdentifier BuildProductIdentifier(string input);
    }
}