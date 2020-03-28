using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        //public List<Department> FindAll()
        //{     //CHAMADA SINCRONA
        //    return _context.Department.OrderBy(n => n.Name).ToList();   
        //}
    }
}
