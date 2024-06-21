using AutoMapper;
using HRMS.EntityDto;
using HRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.AddDepartmentAsync(departmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentDto = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (departmentDto == null)
            {
                return NotFound();
            }
            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentDto departmentDto)
        {
            if (id != departmentDto.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _departmentService.UpdateDepartmentAsync(departmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentDto = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (departmentDto == null)
            {
                return NotFound();
            }

            return View(departmentDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
