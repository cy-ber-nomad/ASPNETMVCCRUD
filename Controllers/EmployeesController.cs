using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Data; 
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;

        }

        [HttpGet]
        public async  Task<IActionResult> Index()
        {
           var employees = await mvcDemoDbContext.Employees.ToListAsync();  
            return View (employees); 
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add (AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult View (Guid id)
        {
            var employee = mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
                                               
            return View(employee);
        }

    }
}
