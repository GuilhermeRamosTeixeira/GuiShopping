
using GuiShopping.Web.Models;
using GuiShopping.Web.Services.IServices;
using GuiShopping.Web.Utils;

namespace GuiShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string  basePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJson(basePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Algo deu errado");
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{basePath}/{id}");
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Algo deu errado");

        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(basePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductsById(long id)
        {
            var response = await _client.GetAsync($"{basePath}/{ id}");
            return await response.ReadContentAs<ProductModel>();
        }


        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PUtAsJson(basePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Algo deu errado");
        }
    }
}
