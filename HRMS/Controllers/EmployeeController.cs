using AutoMapper;
using HRMS.EntityDto;
using HRMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(employeeDto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "Name", employeeDto.DepartmentId);
            return View(employeeDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employeeDto == null)
            {
                return NotFound();
            }
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "Name", employeeDto.DepartmentId);
            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(employeeDto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "Name", employeeDto.DepartmentId);
            return View(employeeDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employeeDto == null)
            {
                return NotFound();
            }

            return View(employeeDto);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int EmployeeId)
        {
            if (EmployeeId == 0)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(EmployeeId);
            return RedirectToAction(nameof(Index));
        }

    }
}
