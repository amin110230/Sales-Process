using SalesProcess.Core.DataModels;
using SalesProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.IServices
{
    public interface IOrdersService
    {
        List<Orders> GetAllOrders(bool? pIsPaid);

        Orders GetOrder(long id);

        void AddOrder(Orders product);

        void UpdateOrder(Orders product);

        void DeleteOrder(long id);

        List<Orders> GetOrdersByBuyer(long id);

        List<BuyersOrderDataModel> GetBuyersByOrder(long id);

        void PlaceOrder(long[] pProductIds, int[] pProductQuantity, long[] pBuyerIds, double[] pBuyersPCT, decimal pTotalPrice);

        void MakePayment(long pId, long[] pBuyerOrderIds, decimal[] pBuyersPayments);

        List<PaymentDetailsDataModel> GetPaymentLogByBuyers(long buyersId, long orderId);
    }
}
