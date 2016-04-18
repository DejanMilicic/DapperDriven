
using Dapper;
using DapperDriven.Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DapperDriven
{
    public class Repository
    {
        public Invoice GetInvoiceExpandLevel1(int id)
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

        public Invoice GetInvoiceExpandLevel2(int id)
        {
            Invoice invoice;

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                sqlConnection.Open();

                invoice = sqlConnection
                    .Query<Invoice, Customer, Employee, Invoice>(
                        @"
                    select 
                        Invoice.*, Customer.*, Employee.*
                    from 
                        Invoice 
                    join 
                        Customer
                    on 
                        Invoice.CustomerId = Customer.CustomerId
                    join
                        Employee
                    on
                        Customer.SupportRepId = Employee.EmployeeId
                    where
                        Invoice.InvoiceId = @InvoiceId",
                        (i, c, e) =>
                        {
                            i.Customer = c;
                            i.Customer.SupportRep = e;
                            return i;
                        },
                        new { InvoiceId = id },
                        splitOn: "CustomerId, EmployeeId" // use splitOn, if the id field is not Id or ID
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
