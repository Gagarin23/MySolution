using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExtendedProject.Model
{
    public sealed class Shop
    {
        private string _shopId;
        [Key]
        public string StringId
        {
            get => _shopId;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length < 50) //Добавить бы ещё валидацию на корректность символов, но это отдельная тема.
                    _shopId = value;

                else
                {
                    Console.WriteLine($"Id магазина не может быть пустым и не должно превышать 49 символов. Значение было \"{value}\"");
                }
            }
        }

        public override string ToString()
        {
            return _shopId;
        }
    }
}
