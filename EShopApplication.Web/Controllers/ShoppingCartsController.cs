using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShopApplication.Domain.DomainModels;
using EShopApplication.Service.Interfaces;
using System.Security.Claims;

namespace EShopApplication.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService cartService)
        {
            _shoppingCartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = _shoppingCartService.GetByUserIdIncudingProducts(Guid.Parse(userId));
            return View(cart);
        }
        public IActionResult DeleteFromCart(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _shoppingCartService.DeleteFromCart(id, userId);
            return RedirectToAction("Index");
        }
        public IActionResult Order()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _shoppingCartService.OrderProducts(userId);
            return RedirectToAction("Index");
        }
    }
}
