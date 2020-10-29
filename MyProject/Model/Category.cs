using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    public class Category
    {
        [XmlAttribute("id")]
        [Key]
        public int CategoryId { get; set; }

        [XmlAttribute("parentId")]
        public int ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
    }
}
