using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Entity
{
    public class Loan
    {

        public int LoanID { get; set; }
        public int CustomerID { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        // Default constructor
        public Loan() { }

        // Constructor with parameters
        public Loan(int loanID, int customerID, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanID = loanID;
            CustomerID = customerID;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public virtual void PrintAllInformation()
        {
            Console.WriteLine($"Loan ID: {LoanID}, Customer ID: {CustomerID}, Principal Amount: {PrincipalAmount}, Interest Rate: {InterestRate}, Loan Term: {LoanTerm}, Loan Type: {LoanType}, Loan Status: {LoanStatus}");
        }
    }

}

