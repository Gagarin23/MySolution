using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model
{
    /// <summary>
    /// Промежуточная таблица для реализации отношения многие ко многим.
    /// </summary>
    public class AvailabilityInShop
    {
        [Key]
        public int Id { get; set; }
        public Shop Shop { get; set; }
        public Offer Offer { get; set; }
    }
}
