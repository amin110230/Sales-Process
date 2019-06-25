using SalesProcess.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesProcess.Core.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SalesProcess.Core.DataModels;

namespace SalesProcess.Infrastucture.Services
{
    public class OrdersServices : IOrdersService
    {
        private string connection;
        public OrdersServices()
        {
            connection = ConfigurationManager.ConnectionStrings["SalesProcessDBContext"].ConnectionString;
        }
        public void AddOrder(Orders product)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(long id)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetAllOrders(bool? pIsPaid)
        {
            try
            {
                var orders = new List<Orders>();
                const string procedureName = "procListOfOrders";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pIsPaid", SqlDbType.Bit).Value = (object) pIsPaid ?? DBNull.Value;


                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            orders.Add(new Orders
                            {
                                Id = Convert.ToInt32(sqlDataReader["Id"]),
                                OrderId = sqlDataReader["OrderId"].ToString(),
                                OrderDate = Convert.ToDateTime(sqlDataReader["OrderDate"]),
                                IsPaid = Convert.ToBoolean(sqlDataReader["IsPaid"]),
                                OrderTotalAmount = Convert.ToDecimal(sqlDataReader["OrderTotalAmount"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BuyersOrderDataModel> GetBuyersByOrder(long id)
        {
            try
            {
                var records = new List<BuyersOrderDataModel>();
                const string procedureName = "procListOfBuyersInAnOrder";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = id;
                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDatareader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDatareader.Read())
                        {
                            records.Add(new BuyersOrderDataModel
                            {
                                Id = Convert.ToInt64(sqlDatareader["Id"]),
                                BuyersName = sqlDatareader["BuyersName"].ToString(),
                                TotalAmount = Convert.ToDecimal(sqlDatareader["TotalAmount"]),
                                PaidAmount = Convert.ToDecimal(sqlDatareader["PaidAmount"]),
                                DueAmount = Convert.ToDecimal(sqlDatareader["DueAmount"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Orders GetOrder(long id)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetOrdersByBuyer(long id)
        {
            try
            {
                var records = new List<Orders>();
                const string procedureName = "procListOfOrdersByBuyer";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pBuyerId", SqlDbType.BigInt).Value = id;
                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            records.Add(new Orders
                            {
                                Id = Convert.ToInt32(sqlDataReader["Id"]),
                                OrderId = sqlDataReader["OrderId"].ToString(),
                                OrderDate = Convert.ToDateTime(sqlDataReader["OrderDate"]),
                                IsPaid = Convert.ToBoolean(sqlDataReader["IsPaid"]),
                                OrderTotalAmount = Convert.ToDecimal(sqlDataReader["OrderTotalAmount"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentDetailsDataModel> GetPaymentLogByBuyers(long buyersId, long orderId)
        {
            try
            {
                var records = new List<PaymentDetailsDataModel>();
                const string procedureName = "procListOfPaymentDetailsLog";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pBuyersId", SqlDbType.BigInt).Value = buyersId;
                    sqlCommand.Parameters.Add("@pOrderId", SqlDbType.BigInt).Value = orderId;
                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            records.Add(new PaymentDetailsDataModel
                            {
                                OrderId = sqlDataReader["OrderId"].ToString(),
                                CreatedOn = Convert.ToDateTime(sqlDataReader["CreatedOn"]),
                                AmountPayable = Convert.ToDecimal(sqlDataReader["AmountPayable"]),
                                PaidAmount = Convert.ToDecimal(sqlDataReader["PaidAmount"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }

                return records;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void MakePayment(long pId, long[] pBuyerOrderIds, decimal[] pBuyersPayments)
        {
            try
            {
                for(int i = 0; i < pBuyerOrderIds.Length; i++)
                {
                    const string procMakePayment = "procMakePayment";
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        SqlCommand sqlCommand = new SqlCommand(procMakePayment, sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@pBuyerOrderId", SqlDbType.BigInt).Value = pBuyerOrderIds[i];
                        sqlCommand.Parameters.Add("@pBuyersPaidAmount", SqlDbType.Decimal).Value = pBuyersPayments[i];

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }

                const string procUpdateOrder = "procUpdateOrder";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procUpdateOrder, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = pId;

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PlaceOrder(long[] pProductIds, int[] pProductQuantity, long[] pBuyerIds, double[] pBuyersPCT, decimal pTotalPrice)
        {
            try
            {
                //Create Order First
                var order = new Orders();
                const string orderProc = "procInsertOrder";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(orderProc, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pTotalAmount", SqlDbType.Decimal).Value = pTotalPrice;
                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDatareader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDatareader.Read())
                        {
                            order.Id = Convert.ToInt64(sqlDatareader["Id"]);
                            order.OrderId = sqlDatareader["OrderId"].ToString();
                            order.OrderDate = Convert.ToDateTime(sqlDatareader["OrderDate"]);
                            order.IsPaid = Convert.ToBoolean(sqlDatareader["IsPaid"]);
                            order.OrderTotalAmount = Convert.ToDecimal(sqlDatareader["OrderTotalAmount"]);
                            order.CreatedOn = Convert.ToDateTime(sqlDatareader["CreatedOn"]);
                        }
                    }
                    sqlConnection.Close();
                }

                //Add Products of order to the table
                for (int i = 0; i < pProductIds.Length; i++)
                {
                    const string procProduct = "procInsertAndUpdateProductOrder";
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        SqlCommand sqlCommand = new SqlCommand(procProduct, sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@pProductId", SqlDbType.BigInt).Value = pProductIds[i];
                        sqlCommand.Parameters.Add("@pOrderId", SqlDbType.BigInt).Value = order.Id;
                        sqlCommand.Parameters.Add("@pOrderedQuantity", SqlDbType.Int).Value = pProductQuantity[i];

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }

                //Add Buyer of order to the table
                for (int i = 0; i < pBuyerIds.Length; i++)
                {
                    const string procBuyer = "procInsertBuyerOrdered";
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        SqlCommand sqlCommand = new SqlCommand(procBuyer, sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@pBuyerId", SqlDbType.BigInt).Value = pBuyerIds[i];
                        sqlCommand.Parameters.Add("@pOrderId", SqlDbType.BigInt).Value = order.Id;
                        sqlCommand.Parameters.Add("@pBuyerPCT", SqlDbType.Float).Value = pBuyersPCT[i];
                        sqlCommand.Parameters.Add("@pTotalPrice", SqlDbType.Decimal).Value = pTotalPrice;

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrder(Orders product)
        {
            throw new NotImplementedException();
        }
    }
}
