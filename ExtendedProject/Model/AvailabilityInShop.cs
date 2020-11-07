using System;
using System.ComponentModel.DataAnnotations;

namespace ExtendedProject.Model
{
    /// <summary>
    /// Промежуточная таблица для реализации отношения многие ко многим.
    /// </summary>
    public class AvailabilityInShop 
    {
        [Key]
        public int AvabilityId { get; set; }
        private Shop _shop;
        public Shop Shop
        {
            get => _shop;
            set
            {
                if (_shop != null)
                {
                    Console.WriteLine("Товар с id {0} уже привязан к магазину {1}", OfferId, Shop);
                }
                else
                {
                    _shop = value;
                    ShopId = value.StringId;
                }
            }
        }

        private string _shopId;
        public string ShopId
        {
            get => _shopId;
            private set
            {
                if (!string.IsNullOrEmpty(value) && value.Length < 50) //Добавить бы ещё валидацию на корректность символов, но это отдельная тема.
                    _shopId = value;

                else
                {
                    Console.WriteLine($"Id магазина не может быть пустым и не должно превышать 49 символов. Значение было \"{value}\"");
                }
            }
        }

        private Offer _offer;
        public Offer Offer
        {
            get => _offer;
            set
            {
                _offer = value;
                OfferId = value.OfferId;
            }
        }
        private int _offerId;
        public int OfferId
        {
            get => _offerId;
            private set
            {
                if (value >= 1)
                {
                    try
                    {
                        _offerId = checked(value);
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

        public override string ToString()
        {
            return $"Shop: {ShopId}, OfferId: {OfferId}";
        }
    }
}
