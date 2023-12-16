//using Loan_Management.Entity;
//using Loan_Management.Exception;
//using Loan_Management.Repository;

//ICustomerRepository customerRepository = new CustomerRepository();

//// Example usage
//try
//{
//    // Create a new customer
//    Customer newCustomer = new Customer
//    {
//        CustomerID = 1,
//        Name = "John Doe",
//        EmailAddress = "john@example.com",
//        PhoneNumber = "1234567890",
//        Address = "123 Main Street",
//        CreditScore = 750
//    };

//    // Add the customer
//    customerRepository.AddCustomer(newCustomer);
//    Console.WriteLine("Customer added successfully.");

//    // Get a customer by ID
//    int customerId = 1;
//    Customer retrievedCustomer = customerRepository.GetCustomerById(customerId);
//    if (retrievedCustomer != null)
//    {
//        Console.WriteLine($"Retrieved Customer - ID: {retrievedCustomer.CustomerID}, Name: {retrievedCustomer.Name}");
//    }

//    // Update the customer
//    retrievedCustomer.Name = "Updated John Doe";
//    customerRepository.UpdateCustomer(retrievedCustomer);
//    Console.WriteLine("Customer updated successfully.");

//    // Delete the customer
//    customerRepository.DeleteCustomer(customerId);
//    Console.WriteLine("Customer deleted successfully.");

//    // Get all customers
//    var allCustomers = customerRepository.GetAllCustomers();
//    if (allCustomers.Count > 0)
//    {
//        Console.WriteLine("All Customers:");
//        foreach (var customer in allCustomers)
//        {
//            Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}");
//        }
//    }
//    else
//    {
//        Console.WriteLine("No customers found.");
//    }
//}
//catch (CustomerNotFoundException ex)
//{
//    Console.WriteLine($"Error: {ex.Message}");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
//}




using Loan_Management.Entity;
using Loan_Management.Repository;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;


ICustomerRepository cr = new CustomerRepository();
ILoanRepository lr = new LoanRepository();

Console.WriteLine("Welcome to the Customer Management System!");

