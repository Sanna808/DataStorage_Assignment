using Buisness.Models;

namespace Buisness.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(ProductRegistrationForm form);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByNameAsync(string productName);
        Task<Product> UppdateProductAsync(ProductUpdateForm form);
    }
}