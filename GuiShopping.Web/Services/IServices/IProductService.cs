using GuiShopping.Web.Models;

namespace GuiShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task <IEnumerable<ProductModel>> FindAllProducts(string token);
        Task <ProductModel> FindProductsById(long id, string token);
        Task <ProductModel> CreateProduct(ProductModel model, string token);
        Task <ProductModel> UpdateProduct(ProductModel model, string token);
        Task <bool>DeleteProductById(long id , string token);
    }
}
