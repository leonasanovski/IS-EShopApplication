using EShopApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EShopApplication.Domain.Identity
{
    public class EShopApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surnname { get; set; }
        public string? Address { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }

}