while (true)
{
    Console.WriteLine("\nSelect an option:");
    Console.WriteLine("1. Add a new customer");
    Console.WriteLine("2. Update a customer");
    Console.WriteLine("3. Delete a customer");
    Console.WriteLine("4. Get a customer by ID");
    Console.WriteLine("5. Get all customers");
    Console.WriteLine("6. Exit");
    Console.WriteLine("7. Loan");


    Console.Write("Enter your choice: ");
    int choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:

            //Console.WriteLine("Enter Customer ID ...");
            //int id = int.Parse(Console.ReadLine());
            //Console.WriteLine("\nEnter Name ...");
            //string name = Console.ReadLine();
            //Console.WriteLine("\nEnter the Email-Id ...");
            //string mail = Console.ReadLine();
            //Console.WriteLine("\nEnter the Phone-Number ...");
            //string phone = Console.ReadLine();
            //Console.WriteLine("\nEnter the Address ...");
            //string address = Console.ReadLine();
            //Console.WriteLine("\nEnter the CreditScore ...");
            //string credit = Console.ReadLine();


            Console.WriteLine("\nEnter customer details:");
            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email Address: ");
            string emailAddress = Console.ReadLine();
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("Credit Score: ");
            int creditScore = int.Parse(Console.ReadLine());

            Customer newCustomer = new Customer()
            {
                CustomerID = customerId,
                Name = name,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                Address = address,
                CreditScore = creditScore
            };

            cr.AddCustomer(newCustomer);

            break;

        case 2:
            Console.WriteLine("\nEnter the CustomerID");
            int ccc = int.Parse(Console.ReadLine());
            int customerIdToUpdate = ccc;
            Console.WriteLine("\nEnter the New Phone_Number");
            string nnn = Console.ReadLine();

            string newPhoneNumber = nnn;

            Console.WriteLine($"Updating phone number for CustomerID: {customerIdToUpdate}");

            // Call the method to update the phone number
            cr.UpdateCustomer(customerIdToUpdate, newPhoneNumber);
            break;

        case 3:
            Console.WriteLine("\nEnter the CustomerID");
            int cc = int.Parse(Console.ReadLine());
            int customerIdToDelete = cc; 

            Console.WriteLine($"Deleting Customer with CustomerID: {customerIdToDelete}");

            // Call the method to delete the customer
            cr.DeleteCustomer(customerIdToDelete);
            break;

        case 4:
            Console.WriteLine("\nEnter the CustomerID");
            int cc1 = int.Parse(Console.ReadLine());

            int customerIdToRetrieve = cc1;

            Console.WriteLine($"Retrieving Customer with CustomerID: {customerIdToRetrieve}");

            // Call the method to retrieve the customer
            Customer retrievedCustomer = cr.GetCustomerById(customerIdToRetrieve);

            if (retrievedCustomer != null)
            {
                Console.WriteLine("Customer Details:");
                Console.WriteLine($"CustomerID: {retrievedCustomer.CustomerID}");
                Console.WriteLine($"Name: {retrievedCustomer.Name}");
                Console.WriteLine($"EmailAddress: {retrievedCustomer.EmailAddress}");
                Console.WriteLine($"PhoneNumber: {retrievedCustomer.PhoneNumber}");
                Console.WriteLine($"Address: {retrievedCustomer.Address}");
                Console.WriteLine($"CreditScore: {retrievedCustomer.CreditScore}");
            }
            else
            {
                Console.WriteLine("No customer found for the provided CustomerID.");
            }

            break;

        case 5:
            Console.WriteLine("Retrieving all customers...");

            // Call the method to retrieve all customers
            List<Customer> allCustomers = cr.GetAllCustomers();

            if (allCustomers.Count > 0)
            {
                Console.WriteLine("List of Customers:");

                foreach (var customer in allCustomers)
                {
                    Console.WriteLine($"CustomerID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.EmailAddress}, Phone: {customer.PhoneNumber}, Address: {customer.Address}, CreditScore: {customer.CreditScore}");
                }
            }
            else
            {
                Console.WriteLine("No customers found in the database.");
            }
            break;

        case 6:
            // Exit the application
            Console.WriteLine("Exiting the Customer Management System. Goodbye!");
            Environment.Exit(0);
            break;

            case 7:





            bool exit = false;

            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Apply Loan");
                Console.WriteLine("2. Calculate Interest");
                Console.WriteLine("3. Check Loan Status");
                Console.WriteLine("4. Check Loan Repayment");
                Console.WriteLine("5. Get ALl Loans");
                Console.WriteLine("6. Get Loan By Id");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                int ccc111;
                if (!int.TryParse(Console.ReadLine(), out ccc111))
                {
                    Console.WriteLine("Please enter a valid option.");
                    continue;
                }

                switch (ccc111)
                {
                    case 1:



                      bool exitLoanMenu = false;

                        do
                        {
                            Console.WriteLine("Choose an option:");
                            Console.WriteLine("1. Apply for a Home Loan");
                            Console.WriteLine("2. Apply for a Car Loan");
                            Console.WriteLine("3. Exit");
                            Console.Write("Enter your choice: ");

                            int loanMenuChoice;
                            if (!int.TryParse(Console.ReadLine(), out loanMenuChoice))
                            {
                                Console.WriteLine("Please enter a valid option.");
                                continue;
                            }

                            switch (loanMenuChoice)
                            {
                                case 1:
                                    Console.Write("Enter Loan Type (Home Loan): ");
                                    string homeLoanType = Console.ReadLine();

                                    // Ask for specific details for Home Loan
                                    Console.Write("Enter Property Address: ");
                                    string propertyAddress = Console.ReadLine();

                                    Console.Write("Enter Property Value: ");
                                    int propertyValue;
                                    while (!int.TryParse(Console.ReadLine(), out propertyValue))
                                    {
                                        Console.WriteLine("Please enter a valid Property Value.");
                                        Console.Write("Enter Property Value: ");
                                    }

                                    Console.WriteLine("\nEnter Loan details:");
                                    Console.Write("Loan ID: ");
                                    int lid = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Customer ID");
                                    int cid = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter Principal_Amount:");
                                    decimal pm = decimal.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter Interest_Rate:");
                                    decimal ir = decimal.Parse(Console.ReadLine());
                                    Console.WriteLine("Loan_Term");
                                    int lt = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Loan_Status:");
                                    string ls = Console.ReadLine();

                                    // Create a HomeLoan instance and set the specific details
                                    HomeLoan homeLoan = new HomeLoan()
                                    {
                                        LoanID = lid,
                                        PropertyAddress = propertyAddress,
                                        PropertyValue = propertyValue,
                                        CustomerID = cid,
                                        PrincipalAmount = pm,
                                        InterestRate = ir,
                                        LoanTerm = lt,
                                        LoanType = homeLoanType,
                                        LoanStatus = ls
                                    };


                                    lr.ApplyLoan(homeLoan);
                                    break;

                                case 2:
                                    Console.Write("Enter Loan Type (Car Loan): ");
                                    string carLoanType = Console.ReadLine();

                                    // Ask for specific details for Car Loan (Car Model, Car Value)
                                    Console.Write("Enter Car Model: ");
                                    string carModel = Console.ReadLine();

                                    Console.Write("Enter Car Value: ");
                                    int carValue = int.Parse(Console.ReadLine());


                                    Console.WriteLine("\nEnter Loan details:");
                                    Console.Write("Loan ID: ");
                                    int lid1 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Customer ID");
                                    int cid1 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter Principal_Amount:");
                                    decimal pm1 = decimal.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter Interest_Rate:");
                                    decimal ir1 = decimal.Parse(Console.ReadLine());
                                    Console.WriteLine("Loan_Term");
                                    int lt1 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Loan_Status:");
                                    string ls1 = Console.ReadLine();

                                    // Create a CarLoan instance and set the specific details
                                    CarLoan carLoan = new CarLoan()
                                    {
                                        LoanID = lid1,
                                        CustomerID = cid1,
                                        PrincipalAmount = pm1,
                                        InterestRate = ir1,
                                        LoanTerm = lt1,
                                        LoanStatus = ls1,
                                        LoanType = carLoanType,
                                        CarModel = carModel,
                                        CarValue = carValue
                                    };

                                    // Apply for the Car Loan using the Loan Repository
                                    lr.ApplyLoan(carLoan);
                                    break;

                                case 3:
                                    exitLoanMenu = true;
                                    break;

                                default:
                                    Console.WriteLine("Invalid option. Please choose a valid option.");
                                    break;
                            }

                        } while (exitLoanMenu);
                           

                        break;

                    case 2:
                        Console.Write("Loan ID: ");
                        int lidd = int.Parse(Console.ReadLine());
                        int igli = lidd; // Replace this with an existing loan ID from your database
                        // Calculate interest for a specific loan
                        decimal iner = lr.CalculateInterest(igli);

                        if (iner != 0)
                        {
                            Console.WriteLine($"Interest for Loan ID {igli}: {igli:C2}");
                        }
                        else
                        {
                            Console.WriteLine("Interest calculation failed or loan not found.");
                        }
                        break;

                    case 3:

                        Console.Write("Enter Loan ID: ");
                        int loanId;
                        while (!int.TryParse(Console.ReadLine(), out loanId))
                        {
                            Console.WriteLine("Please enter a valid Loan ID.");
                            Console.Write("Enter Loan ID: ");
                        }


                        string status = lr.LoanStatus(loanId);

                        switch (status)
                        {
                            case "Approved":
                                Console.WriteLine("Congratulations! Your loan is approved.");
                                break;
                            case "Rejected":
                                Console.WriteLine("Sorry, your loan application is rejected.");
                                break;
                            case "Loan not found":
                                Console.WriteLine("Loan not found.");
                                break;
                            case "Error":
                                Console.WriteLine("An error occurred while processing the request.");
                                break;
                            default:
                                Console.WriteLine("Unable to determine the loan status.");
                                break;
                        }
                        break;

                    case 4:

                        Console.Write("Loan ID: ");
                        int id341 = int.Parse(Console.ReadLine());
                        int qwe = id341; // Replace with an existing loan ID from your database
                        decimal paymentAmount = 1500; // Replace with the payment amount

                        int noOfEmiPaid = lr.LoanRepayment(qwe, paymentAmount);
                        Console.WriteLine($"No. of EMIs paid: {noOfEmiPaid}");


                        break;


                    case 5:

                        var al = lr.GetAllLoans();
                        Console.WriteLine("All Loans:");
                        foreach (var loan in al)
                        {
                            Console.WriteLine($"LoanID: {loan.LoanID}, PrincipalAmount: {loan.PrincipalAmount}, InterestRate: {loan.InterestRate}, LoanTerm: {loan.LoanTerm}");
                        }


                        break; 
                    
                    case 6:
                        Console.Write("Loan ID: ");
                        int nm = int.Parse(Console.ReadLine());
                        int lf = nm; // Replace with an existing loan ID from your database
                        var vc = lr.GetLoanById(lf);
                        if (vc != null)
                        {
                            Console.WriteLine($"Loan fetched with ID {vc.LoanID}: PrincipalAmount: {vc.PrincipalAmount}, InterestRate: {vc.InterestRate}, LoanTerm: {vc.LoanTerm}");
                            // Display other fetched loan properties as needed
                        }
                        else
                        {
                            Console.WriteLine("Loan not found.");
                            // Handle loan not found scenario
                        }

                        break;

                    case 7:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            } while (!exit);
            break;


        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}
        



