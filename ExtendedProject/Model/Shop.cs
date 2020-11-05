﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Model
{
    public class Shop
    {
        private string _shopId;
        [Key]
        public string ShopId
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