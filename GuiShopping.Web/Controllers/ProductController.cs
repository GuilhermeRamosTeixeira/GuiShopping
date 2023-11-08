﻿using GuiShopping.Web.Models;
using GuiShopping.Web.Services.IServices;
using GuiShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GuiShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null) return  RedirectToAction(nameof( ProductIndex));
            }
            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductsById(id);
            if(product !=null ) return View(product);
            return NotFound();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            var response = await _productService.UpdateProduct(model);
            if(response != null) return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.FindProductsById(id);
            if (product != null) return View(product);
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _productService.DeleteProductById(model.Id);
            if(response) return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}

