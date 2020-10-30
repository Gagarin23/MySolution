using MyProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
