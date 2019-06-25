using SalesProcess.Core.DataModels;
using SalesProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.IServices
{
    public interface IProductsService
    {
        List<Products> GetAllProducts(bool? pIsAvailable);

        Products GetProduct(long id);

        void AddProduct(Products product);

        void UpdateProduct(Products product);

        void DeleteProduct(long id);

        List<Products> GetProductsByBuyer(long id);

        List<ProductsOrderedDataModel> GetProductsOrderedByOrder(long orderId);

    }
}
