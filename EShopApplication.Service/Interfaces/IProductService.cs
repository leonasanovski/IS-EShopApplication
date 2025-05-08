using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Service.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product? GetById(Guid Id);
        Product Update(Product product);
        Product DeleteById(Guid Id);
        Product Add(Product product);
        void AddToCart(AddToCartDTO modelDTO, Guid userId);
    }
}
