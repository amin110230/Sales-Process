using SalesProcess.Core.Entities;
using SalesProcess.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesProcess.WEB.Controllers
{
    public class ProductController : Controller
    {
        private IProductsService _productService;
        public ProductController(IProductsService _productService)
        {
            this._productService = _productService;
        }

        public ActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllProducts(bool? pIsAvailable)
        {
            var products = _productService.GetAllProducts(pIsAvailable).OrderByDescending(x => x.Id);

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProduct(Products product)
        {
            _productService.AddProduct(product);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProduct(Products product)
        {
            _productService.UpdateProduct(product);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteProduct(long id)
        {
            _productService.DeleteProduct(id);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductsOfNewOrder(long[] pSelectedProducts)
        {
            var products = _productService.GetAllProducts(true).Where(x => pSelectedProducts.Contains(x.Id)).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductsOrderedByOrder(long pId)
        {
            var records = _productService.GetProductsOrderedByOrder(pId);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
    }
}