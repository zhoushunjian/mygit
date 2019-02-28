using System;
using System.Collections.Generic;
using System.Linq;
using CoreBackend.Dtos;
using CoreBackend.Filter;
using CoreBackend.Repositories;
using CoreBackend.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreBackend.Controllers
{
    /// <summary>
    /// 产品控制器
    /// </summary>
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMailService _mailService;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IMailService mailService, IProductRepository productRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }
        
        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ProductWithoutMaterialDto> GetProducts()
        {
            var products = _productRepository.GetProducts();
            var results = new List<ProductWithoutMaterialDto>();
            foreach (var product in products)
            {
                results.Add(new ProductWithoutMaterialDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                });
            }

            return results;
        }

        /// <summary>
        /// 获取产品根据主键
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeMaterial"></param>
        [HttpGet]
        /// <returns></returns>
        public ProductDto GetProduct(int id, bool includeMaterial = false)
        {
            var product = _productRepository.GetProduct(id, includeMaterial);
            if (product == null)
            {
                _logger.LogInformation("testLog");
                throw new Exception("找不到对应的产品");
            }

            if (includeMaterial)
            {
                var productWithMaterialResult = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };
                productWithMaterialResult.Materials.AddRange(product.Materials.Select(s => new Material() { Id = s.Id, Name = s.Name }).ToList());

                return productWithMaterialResult;
            }

            var onlyProductResult = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            return onlyProductResult;
        }

        [HttpGet]
        public void testException(int id)
        {
            if (id == 1)
            {
                throw new Exception("test");
            }
        }

        [HttpGet]
        [SkipActionFilter]
        public void testException1(int id)
        {
            if (id == 1)
            {
                throw new Exception("test");
            }
        }

        [HttpPost]
        public ProductDto AddProduct([FromBody] AddProduct request)
        {
            if (request == null)
            {
                throw new Exception("参数不能为空");
            }

            var maxId = ProductService.Current.Products.Max(s => s.Id);
            var productEntity = new ProductDto()
            {
                Id = maxId + 1,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };
            ProductService.Current.Products.Add(productEntity);

            return productEntity;
        }

        [HttpPost]
        public string testCore([FromBody] string request)
        {
            return request;
        }

        [HttpPut]
        public ProductDto UpdateProduct(int id, [FromBody]UpdateProduct request)
        {
            if (request == null)
            {
                throw new Exception("参数不能为空");
            }

            var model = ProductService.Current.Products.SingleOrDefault(s => s.Id == id);
            if (model == null)
            {
                throw new Exception("找不对对应的产品");
            }
            model.Name = request.Name;
            model.Price = request.Price;
            model.Description = request.Description;

            return model;
        }

        [HttpPatch]
        public ProductDto PatchProduct(int id, [FromBody]JsonPatchDocument<UpdateProduct> requests)
        {
            if (requests == null)
            {
                throw new Exception("参数不能为空");
            }

            var model = ProductService.Current.Products.SingleOrDefault(s => s.Id == id);
            if (model == null)
            {
                throw new Exception("找不到对应的产品");
            }

            var toPatch = new UpdateProduct
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
            requests.ApplyTo(toPatch, ModelState);

            model.Name = toPatch.Name;
            model.Price = toPatch.Price;
            model.Description = toPatch.Description;

            return model;
        }

        [HttpDelete]
        public void DeleteProduct(int id)
        {
            var model = ProductService.Current.Products.SingleOrDefault(s => s.Id == id);
            if (model == null)
            {
                throw new Exception("找不到对应的产品");
            }
            _mailService.Send("Product Deleted", $"Id为{id}的产品被删除了");
            ProductService.Current.Products.Remove(model);
        }
    }
}