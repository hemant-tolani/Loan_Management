using Loan_Management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Repository
{
    internal interface ICustomerRepository
    {

        void AddCustomer(Customer customer);
        void UpdateCustomer(int customerId, string newPhoneNumber);
        void DeleteCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        List<Customer> GetAllCustomers();

    }
}
