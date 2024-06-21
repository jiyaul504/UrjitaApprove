using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HRMS.EntityDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml;

namespace HRMS.Controllers
{
    public class LeaveApplicationController : Controller
    {
        private readonly ILeaveApplicationService _leaveApplicationService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public LeaveApplicationController(ILeaveApplicationService leaveApplicationService, IEmployeeService employeeService, IMapper mapper)
        {
            _leaveApplicationService = leaveApplicationService;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var leaveApplications = await _leaveApplicationService.GetAllLeaveApplicationsAsync();
            return View(leaveApplications);

        }

        public async Task<IActionResult> Create()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var departments = await _employeeService.GetAllDepartmentsAsync();

            ViewBag.Employees = _mapper.Map<List<EmployeeDto>>(employees);
            ViewBag.Departments = _mapper.Map<List<DepartmentDto>>(departments); // Ensure you have a DepartmentDto

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplicationDto leaveApplication)
        {
            if (ModelState.IsValid)
            {
                var approver = await _employeeService.GetManagerByDepartmentIdAsync(leaveApplication.DepartmentId);
                if (approver != null)
                {
                    leaveApplication.ApproverId = approver.EmployeeId;
                    await _leaveApplicationService.CreateLeaveApplicationAsync(leaveApplication);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Unable to determine approver for the selected department.");
                }
            }
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                foreach (var error in state.Errors)
                {
                    Console.WriteLine($"{key}: {error.ErrorMessage}");
                }
            }

            var employees = await _employeeService.GetAllEmployeesAsync();
            var departments = await _employeeService.GetAllDepartmentsAsync();

            ViewBag.Employees = _mapper.Map<List<EmployeeDto>>(employees);
            ViewBag.Departments = _mapper.Map<List<DepartmentDto>>(departments);
            return View(leaveApplication);
        }

        private async Task<int> DetermineApproverId(int employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {employeeId} not found.");
            }

            // Example logic to determine Approver based on employee or department rules
            if (employee.Position == "Senior Engineer")
            {
                // Return Manager's EmployeeId as Approver
                var manager = await _employeeService.GetManagerByDepartmentIdAsync(employee.DepartmentId);
                return manager.EmployeeId;
            }
            else if (employee.Position == "Manager")
            {
                // Optionally, Manager can approve their own leave
                return employee.EmployeeId;
            }
            else
            {
                // Default to Manager as the ultimate Approver
                var manager = await _employeeService.GetManagerByDepartmentIdAsync(employee.DepartmentId);
                return manager.EmployeeId;
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leaveApplication = await _leaveApplicationService.GetLeaveApplicationByIdAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.Employees = _mapper.Map<List<EmployeeDto>>(employees);
            ViewBag.Approvers = _mapper.Map<List<EmployeeDto>>(employees);

            return View(leaveApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplicationDto leaveApplication)
        {
            if (ModelState.IsValid)
            {
                await _leaveApplicationService.UpdateLeaveApplicationAsync(leaveApplication);
                return RedirectToAction(nameof(Index));
            }

            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.Employees = _mapper.Map<List<EmployeeDto>>(employees);
            ViewBag.Approvers = _mapper.Map<List<EmployeeDto>>(employees);

            return View(leaveApplication);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leaveApplication = await _leaveApplicationService.GetLeaveApplicationByIdAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            return View(leaveApplication);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int LeaveApplicationId)
        {
            await _leaveApplicationService.DeleteLeaveApplicationAsync(LeaveApplicationId);
            return RedirectToAction(nameof(Index));
        }




        //public async Task<IActionResult> Approve(int id)
        //{
        //    var application = await _leaveApplicationService.GetLeaveApplicationByIdAsync(id);
        //    if (application == null)
        //    {
        //        return NotFound();
        //    }

        //    application.Status = "Approved";
        //    await _leaveApplicationService.UpdateLeaveApplicationAsync(application);

        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Reject(int id)
        //{
        //    var application = await _leaveApplicationService.GetLeaveApplicationByIdAsync(id);
        //    if (application == null)
        //    {
        //        return NotFound();
        //    }

        //    application.Status = "Rejected";
        //    await _leaveApplicationService.UpdateLeaveApplicationAsync(application);

        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Approve(int id)
        {
            var leaveApplication = await _leaveApplicationService.GetLeaveApplicationByIdAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            return View(leaveApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(LeaveApplicationDto leaveApplication)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    leaveApplication.Status = "Approved";
                    await _leaveApplicationService.UpdateLeaveApplicationAsync(leaveApplication);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while approving the leave: {ex.Message}");
                }
            }
            return View(leaveApplication);
        }

        public async Task<IActionResult> Reject(int id)
        {
            var leaveApplication = await _leaveApplicationService.GetLeaveApplicationByIdAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            return View(leaveApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(LeaveApplicationDto leaveApplication)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    leaveApplication.Status = "Rejected";
                    await _leaveApplicationService.UpdateLeaveApplicationAsync(leaveApplication);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while rejecting the leave: {ex.Message}");
                }
            }
            return View(leaveApplication);
        }

        public async Task<IActionResult> LeaveHistory()
        {
            var leaveApplications = await _leaveApplicationService.GetAllLeaveApplicationsAsync();
            return View(leaveApplications);
        }

        public async Task<IActionResult> GenerateReport()
        {
            var leaveApplications = await _leaveApplicationService.GetAllLeaveApplicationsAsync();
            return View(leaveApplications);
        }

        public async Task<IActionResult> ExportToExcel()
        {
            var leaveApplications = await _leaveApplicationService.GetAllLeaveApplicationsAsync();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Leave Applications");
                worksheet.Cells.LoadFromCollection(leaveApplications, true);
                package.Save();
            }

            stream.Position = 0;
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "LeaveApplications.xlsx";

            return File(stream, contentType, fileName);
        }

        public async Task<IActionResult> ExportToPdf()
        {
            var leaveApplications = await _leaveApplicationService.GetAllLeaveApplicationsAsync();
            var stream = new MemoryStream();

            var document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            var writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            var table = new PdfPTable(5) { WidthPercentage = 100 };
            table.AddCell("Employee Name");
            table.AddCell("Approver Name");
            table.AddCell("Start Date");
            table.AddCell("End Date");
            table.AddCell("Status");

            foreach (var leaveApplication in leaveApplications)
            {
                table.AddCell(leaveApplication.EmployeeName);
                table.AddCell(leaveApplication.Approver != null ? $"{leaveApplication.Approver.FirstName} {leaveApplication.Approver.LastName}" : "-");
                table.AddCell(leaveApplication.StartDate.ToString("dd-MM-yyyy"));
                table.AddCell(leaveApplication.EndDate.ToString("dd-MM-yyyy"));
                table.AddCell(leaveApplication.Status);
            }

            document.Add(table);
            document.Close();

            stream.Position = 0;
            var contentType = "application/pdf";
            var fileName = "LeaveApplications.pdf";

            return File(stream, contentType, fileName);
        }
    }
}
