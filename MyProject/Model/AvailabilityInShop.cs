using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Model
{
    /// <summary>
    /// Промежуточная таблица для реализации отношения многие ко многим.
    /// </summary>
    public class AvailabilityInShop
    {
        [Key]
        public int Id { get; set; }

        private Shop shop;
        public Shop Shop
        {
            get => shop;
            set
            {
                if (shop != null)
                {
                    Console.WriteLine("Товар с id {0} уже привязан к магазину {1}", OfferId, Shop);
                }
                else
                {
                    shop = value;
                    ShopId = value.ShopId;
                }
            }
        }

        private string shopId;
        public string ShopId
        {
            get => shopId;
            private set
            {
                if (value.Length < 50) //Добавить бы ещё валидацию на корректность символов, но это отдельная тема.
                    shopId = value;

                else
                {
                    Console.WriteLine("Id магазина не должно превышать 49 символов.");
                }
            }
        }

        private Offer offer;
        public Offer Offer
        {
            get => offer;
            set
            {
                offer = value;
                OfferId = value.OfferId;
            }
        }
        private int offerId;
        public int OfferId
        {
            get => offerId;
            private set
            {
                if (value >= 1)
                {
                    try
                    {
                        offerId = checked(value);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                else
                    Console.WriteLine("Id объекта не может быть меньше 1. Значение было {0}", value);
            }
        }
    }
}
