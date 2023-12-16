using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Entity
{
    public class CarLoan : Loan
    {

        public string CarModel { get; set; }
        public int CarValue { get; set; }

        // Default constructor
        public CarLoan() { }

        // Constructor with parameters
        public CarLoan(int loanId, int customerId, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus, string carModel, int carValue)
            : base(loanId, customerId, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public override void PrintAllInformation()
        {
            base.PrintAllInformation();
            Console.WriteLine($"Car Model: {CarModel}, Car Value: {CarValue}");
        }


    }
}
