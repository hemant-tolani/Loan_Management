using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Entity
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public int PropertyValue { get; set; }

        // Default constructor
        public HomeLoan() { }

        // Constructor with parameters
        public HomeLoan(int loanId, int customerId, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus, string propertyAddress, int propertyValue)
            : base(loanId, customerId, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public override void PrintAllInformation()
        {
            base.PrintAllInformation();
            Console.WriteLine($"Property Address: {PropertyAddress}, Property Value: {PropertyValue}");
        }

    }
}
