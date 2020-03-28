using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Data.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            // EXEMPLO DE COMO TRAZER SUB - OBJETOS REALIZAR O JOIN - USANDO EAGER LOADING -ORM

            return _context.Seller // PROCURA NA TABELA SELLER

                .Include(department => department.Department) // INCLUI O OBJETO DEPARTAMENTO DESSE SELLER
                    // .ThenInclude(department => department.Sellers) // ThenInclude BUSCA OS DADOS DO SUBOBJETO

                .FirstOrDefault(obj => obj.Id == id); // PEGA O PRIMEIRO OU VAZIO QUE OBEDECE A CONDIÇÃO LINQ


            // OBS: PARA USAR O INCLUDE ELE FAZ PARTE DO "using Microsoft.EntityFrameworkCore;"
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
