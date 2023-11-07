using GuiShopping.Web.Models;

namespace GuiShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task <IEnumerable<ProductModel>> FindAllProducts();
        Task <ProductModel> FindProductsById(long id);
        Task <ProductModel> CreateProduct(ProductModel model);
        Task <ProductModel> UpdateProduct(ProductModel model);
        Task <bool>DeleteProductById(long id);
    }
}
