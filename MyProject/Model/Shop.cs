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
        private string shopId;
        public string ShopId 
        {
            get => shopId;
            set
            {
                if (value.Length < 50)
                    shopId = value;

                else
                {
                    Console.WriteLine("Id магазина не должно превышать 49 символов.");
                }
            }
        }
        public List<Offer> Offers { get; set; }

        public override string ToString()
        {
            return shopId;
        }
    }
}
