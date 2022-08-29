using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        /* private const object HttpVerbs;*/

        private ApplicationDbContext Context { get; }
        public object DbContext { get; private set; }
        public readonly IWebHostEnvironment WebHostEnvironment;
        public EmployeeController(ApplicationDbContext _context, IWebHostEnvironment webHostEnvironment)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
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
        public IActionResult AddEmployee(EmployeeVM e)
        {
            string StringFile = upload(e);

            var ViewModel = new Employee()
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Address = e.Address,
                Job = e.Job,
                Phone = e.Phone,
                Post = e.Post,
                Dob = e.Dob,
                Gender = e.Gender,
                Image = StringFile
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
        public IActionResult UpdateEmployee(EmployeeVM e)
        {
            var employee = Context.Employees.Find(e.Id);
            employee.Name = e.Name;
            employee.Email = e.Email;
            employee.Address = e.Address;
            employee.Job = e.Job;
            employee.Phone = e.Phone;
            employee.Post = e.Post;
            employee.Dob = e.Dob;
            employee.Gender = e.Gender;
            
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

        public string upload(EmployeeVM e)
        {
            string fileName = "";
            if (e.Image != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "_" + e.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    e.Image.CopyTo(fileStream);
                }


               }
            return fileName;
        }
    }
 }