static int GetCustomerIdFromUser(string operation)
{
    Console.Write($"\nEnter the Customer ID to {operation}: ");
    return int.Parse(Console.ReadLine());
}






















//LoanRepository loanRepository = new LoanRepository(connectionString);

//// Example loan details for interest calculation
//int loanId = 123; // Replace this with an existing loan ID from your database
//decimal principalAmount = 50000;
//decimal interestRate = 5.5m;
//int loanTerm = 24;

//// Calculate interest for a specific loan using provided parameters
//decimal interest = loanRepository.CalculateInterest(loanId, principalAmount, interestRate, loanTerm);

//if (interest != 0)
//{
//    Console.WriteLine($"Interest for Loan ID {loanId}: {interest:C2}");
//}
//else
//{
//    Console.WriteLine("Interest calculation failed or loan not found.");
//    // Handle the scenario when interest calculation fails or the loan is not found
//    // You can log the error or display an appropriate message based on your application's requirements
//}





//LoanRepository loanRepository = new LoanRepository(connectionString);

//// Example 1: Calculate EMI using loan details from the database
//int loanId = 123; // Replace this with an existing loan ID from your database
//decimal emiFromDb = loanRepository.CalculateEMI(loanId);
//Console.WriteLine($"EMI calculated from database details: {emiFromDb:C2}");

