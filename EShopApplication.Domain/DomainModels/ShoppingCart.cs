using EShopApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public EShopApplicationUser? OwnerUser { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductsInTheShoppingCart { get; set; }
    }
}
