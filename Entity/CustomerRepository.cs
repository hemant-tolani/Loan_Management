using Loan_Management.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Loan_Management.Utility;
using Loan_Management.Exception;
using System.Data;

namespace Loan_Management.Entity
{
    internal class CustomerRepository : ICustomerRepository
    {

        //public List<Customer> customers = new List<Customer>();

        public string connectionString;
        SqlCommand cmd = null;



        public CustomerRepository()
        {

            //sqlConnection = new SqlConnection("Server=DESKTOP-0TE71RT;Database=PRODUCTAPPDB;Trusted_Connection=True");
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }


        //public void AddCustomer(Customer customer)
        //{
        //    try
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (CustomerID, Name, EmailAddress, PhoneNumber, Address, CreditScore) " +
        //                                                    "VALUES (@CustomerID, @Name, @EmailAddress, @PhoneNumber, @Address, @CreditScore)", sqlConnection))
        //            {
        //                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
        //                cmd.Parameters.AddWithValue("@Name", customer.Name);
        //                cmd.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
        //                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
        //                cmd.Parameters.AddWithValue("@Address", customer.Address);
        //                cmd.Parameters.AddWithValue("@CreditScore", customer.CreditScore);

        //                cmd.Connection = sqlConnection;
        //                sqlConnection.Open();
        //                int rowsAffected = cmd.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    Console.WriteLine("Customer added successfully.");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Failed to add customer.");
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new CustomerNotFoundException("Error occurred while adding customer.");
        //    }
        //}

        public void UpdateCustomer(int customerId, string newPhoneNumber)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;

                // Check if CustomerID exists in the database
                cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                sqlConnection.Open();
                int existingCustomerCount = (int)cmd.ExecuteScalar();

                if (existingCustomerCount > 0)
                {
                    // Update PhoneNumber if CustomerID exists
                    cmd.CommandText = "UPDATE Customer SET PhoneNumber = @NewPhoneNumber WHERE CustomerID = @CustomerID";
                    cmd.Parameters.Clear(); // Clear previous parameters
                    cmd.Parameters.AddWithValue("@NewPhoneNumber", newPhoneNumber);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Customer PhoneNumber updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update Customer PhoneNumber.");
                    }
                }
                else
                {
                    Console.WriteLine("CustomerID not found. Cannot update PhoneNumber.");
                }
            }
        }



        //public void AddCustomer(Customer customer)
        //{
        //    Console.WriteLine(customer.CreditScore);
        //    try
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //        {
        //            cmd.CommandText = "INSERT INTO Customer (CustomerID, Name, EmailAddress, PhoneNumber, Address, CreditScore) VALUES (@CustomerID, @Name, @EmailAddress, @PhoneNumber, @Address, @CreditScore";
        //                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
        //                cmd.Parameters.AddWithValue("@Name", customer.Name);
        //                cmd.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
        //                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
        //                cmd.Parameters.AddWithValue("@Address", customer.Address);
        //                cmd.Parameters.AddWithValue("@CreditScore", customer.CreditScore);

        //                cmd.Connection = sqlConnection;
        //                sqlConnection.Open();
        //                int rowsAffected = cmd.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    Console.WriteLine("Customer added successfully.");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Failed to add customer.");

        //                }
        //            }

        //    }
        //    catch (SqlException ex)
        //    {
        //        // Log the SQL exception details for debugging purposes
        //        Console.WriteLine("SQL Exception: " + ex.Message);

        //        // Rethrow the original exception or handle it accordingly
        //        throw; // Re-throw the same exception to maintain original exception details
        //    }

        //}



        public void AddCustomer(Customer customer)
        {
            Console.WriteLine(customer.CreditScore);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;

                // Check if CustomerID already exists
                cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                sqlConnection.Open();
                int existingCustomerCount = (int)cmd.ExecuteScalar();

                // If CustomerID doesn't exist, add the customer
                if (existingCustomerCount == 0)
                {
                    cmd.CommandText = "INSERT INTO Customer (CustomerID, Name, EmailAddress, PhoneNumber, Address, CreditScore) " +
                        "VALUES (@CustomerID, @Name, @EmailAddress, @PhoneNumber, @Address, @CreditScore)";

                    cmd.Parameters.Clear(); // Clear previous parameters
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@CreditScore", customer.CreditScore);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Customer added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add customer.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Log the SQL exception details for debugging purposes
                        Console.WriteLine("SQL Exception: " + ex.Message);
                        // Rethrow the original exception or handle it accordingly
                        throw; // Re-throw the same exception to maintain original exception details
                    }
                }
                else
                {
                    Console.WriteLine("CustomerID already exists. Skipping addition of customer.");
                }
            }
        }




        //public void AddCustomer(Customer customer)
        //{
        //    Console.WriteLine(customer.CreditScore);

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = sqlConnection;

        //        // Check if CustomerID already exists
        //        cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerID";
        //        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

        //        sqlConnection.Open();
        //        int existingCustomerCount = (int)cmd.ExecuteScalar();

        //        // If CustomerID doesn't exist, add the customer
        //        if (existingCustomerCount == 0)
        //        {
        //            cmd.CommandText = "INSERT INTO Customer (CustomerID, Name, EmailAddress, PhoneNumber, Address, CreditScore) " +
        //                "VALUES (@CustomerID, @Name, @EmailAddress, @PhoneNumber, @Address, @CreditScore)";

        //            cmd.Parameters.Clear(); // Clear previous parameters
        //            cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
        //            cmd.Parameters.AddWithValue("@Name", customer.Name);
        //            cmd.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
        //            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
        //            cmd.Parameters.AddWithValue("@Address", customer.Address);
        //            cmd.Parameters.AddWithValue("@CreditScore", customer.CreditScore);

        //            int rowsAffected = cmd.ExecuteNonQuery();

        //            if (rowsAffected > 0)
        //            {
        //                Console.WriteLine("Customer added successfully.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to add customer.");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("CustomerID already exists. Skipping addition of customer.");
        //        }
        //    }
        //}




        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;

                // Check if CustomerID exists in the database
                cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                sqlConnection.Open();
                int existingCustomerCount = (int)cmd.ExecuteScalar();

                if (existingCustomerCount > 0)
                {
                    // Delete Customer if CustomerID exists
                    cmd.CommandText = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                    cmd.Parameters.Clear(); // Clear previous parameters
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Customer deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete Customer.");
                    }
                }
                else
                {
                    Console.WriteLine("CustomerID not found. Cannot delete Customer.");
                }
            }
        }


        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;

                // Retrieve Customer by CustomerID
                cmd.CommandText = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        Name = reader["Name"].ToString(),
                        EmailAddress = reader["EmailAddress"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        CreditScore = Convert.ToInt32(reader["CreditScore"])
                    };
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }

            return customer;
        }



























        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;

                // Retrieve all customers
                cmd.CommandText = "SELECT * FROM Customer";

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Create a Customer object for each retrieved row and add it to the list
                    Customer customer = new Customer
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        Name = reader["Name"].ToString(),
                        EmailAddress = reader["EmailAddress"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        CreditScore = Convert.ToInt32(reader["CreditScore"])
                    };

                    customers.Add(customer);
                }
            }

            return customers;
        }




    }
}
