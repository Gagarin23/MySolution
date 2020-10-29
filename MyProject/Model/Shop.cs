using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model
{
    class Shop
    {
        [Key]
        public int ShopId { get; set; }
        public int OffersId { get; set; }
        public Offers Offers { get; set; }
    }
}
