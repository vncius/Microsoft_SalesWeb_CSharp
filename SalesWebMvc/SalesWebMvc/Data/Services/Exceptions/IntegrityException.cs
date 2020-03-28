using System;

namespace SalesWebMvc.Data.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        { 
            
        }
    }
}
