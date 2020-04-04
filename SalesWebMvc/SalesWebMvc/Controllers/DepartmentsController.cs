using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Data.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : ControladorCrudAbstrato
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(SalesWebMvcContext context, DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Departments";
            ViewData["ehModelInvalid"] = false;
            return View(await _departmentService.FindAllAsync());
        }

        public async Task<IActionResult> _ViewAcoes(int? id, EnumStatusAcoes EnumAcao)
        {
            var action = ObtenhaActionController(EnumAcao);

            if (id.HasValue)
            {
                var viewModel = await _departmentService.FindById(id);

                if (viewModel.Equals(null))
                {
                    throw new ApplicationException(String.Format("Id informamado ({0}) não existe na base de dados!", id));
                }

                if (ObtenhaViewDatas(ViewData, EnumAcao, id, action, true, true).Count > 0)
                {
                    return PartialView(viewModel);
                }
            }
            else 
            {
                if (ObtenhaViewDatas(ViewData, EnumAcao, id, action, false, false).Count > 0)
                {
                    return PartialView();
                }                
            }

            throw new ApplicationException("Falha ao obter dados para abertura da tela!");
        }

        private string ObtenhaActionController(EnumStatusAcoes enumAcao)
        {
            if (enumAcao.Equals(EnumStatusAcoes.CREATE))
            {
                return nameof(Create);
            }
            else if (enumAcao.Equals(EnumStatusAcoes.EDIT))
            {
                return nameof(Edit);
            }
            else if (enumAcao.Equals(EnumStatusAcoes.DELETE))
            {
                return nameof(Delete);
            }
            else if (enumAcao.Equals(EnumStatusAcoes.DETAILS))
            {
                return "#";
            }
            else { return null; }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Create";
                ViewData["acao"] = EnumStatusAcoes.CREATE;
                ViewData["acaoForm"] = string.Format(nameof(Create));
                ViewData["ehModelInvalid"] = true;
                return View(nameof(_ViewAcoes), department);
            }

            await _departmentService.AddDepartment(department);
            return RedirectToAction(nameof(Index));

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Editar department";
                ViewData["acao"] = EnumStatusAcoes.EDIT;
                ViewData["acaoForm"] = string.Format(nameof(Edit));
                ViewData["ehModelInvalid"] = true;
                return View(nameof(Index), department);
            }

            try
            {
                await _departmentService.UpdateDepartment(department);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return RedirectToAction("Error", new { message = ex.Message }); ;
            }
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            ViewData["ehModelInvalid"] = false;
            await _departmentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
