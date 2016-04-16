﻿
using Dapper;
using DapperDriven.Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DapperDriven
{
    public class Repository
    {
        public Invoice GetInvoice(int id)
        {
            Invoice invoice;

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                sqlConnection.Open();

                invoice = sqlConnection
                    .Query<Invoice, Customer, Invoice>(
                        @"
                    select 
                        Invoice.*, Customer.* 
                    from 
                        Invoice join Customer 
                    on 
                        Invoice.CustomerId = Customer.CustomerId
                    where
                        Invoice.InvoiceId = @InvoiceId",
                        (i, c) =>
                        {
                            i.Customer = c;
                            return i;
                        },
                        new { InvoiceId = id },
                        splitOn: "CustomerId" // use splitOn, if the id field is not Id or ID
                    )
                    .FirstOrDefault();

                //foreach (Product product in products)
                //{
                //    ObjectDumper.Write(product.Supplier);
                //}

                sqlConnection.Close();
            }

            return invoice;
        }
    }
}