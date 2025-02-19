using Buisness.Factories;
using Buisness.Interfaces;
using Buisness.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Buisness.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    {
        var entity = await _productRepository.GetAsync(x => x.ProductName == form.ProductName);
        entity ??= await _productRepository.CreateAsync(ProductFactory.Create(form));

        return ProductFactory.Create(entity);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var enteties = await _productRepository.GetAllAsync();
        var products = enteties.Select(ProductFactory.Create);
        return products ?? [];
    }

    public async Task<Product> GetProductByNameAsync(string productName)
    {
        var productEntity = await _productRepository.GetAsync(x => x.ProductName == productName);
        var product = ProductFactory.Create(productEntity);
        return product ?? null!;
    }

    public async Task<Product> UppdateProductAsync(ProductUpdateForm form)
    {
        var existingEntity = await _productRepository.GetAsync(x => x.Id == form.Id);
        if (existingEntity == null)
            return null!;

        existingEntity.ProductName = form.ProductName ?? existingEntity.ProductName;
        existingEntity.Price = form.Price ?? existingEntity.Price;

        var result = await _productRepository.UpdateAsync(existingEntity);
        if (result == null)
            return null!;

        return ProductFactory.Create(result);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var result = await _productRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
