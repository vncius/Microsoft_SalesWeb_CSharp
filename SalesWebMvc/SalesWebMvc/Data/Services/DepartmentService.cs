using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data.Services.Exceptions;
using System;

namespace SalesWebMvc.Data.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            // CHAMADA ASSINCRONA
            return await _context.Department.OrderBy(n => n.Name).ToListAsync();
        }

        public bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }

        public async Task<Department> FindById(int? id)
        {
            try
            {
                return await _context.Department.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception)
            {
                throw new NotFoundException(string.Format("Departamento com id ({0}) não encontrado!", id));
            }          
        }

        public async Task AddDepartment(Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartment(Department department)
        {
            try {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }            
        }

        public async Task Delete(int id) 
        {
            try
            {
                var department = await _context.Department.FindAsync(id);
                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
            }
            catch (IntegrityException)
            {
                throw new IntegrityException("Não é possivel deletar um departamento pois o mesmo possui vendedores associados!"); 
                //MENSAGEM DA EXCESSÃO PODE SER PERSONALIZADA
                //throw new IntegrityException(e.Message);
            }
        }
    }
           
}
