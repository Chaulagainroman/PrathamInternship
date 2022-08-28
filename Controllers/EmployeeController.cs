using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        /* private const object HttpVerbs;*/

        private ApplicationDbContext Context { get; }
        public object DbContext { get; private set; }

        public EmployeeController(ApplicationDbContext _context)
        {
            this.Context = _context;
        }
        [HttpGet]
        public IActionResult Index()
        {

         
         return View(Context.Employees.ToList());
        }
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee e)
        {
            var ViewModel = new Employee()
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Address = e.Address,
                Job = e.Job,
            };
            this.Context.Employees.Add(ViewModel);
            this.Context.SaveChanges();
            /*return View(Context.Employees.ToList());*/
            return RedirectToAction("Index");
        }

       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = Context.Employees.Find(id);
    
            return View(data);

        }
        [HttpGet]
        public IActionResult UpdateEmployee(Employee e)
        {
            var employee = Context.Employees.Find(e.Id);
            employee.Name = e.Name;
            employee.Email = e.Email;
            employee.Address = e.Address;
            employee.Job = e.Job;
            Context.SaveChanges();
           return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = Context.Employees.Find(id);
            Context.Employees.Remove(data);
            Context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
 }

