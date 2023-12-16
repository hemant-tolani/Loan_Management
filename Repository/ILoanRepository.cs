using Loan_Management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Repository
{
    internal interface ILoanRepository
    {

        void ApplyLoan(Loan loan); // Method to apply for a loan

        decimal CalculateInterest(int loanId);
        decimal CalculateInterest(int loanId, decimal principalAmount, decimal interestRate, int loanTerm); // Method to calculate interest for a loan
        string LoanStatus(int loanId); // Method to check the status of a loan
        int LoanRepayment(int loanId, decimal amount); // Method to repay a loan
        List<Loan> GetAllLoans(); // Method to get all loans
        Loan GetLoanById(int loanId); //
    }
}
