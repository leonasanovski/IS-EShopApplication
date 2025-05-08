using EShopApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set;}
        public virtual ICollection<ProductsInOrder> ProductsInOrders { get;}

    }
}
