using CoreBackend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        Product GetProduct(int productId, bool includeMaterials);

        IEnumerable<Material> GetMaterialsForProduct(int productId);

        Material GetMaterialForProduct(int productId, int materialId);
    }
}
