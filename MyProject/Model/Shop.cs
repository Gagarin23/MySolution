using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject
{
    public class Shop
    {
        private string shopId;
        [Key]
        public string ShopId
        {
            get => shopId;
            set
            {
                if (value != null || value.Length < 50) //Добавить бы ещё валидацию на корректность символов, но это отдельная тема.
                    shopId = value;

                else
                {
                    Console.WriteLine("Id магазина не может быть null и не должно превышать 49 символов.");
                }
            }
        }

        public override string ToString()
        {
            return shopId;
        }
    }
}
