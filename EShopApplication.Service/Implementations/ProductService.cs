using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using EShopApplication.Repository;
using EShopApplication.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IRepository<ProductInShoppingCart> _productInCartsRepository;

        public ProductService(IRepository<Product> productRepository,
            IShoppingCartService shoppingCartService,
            IRepository<ProductInShoppingCart> productInCartsRepository)
        {
            _productRepository = productRepository;
            _shoppingCartService = shoppingCartService;
            _productInCartsRepository = productInCartsRepository;
        }

        public void AddToCart(AddToCartDTO modelDTO, Guid userId)
        {
            var product_to_add = _productRepository.Get(selector: x => x, predicate: x => x.Id == modelDTO.ProductId);
            var shopping_cart = _shoppingCartService.GetByUserId(userId);
            if (shopping_cart == null)
                throw new Exception("Shopping cart not found.");

            var product_in_shopping_cart_already = _productInCartsRepository.Get(selector: x => x,
                predicate: x => x.ShoppingCart == shopping_cart && x.Product == product_to_add);
            var quanity_to_add = modelDTO.Quantity;
            if (product_in_shopping_cart_already != null)
            {
                //ova znaci deka produktot veke e dodaden vo cart, pa korisnikot zgolemuva samo quantity
                product_in_shopping_cart_already.Quantity += quanity_to_add;
                _productInCartsRepository.Update(product_in_shopping_cart_already);
            }
            else
            {
                ProductInShoppingCart new_obj = new ProductInShoppingCart
                {
                    Id = Guid.NewGuid(),
                    Product = product_to_add,
                    ShoppingCart = shopping_cart,
                    ProductId = modelDTO.ProductId,
                    ShoppingCartId = shopping_cart.Id,
                    Quantity = quanity_to_add
                };
                _productInCartsRepository.Insert(new_obj);
            }

        }

        Product IProductService.Add(Product product)
        {
            product.Id = Guid.NewGuid();//only while adding new product, so we can create an id)
            return _productRepository.Insert(product);
        }


        Product IProductService.DeleteById(Guid Id)
        {
            var product_to_delete = _productRepository.Get(selector: x => x,
                predicate: product => product.Id == Id);
            return _productRepository.Delete(product_to_delete);
        }

        List<Product> IProductService.GetAll()
        {
            return _productRepository.GetAll(selector: x => x).ToList();
        }

        Product? IProductService.GetById(Guid Id)
        {
            return _productRepository.Get(selector: x => x, predicate: product => product.Id == Id);
        }

        Product IProductService.Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
