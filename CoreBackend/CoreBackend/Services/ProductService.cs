using CoreBackend.Dtos;
using System;
using System.Collections.Generic;

namespace CoreBackend.Services
{
    public class ProductService
    {
        public static ProductService Current { get; } = new ProductService();

        public List<ProductDto> Products { get; }

        private ProductService()
        {
            Products = new List<ProductDto>
            {
                new ProductDto{
                    Id = 1,
                    Name = "奶",
                    Price = Convert.ToDecimal(2.5),
                    Description = "这是牛奶啊",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Id = 1,
                            Name = "水"
                        },
                        new Material
                        {
                            Id = 2,
                            Name = "奶粉"
                        }
                    }
                },
                new ProductDto{
                    Id = 2,
                    Name = "包",
                    Price = Convert.ToDecimal(4.5),
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Id = 3,
                            Name = "面粉"
                        },
                        new Material
                        {
                            Id = 4,
                            Name = "糖"
                        }
                    }

                },
                new ProductDto{
                    Id = 3,
                    Name = "酒",
                    Price = Convert.ToDecimal(7.5),
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Id = 5,
                            Name = "麦芽"
                        },
                        new Material
                        {
                            Id = 6,
                            Name = "地下水"
                        }
                    }
                }
            };
        }
    }
}
