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
    public class ProductsServices : IProductsService
    {
        private string connection;
        public ProductsServices()
        {
            connection = ConfigurationManager.ConnectionStrings["SalesProcessDBContext"].ConnectionString;
        }

        public void AddProduct(Products product)
        {
            try
            {
                const string procedureName = "procInsertUpdateProduct";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = product.Id;
                    sqlCommand.Parameters.Add("@pProductName", SqlDbType.NVarChar, 128).Value = product.ProductName;
                    sqlCommand.Parameters.Add("@pProductCode", SqlDbType.NVarChar, 16).Value = product.ProductCode;
                    sqlCommand.Parameters.Add("@pProductUnit", SqlDbType.NVarChar, 32).Value = product.ProductUnit;
                    sqlCommand.Parameters.Add("@pProductPrice", SqlDbType.Float).Value = product.ProductPrice;
                    sqlCommand.Parameters.Add("@pStockQuantity", SqlDbType.Int).Value = product.StockQuantity;
                    sqlCommand.Parameters.Add("@pIsUpdate", SqlDbType.Bit).Value = false;

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

        public void DeleteProduct(long id)
        {
            try
            {
                const string procedureName = "procDeleteProducts";
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

        public List<Products> GetAllProducts(bool? pIsAvailable)
        {
            try
            {
                var products = new List<Products>();
                const string procedureName = "procListOfProducts";

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pIsAvailable", SqlDbType.Bit).Value = (object)pIsAvailable ?? DBNull.Value;
                    sqlConnection.Open();
                    sqlCommand.CommandTimeout = 120;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            products.Add(new Products
                            {
                                Id = Convert.ToInt32(sqlDataReader["Id"]),
                                ProductName = sqlDataReader["ProductName"].ToString(),
                                ProductCode = sqlDataReader["ProductCode"].ToString(),
                                ProductUnit = sqlDataReader["ProductUnit"].ToString(),
                                ProductPrice = Convert.ToDouble(sqlDataReader["ProductPrice"]),
                                StockQuantity = Convert.ToInt32(sqlDataReader["StockQuantity"]),
                                IsAvailable = Convert.ToBoolean(sqlDataReader["IsAvailable"]),
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

        public Products GetProduct(long id)
        {
            throw new NotImplementedException();
        }

        public List<Products> GetProductsByBuyer(long id)
        {
            throw new NotImplementedException();
        }

        public List<ProductsOrderedDataModel> GetProductsOrderedByOrder(long orderId)
        {
            try
            {
                var products = new List<ProductsOrderedDataModel>();
                const string procedureName = "procListOfProductsOrderedByOrder";

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
                            products.Add(new ProductsOrderedDataModel
                            {
                                ProductName = sqlDataReader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(sqlDataReader["Quantity"]),
                                Price = Convert.ToDecimal(sqlDataReader["Price"])
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

        public void UpdateProduct(Products product)
        {
            try
            {
                const string procedureName = "procInsertUpdateProduct";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pId", SqlDbType.BigInt).Value = product.Id;
                    sqlCommand.Parameters.Add("@pProductName", SqlDbType.NVarChar, 128).Value = product.ProductName;
                    sqlCommand.Parameters.Add("@pProductCode", SqlDbType.NVarChar, 16).Value = product.ProductCode;
                    sqlCommand.Parameters.Add("@pProductUnit", SqlDbType.NVarChar, 32).Value = product.ProductUnit;
                    sqlCommand.Parameters.Add("@pProductPrice", SqlDbType.Float).Value = product.ProductPrice;
                    sqlCommand.Parameters.Add("@pStockQuantity", SqlDbType.Int).Value = product.StockQuantity;
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
