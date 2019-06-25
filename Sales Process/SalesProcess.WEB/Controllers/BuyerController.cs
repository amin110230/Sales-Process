using SalesProcess.Core.Entities;
using SalesProcess.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesProcess.WEB.Controllers
{
    public class BuyerController : Controller
    {
        private IBuyersService _buyerService;
        public BuyerController(IBuyersService _buyerService)
        {
            this._buyerService = _buyerService;
        }
        // GET: Buyer
        public ActionResult BuyersList()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBuyer(long id)
        {
            return Json(_buyerService.GetBuyer(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllBuyers(bool? pIsActive)
        {
            var buyers = _buyerService.GetAllBuyers(pIsActive);

            return Json(buyers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBuyer(Buyers buyer)
        {
            buyer = _buyerService.AddBuyer(buyer);
            return Json(buyer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateBuyer(Buyers buyer)
        {
            _buyerService.UpdateBuyer(buyer);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteBuyer(long id)
        {
            _buyerService.DeleteBuyer(id);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BuyersOfNewOrder(long[] pSelectedBuyers)
        {
            var buyers = _buyerService.GetAllBuyers(true).Where(x=> pSelectedBuyers.Contains(x.Id)).ToList();

            return Json(buyers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BuyersOrderedByOrder(long pId)
        {
            var records = _buyerService.GetBuyersOrderedByOrder(pId);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
    }
}