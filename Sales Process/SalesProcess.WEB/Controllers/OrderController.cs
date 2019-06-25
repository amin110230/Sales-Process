using SalesProcess.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesProcess.WEB.Controllers
{
    public class OrderController : Controller
    {
        private IOrdersService _orderService;
        public OrderController(IOrdersService _orderService)
        {
            this._orderService = _orderService;
        }
        // GET: Order
        public ActionResult Order()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult NewOrder()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllOrders(bool? pIsPaid)
        {
            var result = _orderService.GetAllOrders(pIsPaid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBuyersOfOrder(long id)
        {
            var result = _orderService.GetBuyersByOrder(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOrdersByBuyer(long buyerId)
        {
            var result = _orderService.GetOrdersByBuyer(buyerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PlaceOrder(long[] pProductIds, int[] pProductQuantity, long[] pBuyerIds, double[] pBuyersPCT, decimal pTotalPrice)
        {
            _orderService.PlaceOrder(pProductIds, pProductQuantity, pBuyerIds, pBuyersPCT, pTotalPrice);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MakePayment(long pId, long[] pBuyerOrderIds, decimal[] pBuyersPayments)
        {
            _orderService.MakePayment(pId, pBuyerOrderIds, pBuyersPayments);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPaymentLogOfBuyer(long pBuyerId, long pOrderId)
        {
            var records = _orderService.GetPaymentLogByBuyers(pBuyerId, pOrderId);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
    }
}