using System;

namespace SalesWebMvc.Data.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        { 
            // SERVE PARA TRATAR EXCEÇÕES ESPECIFICAS DA CAMADA DE SERVIÇOS                                 
        }
    }
}
