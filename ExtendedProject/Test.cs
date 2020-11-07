using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExtendedProject.Model;

namespace ExtendedProject
{
    class Test
    {
        public static void Run()
        {
            var list1 = new List<AvailabilityInShop>();
            var list2 = new List<AvailabilityInShop>();

            for (int i = 0; i < 10; i++)
            {
                list1.Add(new AvailabilityInShop
                {
                    Shop = new Shop {StringId = i.ToString()},
                    Offer = new Offer{OfferId = i}
                });
            }

            for (int i = 7; i > 0; i--)
            {
                list2.Add(new AvailabilityInShop
                {
                    Shop = new Shop { StringId = i.ToString() },
                    Offer = new Offer { OfferId = i }
                });
            }

            var flag = list2.All(av2 => "4" != av2.ShopId || 4 != av2.OfferId);
            var flag2 = list2.Any(av2 => "4" == av2.ShopId && 4 == av2.OfferId);

            var result1 = list1
                .Where(av1 =>
                    list2.All(av2 => av1.ShopId != av2.ShopId || av1.OfferId != av2.OfferId))
                .ToArray();

            var result2 = list1
                .Where(av1 =>
                    !list2.Any(av2 => av1.ShopId != av2.ShopId && av1.OfferId != av2.OfferId))
                .ToArray();
        }
    }
}
