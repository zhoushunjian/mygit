using System.Collections.Generic;
using System.Linq;
using CoreBackend.Dtos;
using CoreBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Controllers
{
    public class MaterialController : BaseController
    {
        [HttpGet]
        public List<Material> GetMaterials(int productId)
        {
            var product = ProductService.Current.Products.SingleOrDefault(s => s.Id == productId);
            if (product == null)
            {
                return new List<Material>();
            }
            return product.Materials;
        }

        [HttpGet]
        public Material GetMaterial(int productId, int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(s => s.Id == productId);

            if (product == null)
            {
                return null;
            }

            var material = product.Materials.SingleOrDefault(s => s.Id == id);
            if (material == null)
            {
                return null;
            }

            return material;
        }
    }
}