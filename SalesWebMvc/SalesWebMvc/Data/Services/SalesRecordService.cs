using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Data.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            // O RESULT É UM OBJETO DO TIPO IQUERYABLE QUE SERVE PARA CONSTRUIR CONSULTAS ENCIMA DELE
            //var result = from obj in _context.salesrecord select obj;
            //if (min.hasvalue)
            //{
            //    result = result.where(x => x.date >= min); // caso tenha data min, insere condicional na consulta sql
            //}

            //if (max.hasvalue)
            //{
            //    result = result.where(x => x.date <= max); // caso tenha data max, insere condicional na consulta sql
            //}
            return await _context.SalesRecord
                // OUTRA FORMA DE EXECUTAR CONDIÇÃO CASO POSSUA VALOR É USANDO O WHERE 
                .Where(sales => minDate.HasValue ? sales.Date >= minDate : sales.Date >= DateTime.MinValue)
                .Where(sales => maxDate.HasValue ? sales.Date <= maxDate : sales.Date <= DateTime.Now )

                // NO SQL O INCLUDE FAZ UM JOIN
                .Include(sales => sales.Seller) 
                    // FAZ UM JOIN PARA CRIAR SUB-OBJETOS
                    .ThenInclude(seller => seller.Department)

                //ORDENA A LISTA DE FORMA DESCENDENTE
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            
            return await _context.SalesRecord
                // OUTRA FORMA DE EXECUTAR CONDIÇÃO CASO POSSUA VALOR É USANDO O WHERE 
                .Where(sales => minDate.HasValue ? sales.Date >= minDate : sales.Date >= DateTime.MinValue)
                .Where(sales => maxDate.HasValue ? sales.Date <= maxDate : sales.Date <= DateTime.Now)

                // NO SQL O INCLUDE FAZ UM JOIN
                .Include(sales => sales.Seller)
                    // FAZ UM JOIN PARA CRIAR SUB-OBJETOS
                    .ThenInclude(seller => seller.Department) 

                //ORDENA A LISTA DE FORMA DESCENDENTE
                .OrderByDescending(sales => sales.Date)
                // GROUP BY RETORNA LISTA DO TIPO IGROUPING, É NECESSÁRIO ALTERAR O RETORNO
                .GroupBy(x => x.Seller.Department) 
                .ToListAsync();
        }
    }
}
