using SalesProcess.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesProcess.Core.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SalesProcess.Core.DataModels;

namespace SalesProcess.Infrastucture.Services
{
    public class BuyersServices : IBuyersService
    {
        private string connection;
        public BuyersServices()
        {
            connection = ConfigurationManager.ConnectionStrings["SalesProcessDBContext"].ConnectionString;
        }
        public Buyers AddBuyer(Buyers buyer)
        {
            try
            {
                const string procedureName = "procInsertUpdateBuyer";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = buyer.Id;
                    sqlCommand.Parameters.Add("@pBuyersName", SqlDbType.NVarChar, 128).Value = buyer.BuyersName;
                    sqlCommand.Parameters.Add("@pBuyersCode", SqlDbType.NVarChar, 16).Value = buyer.BuyersCode;
                    sqlCommand.Parameters.Add("@pBuyersRegion", SqlDbType.NVarChar, 32).Value = buyer.BuyersRegion;
                    sqlCommand.Parameters.Add("@pBuyersMobile", SqlDbType.NVarChar, 14).Value = buyer.BuyersMobile;
                    sqlCommand.Parameters.Add("@pBuyersEmail", SqlDbType.NVarChar, 32).Value = buyer.BuyersEmail;
                    sqlCommand.Parameters.Add("@pIsUpdate", SqlDbType.Bit).Value = false;

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                return new Buyers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBuyer(long id)
        {
            try
            {
                const string procedureName = "procDeleteBuyers";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = id;

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Buyers> GetAllBuyers(bool? pIsActive)
        {
            try
            {
                var buyers = new List<Buyers>();
                const string procedureName = "procListOfBuyers";

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pIsActive", SqlDbType.Bit).Value = (object)pIsActive ?? DBNull.Value;

                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            buyers.Add(new Buyers
                            {
                                Id = Convert.ToInt32(sqlDataReader["Id"]),
                                BuyersName = sqlDataReader["BuyersName"].ToString(),
                                BuyersCode = sqlDataReader["BuyersCode"].ToString(),
                                BuyersRegion = sqlDataReader["BuyersRegion"].ToString(),
                                BuyersMobile = sqlDataReader["BuyersMobile"].ToString(),
                                BuyersEmail = sqlDataReader["BuyersEmail"].ToString(),
                                IsActive = Convert.ToBoolean(sqlDataReader["IsActive"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }
                return buyers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Buyers GetBuyer(long id)
        {
            try
            {
                var buyer = new Buyers();
                const string procedureName = "procGetBuyer";

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            buyer.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            buyer.BuyersName = sqlDataReader["BuyersName"].ToString();
                            buyer.BuyersCode = sqlDataReader["BuyersCode"].ToString();
                            buyer.BuyersRegion = sqlDataReader["BuyersRegion"].ToString();
                            buyer.BuyersMobile = sqlDataReader["BuyersMobile"].ToString();
                            buyer.BuyersEmail = sqlDataReader["BuyersEmail"].ToString();
                        }
                    }
                    sqlConnection.Close();
                }
                return buyer;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<BuyersOrderDataModel> GetBuyersOrderedByOrder(long orderId)
        {
            try
            {
                var products = new List<BuyersOrderDataModel>();
                const string procedureName = "procListOfBuyersOrderedByOrder";

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pOrderId", SqlDbType.BigInt).Value = orderId;

                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            products.Add(new BuyersOrderDataModel
                            {
                                BuyersName = sqlDataReader["BuyersName"].ToString(),
                                TotalAmount = Convert.ToDecimal(sqlDataReader["AmountPayable"]),
                                PayablePCT = Convert.ToDouble(sqlDataReader["PayablePCT"]),
                                DueAmount = Convert.ToDecimal(sqlDataReader["DueAmount"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }

                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBuyer(Buyers buyer)
        {
            try
            {
                const string procedureName = "procInsertUpdateBuyer";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = buyer.Id;
                    sqlCommand.Parameters.Add("@pBuyersName", SqlDbType.NVarChar, 128).Value = buyer.BuyersName;
                    sqlCommand.Parameters.Add("@pBuyersCode", SqlDbType.NVarChar, 16).Value = buyer.BuyersCode;
                    sqlCommand.Parameters.Add("@pBuyersRegion", SqlDbType.NVarChar, 32).Value = buyer.BuyersRegion;
                    sqlCommand.Parameters.Add("@pBuyersMobile", SqlDbType.NVarChar, 14).Value = buyer.BuyersMobile;
                    sqlCommand.Parameters.Add("@pBuyersEmail", SqlDbType.NVarChar, 32).Value = buyer.BuyersEmail;
                    sqlCommand.Parameters.Add("@pIsUpdate", SqlDbType.Bit).Value = true;

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
    }
}
