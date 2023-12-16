using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management.Exception
{
    internal class CustomerNotFoundException : ApplicationException
    {

        public CustomerNotFoundException() { }

        public CustomerNotFoundException(string message) : base(message) { }

        public CustomerNotFoundException(string message, ApplicationException innerException) : base(message, innerException) { }

        protected CustomerNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