//// Example 2: Calculate EMI using provided parameters
//decimal principalAmount = 50000;
//decimal interestRate = 5.5m;
//int loanTerm = 24;

//decimal emiFromParams = loanRepository.CalculateEMI(principalAmount, interestRate, loanTerm);
//Console.WriteLine($"EMI calculated from parameters: {emiFromParams:C2}");



















//LoanRepository loanRepository = new LoanRepository(connectionString);

//// Example 2: Get all loans
//var allLoans = loanRepository.GetAllLoans();
//Console.WriteLine("All Loans:");
//foreach (var loan in allLoans)
//{
//    Console.WriteLine($"LoanID: {loan.LoanID}, PrincipalAmount: {loan.PrincipalAmount}, InterestRate: {loan.InterestRate}, LoanTerm: {loan.LoanTerm}");
//    // Display other loan properties as needed
//}

//// Example 3: Get loan by ID
//int loanToFetch = 456; // Replace with an existing loan ID from your database
//var fetchedLoan = loanRepository.GetLoanById(loanToFetch);
//if (fetchedLoan != null)
//{
//    Console.WriteLine($"Loan fetched with ID {fetchedLoan.LoanID}: PrincipalAmount: {fetchedLoan.PrincipalAmount}, InterestRate: {fetchedLoan.InterestRate}, LoanTerm: {fetchedLoan.LoanTerm}");
//    // Display other fetched loan properties as needed
//}
//else
//{
//    Console.WriteLine("Loan not found.");
//    // Handle loan not found scenario
//}















