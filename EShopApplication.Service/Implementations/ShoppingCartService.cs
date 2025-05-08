using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using EShopApplication.Repository;
using EShopApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Service.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productsInCartsRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductsInOrder> _productInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
            IRepository<Product> productRepository,
            IRepository<ProductInShoppingCart> productsInCartsRepository,
            IRepository<Order> orderRepository,
            IRepository<ProductsInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _productsInCartsRepository = productsInCartsRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }

        public bool DeleteFromCart(Guid id, string userId)
        {
            //vaka ke ja dobieme kosnicata
            var cart = _shoppingCartRepository.Get(selector: x => x, predicate: x => x.OwnerId == userId);
            //vaka ke dobieme produktot shto treba da go briseme
            var productToDelete = _productsInCartsRepository.Get(x => x,
                predicate: x => x.ProductId == id && x.ShoppingCart == cart);
            if (productToDelete != null)
            {
                _productsInCartsRepository.Delete(productToDelete);
                return true;
            }
            return false;
        }

        public ShoppingCart GetByUserId(Guid id)
        {
            return _shoppingCartRepository.Get(selector: x => x, predicate: x => x.OwnerId == id.ToString());
        }

        public ShoppingCartDTO GetByUserIdIncudingProducts(Guid id)
        {
            var cart = _shoppingCartRepository.Get(selector: x => x,
                predicate: x => x.OwnerId == id.ToString(),
                include: x => x.Include(all => all.ProductsInTheShoppingCart).ThenInclude(prods => prods.Product));
            //sega ja imame kosnickata so produktite vo nea


            //ni trebaat produktite
            var products_in_the_cart = cart.ProductsInTheShoppingCart.ToList();
            double total_price = 0;
            foreach (var item in products_in_the_cart)
            {
                var item_price = item.Product.ProductPrice.Value;
                var item_quantity = item.Quantity;
                total_price += item_price * item_quantity;
            }
            return new ShoppingCartDTO
            {
                Products = products_in_the_cart,
                TotalPrice = total_price
            };
        }

        public AddToCartDTO GetProductInfo(Guid id)
        {
            var product = _productRepository.Get(selector: x => x, predicate: x => x.Id == id);
            return new AddToCartDTO
            {
                ProductId = id,
                ProductName = product.ProductName,
                Quantity = 1
            };
        }

        public bool OrderProducts(string userId)
        {
            
            var shoppingCart = _shoppingCartRepository.Get(
            selector: x => x,
            predicate: x => x.OwnerId == userId,
            include: x => x
            .Include(y => y.ProductsInTheShoppingCart).ThenInclude(z => z.Product)
            .Include(y => y.OwnerUser));

            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                Owner = shoppingCart.OwnerUser,
                OwnerId = userId
            };
            _orderRepository.Insert(newOrder);
            
            var productsInOrder = shoppingCart.ProductsInTheShoppingCart.Select(z => new ProductsInOrder
            {
                Product = z.Product,
                ProductId = z.ProductId,
                Order = newOrder,
                OrderId = newOrder.Id,
                Quantity = z.Quantity
            });
            
            foreach(var obj in productsInOrder)
            {
                _productInOrderRepository.Insert(obj);
            }
            shoppingCart.ProductsInTheShoppingCart.Clear();
            _shoppingCartRepository.Update(shoppingCart);
            return true;
        }
        
    }
}
