using SalesProcess.Core.DataModels;
using SalesProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.IServices
{
    public interface IBuyersService
    {
        List<Buyers> GetAllBuyers(bool? pIsActive);

        Buyers GetBuyer(long id);

        Buyers AddBuyer(Buyers buyer);

        void UpdateBuyer(Buyers buyer);

        void DeleteBuyer(long id);

        List<BuyersOrderDataModel> GetBuyersOrderedByOrder(long orderId);

    }
}
