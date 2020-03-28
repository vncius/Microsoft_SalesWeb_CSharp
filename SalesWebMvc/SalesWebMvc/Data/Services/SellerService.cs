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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {   // ASSSINCRONA
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //public void Insert(Seller obj)
        //{  // SINCRONA
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}

        public async Task<Seller> FindByIdAsync(int id)
        {
            // EXEMPLO DE COMO TRAZER SUB - OBJETOS REALIZAR O JOIN - USANDO EAGER LOADING -ORM

            return await _context.Seller // PROCURA NA TABELA SELLER

                .Include(department => department.Department) // INCLUI O OBJETO DEPARTAMENTO DESSE SELLER
                    // .ThenInclude(department => department.Sellers) // ThenInclude BUSCA OS DADOS DO SUBOBJETO

                .FirstOrDefaultAsync(obj => obj.Id == id); // PEGA O PRIMEIRO OU VAZIO QUE OBEDECE A CONDIÇÃO LINQ


            // OBS: PARA USAR O INCLUDE ELE FAZ PARTE DO "using Microsoft.EntityFrameworkCore;"
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possivel deletar um(a) vendedor(a) pois o(a) mesmo(a) possui vendas associadas!"); //MENSAGEM DA EXCESSÃO PODE SER PERSONALIZADA
                //throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            // ANY VERIFICA SE EXISTE UM ELEMENTO NO BANCO USANDO LINQ ONDE X É O ELEMENTO NO BD
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                // CAPTURA POSSIVEL EXCESSÃO NA CAMADA DE BANCO DE DADOS E LANÇA NOVAMENTE COMO EXCEPTION DA CAMADA DE SERVIÇO
                // MANTENDO ASSIM A ESTRUTURA MVC, FAZENDO COM QUE A CAMADA DE SERVIÇO SEMPRE RESPONDA AO CONTROLADOR
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
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
