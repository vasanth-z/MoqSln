using System.Collections.Generic;

namespace Application.Domain
{
    public class ProductService
    {
        private IProductRepository _productRepository;
        private IProductIdBuilder _productIdBuilder;

        public ProductService(IProductRepository productRepository, IProductIdBuilder productIdBuilder)
        {
            _productRepository = productRepository;
            _productIdBuilder = productIdBuilder;
        }

        public void Create(ProductViewModel productViewModel)
        {
            Product product = ConvertToDomain(productViewModel);
            product.Identifier = _productIdBuilder.BuildProductIdentifier();
            if (product.Identifier == null)
            {
                throw new InvalidProductIdException();
            }
            _productRepository.Save(product);
        }

        private Product ConvertToDomain(ProductViewModel productViewModel)
        {
            return new Product(productViewModel.Name, productViewModel.Description);
        }

        public void CreateMany(List<ProductViewModel> productViewModels)
        {
            foreach (ProductViewModel vm in productViewModels)
            {
                Product newProduct = new Product(vm.Name, vm.Description);
                newProduct.Identifier = _productIdBuilder.BuildProductIdentifier();

                int ii = newProduct.Identifier.RawValue;

                string str = "";

                _productRepository.Save(newProduct);
            }
        }
    }
}