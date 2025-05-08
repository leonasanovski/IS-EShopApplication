using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopApplication.Domain.DomainModels
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductDescription { get; set; }
        [Range(1, 5)]
        public int? ProductRating { get; set; }
        public int? ProductPrice { get; set; }

    }
}
