using Loan_Management.Repository;
using Loan_Management.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Entity
{
    internal class LoanRepository : ILoanRepository
    {


        public string connectionString;
        SqlCommand cmd = null;
        public LoanRepository()
        {
            //sqlConnection = new SqlConnection("Server=DESKTOP-0TE71RT;Database=PRODUCTAPPDB;Trusted_Connection=True");
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public void ApplyLoan(Loan loan)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Loan (LoanID, CustomerID, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) " +
                                                           "VALUES (@LoanID, @CustomerID, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)", sqlConnection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@LoanID", loan.LoanID);
                        cmd.Parameters.AddWithValue("@CustomerID", loan.CustomerID);
                        cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                        cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                        cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                        cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
                        cmd.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);

                        cmd.ExecuteNonQuery();
                    }

                    if (loan is HomeLoan homeLoan)
                    {
                        using (SqlCommand homeLoanCmd = new SqlCommand("INSERT INTO HomeLoan (LoanID, PropertyAddress, PropertyValue) " +
                                                                       "VALUES (@LoanID, @PropertyAddress, @PropertyValue)", sqlConnection, transaction))
                        {
                            homeLoanCmd.Parameters.AddWithValue("@LoanID", homeLoan.LoanID);
                            homeLoanCmd.Parameters.AddWithValue("@PropertyAddress", homeLoan.PropertyAddress);
                            homeLoanCmd.Parameters.AddWithValue("@PropertyValue", homeLoan.PropertyValue);

                            homeLoanCmd.ExecuteNonQuery();
                        }
                    }
                    else if (loan is CarLoan carLoan)
                    {
                        using (SqlCommand carLoanCmd = new SqlCommand("INSERT INTO CarLoan (LoanID, CarModel, CarValue) " +
                                                                      "VALUES (@LoanID, @CarModel, @CarValue)", sqlConnection, transaction))
                        {
                            carLoanCmd.Parameters.AddWithValue("@LoanID", carLoan.LoanID);
                            carLoanCmd.Parameters.AddWithValue("@CarModel", carLoan.CarModel);
                            carLoanCmd.Parameters.AddWithValue("@CarValue", carLoan.CarValue);

                            carLoanCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Loan application successful.");
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error applying for loan: {ex.Message}");
                   
                }
            }
        }


        public decimal CalculateInterest(int loanId)
        {
            decimal interest = 0;
            bool loanFound = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanID = @LoanID";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@LoanID", loanId);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        loanFound = true;

                        decimal principalAmount = Convert.ToDecimal(reader["PrincipalAmount"]);
                        decimal interestRate = Convert.ToDecimal(reader["InterestRate"]);
                        int loanTerm = Convert.ToInt32(reader["LoanTerm"]);

                        // Calculate the interest amount
                        interest = (principalAmount * interestRate * loanTerm) / 12;
                    }
                }
            }

            if (!loanFound)
            {
                Console.WriteLine("Loan not found.");
                // Handle loan not found scenario
                // You can set a default interest value, throw an exception, or return an error status based on your requirements
            }

            return interest;
        }


        public decimal CalculateInterest(int loanId, decimal principalAmount, decimal interestRate, int loanTerm)
        {
            decimal interest = 0;
            bool loanFound = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT LoanID FROM Loan WHERE LoanID = @LoanID";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@LoanID", loanId);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        loanFound = true;
                        // Calculate the interest amount using the provided parameters
                        interest = (principalAmount * interestRate * loanTerm) / 12;
                    }
                    else
                    {
                        Console.WriteLine("Loan not found.");
                        // Handle loan not found scenario
                        // You can set a default interest value, return an error status, or log a message based on your requirements
                    }
                }
            }

            return interest;
        }

        public string LoanStatus(int loanId)
        {
            string status = "";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT C.CreditScore FROM Loan L JOIN Customer C ON L.CustomerID = C.CustomerID WHERE L.LoanID = @LoanId", sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@LoanId", loanId);

                        sqlConnection.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int creditScore = Convert.ToInt32(result);

                            if (creditScore > 650)
                            {
                                status = "Approved";
                            }
                            else
                            {
                                status = "Rejected";
                            }
                        }
                        else
                        {
                            status = "Loan not found";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                // Log the exception for debugging purposes
                status = "Error";
            }

            return status;
        }



        private void UpdateLoanStatus(int loanId, string status)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Loan SET LoanStatus = @Status WHERE LoanID = @LoanID";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, sqlConnection))
                {
                    updateCmd.Parameters.AddWithValue("@Status", status);
                    updateCmd.Parameters.AddWithValue("@LoanID", loanId);

                    sqlConnection.Open();
                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Loan status updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update loan status.");
                        // Handle the update failure scenario
                        // You can log an error message or return an error status based on your requirements
                    }
                }
            }
        }

        //public decimal CalculateEMI(int loanId)
        //{
        //    decimal emi = 0;

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanID = @LoanID";

        //        using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
        //        {
        //            cmd.Parameters.AddWithValue("@LoanID", loanId);

        //            sqlConnection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.Read())
        //            {
        //                decimal principalAmount = Convert.ToDecimal(reader["PrincipalAmount"]);
        //                decimal interestRate = Convert.ToDecimal(reader["InterestRate"]);
        //                int loanTerm = Convert.ToInt32(reader["LoanTerm"]);

        //                emi = CalculateEMI(principalAmount, interestRate, loanTerm);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Loan not found.");
        //                // Handle loan not found scenario
        //                // You can set a default EMI value, return an error status, or log a message based on your requirements
        //            }
        //        }
        //    }

        //    return emi;
        //}

        private decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            decimal emi = 0;

            decimal monthlyInterestRate = interestRate / 12 / 100;
            decimal numerator = principalAmount * monthlyInterestRate * (decimal)Math.Pow(1 + (double)monthlyInterestRate, loanTerm);
            decimal denominator = (decimal)Math.Pow(1 + (double)monthlyInterestRate, loanTerm) - 1;

            emi = numerator / denominator;

            return emi;
        }


        public int LoanRepayment(int loanId, decimal amount)
        {
            int noOfEmiPaid = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanID = @LoanID";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@LoanID", loanId);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        decimal principalAmount = Convert.ToDecimal(reader["PrincipalAmount"]);
                        decimal interestRate = Convert.ToDecimal(reader["InterestRate"]);
                        int loanTerm = Convert.ToInt32(reader["LoanTerm"]);

                        decimal emi = CalculateEMI(principalAmount, interestRate, loanTerm);

                        if (amount >= emi)
                        {
                            noOfEmiPaid = (int)(amount / emi);
                            Console.WriteLine($"Paid {noOfEmiPaid} EMI(s) from the given amount.");
                            // Update the loan status or payment details in the database accordingly
                            // Implement the necessary database update logic here
                        }
                        else
                        {
                            Console.WriteLine("Payment amount is less than the EMI. Payment rejected.");
                            // Handle payment rejection scenario
                            // You can return an error status or log a message based on your requirements
                        }
                    }
                    else
                    {
                        Console.WriteLine("Loan not found.");
                        // Handle loan not found scenario
                        // You can return an error status or log a message based on your requirements
                    }
                }
            }

            return noOfEmiPaid;
        }


        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Loan";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Loan loan = new Loan
                        {
                            LoanID = Convert.ToInt32(reader["LoanID"]),
                            // Populate other loan properties here...
                        };
                        loans.Add(loan);
                    }
                }
            }

            return loans;
        }


        public Loan GetLoanById(int loanId)
        {
            Loan loan = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Loan WHERE LoanID = @LoanID";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@LoanID", loanId);

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        loan = new Loan
                        {
                            LoanID = Convert.ToInt32(reader["LoanID"]),
                            // Populate other loan properties here...
                        };
                    }
                    else
                    {
                        Console.WriteLine("Loan not found.");
                        // Handle loan not found scenario
                        // You can return an error status or log a message based on your requirements
                    }
                }
            }

            return loan;
        }



    }
}
