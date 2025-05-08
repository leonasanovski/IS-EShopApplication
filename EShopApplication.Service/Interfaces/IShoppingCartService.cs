using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Service.Interfaces
{
    public interface IShoppingCartService
    {
        ShoppingCart GetByUserId(Guid id);
        ShoppingCartDTO GetByUserIdIncudingProducts(Guid id);
        AddToCartDTO GetProductInfo(Guid id);
        Boolean DeleteFromCart(Guid id, string userId);
        //added for the shopping cart order functionality
        Boolean OrderProducts(string userId);
    }
}
