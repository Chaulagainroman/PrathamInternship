using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyf;

        public EmployeeController(ApplicationDbContext _context, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
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
            if (!ModelState.IsValid)
            {
                return View("AddEmployee");
            }
            else
            {
                string StringFile = upload(e);
                var data = new Employee()
                {
                   
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


                this.Context.Employees.Add(data);
                this.Context.SaveChanges();
                _notyf.Success("Inserted Successfully");
                return RedirectToAction("Index");

            }
            



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
            _notyf.Success("Updated Successfully");
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = Context.Employees.Find(id);
            Context.Employees.Remove(data);
            Context.SaveChanges();
            _notyf.Error("Deleted Successfully");
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


        public IActionResult Profile()
        {
            return View();

        }
        public IActionResult Contact()
        {
            return View();

        }

        public IActionResult Error()
        {
            return View();

        }

        public IActionResult LeaveApplication()
        {
            return View();

        }


    }
}
