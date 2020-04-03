using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Data.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(SalesWebMvcContext context, DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Departments";
            return View(await _departmentService.FindAllAsync());
        }

        public async Task<IActionResult> _ViewAcoes(int? id, EnumStatusAcoes EnumAcao)
        {
            if (!id.HasValue)
            {
                if (EnumAcao.Equals(EnumStatusAcoes.CREATE))
                {
                    ViewData["Title"] = "Create";
                    ViewData["acao"] = EnumStatusAcoes.CREATE;
                    ViewData["acaoForm"] = string.Format(nameof(Create));
                    return View();
                }
            }

            if (id.HasValue)
            {
                var viewModel = await _departmentService.FindById(id);

                if (!viewModel.Equals(null))
                {
                    if (EnumAcao.Equals(EnumStatusAcoes.EDIT))
                    {
                        ViewData["Title"] = "Editar department";
                        ViewData["acao"] = EnumStatusAcoes.EDIT;
                        ViewData["acaoForm"] = string.Format(nameof(Edit));
                    }
                    else if (EnumAcao.Equals(EnumStatusAcoes.DELETE))
                    {
                        ViewData["Title"] = "Delete department";
                        ViewData["acao"] = EnumStatusAcoes.DELETE;
                        ViewData["acaoForm"] = string.Format(nameof(Delete));
                    }
                    else if (EnumAcao.Equals(EnumStatusAcoes.DETAILS))
                    {
                        ViewData["Title"] = "Details department";
                        ViewData["acao"] = EnumStatusAcoes.DETAILS;
                        ViewData["acaoForm"] = "#";
                    }
                    else {
                        return NotFound();
                    }

                    return View(viewModel);
                }
                else
                {
                    throw new ApplicationException("Id informamado ({0}) não existe na base de dados!");
                }
            }

            return NotFound();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Create";
                ViewData["acao"] = EnumStatusAcoes.CREATE;
                ViewData["acaoForm"] = string.Format(nameof(Create));

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
                return View(nameof(_ViewAcoes), department);
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
            await _departmentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
