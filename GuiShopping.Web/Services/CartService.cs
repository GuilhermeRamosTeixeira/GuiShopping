﻿using GuiShopping.Web.Models;
using GuiShopping.Web.Services.IServices;
using GuiShopping.Web.Utils;
using System.Net.Http.Headers;
using System.Reflection;

namespace GuiShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/cart";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BasePath}/add-cart", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Something went wrong when calling API");
        }

        public Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> Chwckout(CartHeaderViewModel cartheader, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> FindcartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel>();
        }

        public Task<bool> removeCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.DeleteAsync($"{BasePath}/remove-card/{cartId}");
                if (response.IsSuccessStatusCode)
                    return await response.ReadContentAs<bool>();
                else throw new Exception("Something went wrong when calling API");
            }
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson($"{BasePath}/update-cart", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Something went wrong when calling API");
        }
    }
}
