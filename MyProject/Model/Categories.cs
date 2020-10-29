using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    [XmlRoot("categories")]
    public class Categories
    {
        [XmlElement("category")]
        public List<Category> CategoriesList { get; set; }
    }
}
