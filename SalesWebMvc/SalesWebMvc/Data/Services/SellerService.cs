using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data.Services.Exceptions;

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

        public void Update(Seller obj)
        {
            // ANY VERIFICA SE EXISTE UM ELEMENTO NO BANCO USANDO LINQ ONDE X É O ELEMENTO NO BD
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                // CAPTURA POSSIVEL EXCESSÃO NA CAMADA DE BANCO DE DADOS E LANÇA NOVAMENTE COMO EXCEPTION DA CAMADA DE SERVIÇO
                // MANTENDO ASSIM A ESTRUTURA MVC, FAZENDO COM QUE A CAMADA DE SERVIÇO SEMPRE RESPONDA AO CONTROLADOR
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // CAPTURA POSSIVEL EXCESSÃO NA CAMADA DE BANCO DE DADOS E LANÇA NOVAMENTE COMO EXCEPTION DA CAMADA DE SERVIÇO
                // MANTENDO ASSIM A ESTRUTURA MVC, FAZENDO COM QUE A CAMADA DE SERVIÇO SEMPRE RESPONDA AO CONTROLADOR
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
