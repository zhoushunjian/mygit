using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Materials = new List<Material>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public string Description { get; set; }

        public List<Material> Materials { get; set; }

        public int MaterialCount => Materials.Count;
    }

    public class Material
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
