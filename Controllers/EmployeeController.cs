using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebApplication1.Controllers

{
    public class EmployeeController : Controller
    {
      

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


        [Authorize]

        [HttpGet]
        public IActionResult Index()
        {
            return View(Context.Employees.ToList());
        }
        public IActionResult AddEmployee()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = Context.Employees.Find(id);

            return View(data);

        }
        [Authorize]
        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeVM e)
        {
            string stringFile = upload(e);
            if (e.Image != null)
            {
                var employee = Context.Employees.Find(e.Id);
                string delDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images", employee.Image);
                FileInfo f1 = new FileInfo(delDir);


                if (f1.Exists)
                {
                    System.IO.File.Delete(delDir);
                    f1.Delete();

                }
                employee.Name = e.Name;
                employee.Email = e.Email;
                employee.Address = e.Address;
                employee.Job = e.Job;
                employee.Phone = e.Phone;
                employee.Post = e.Post;
                employee.Dob = e.Dob;
                employee.Gender = e.Gender;
                employee.Image = stringFile;

                Context.SaveChanges();
                _notyf.Success("Updated Successfully");
                return RedirectToAction("Index");
            }
            else
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


        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = Context.Employees.Find(id);
            string delDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images", data.Image);
            FileInfo f1 = new FileInfo(delDir);


            if (f1.Exists)
            {
                System.IO.File.Delete(delDir);
                f1.Delete();

            }
            Context.Employees.Remove(data);
            Context.SaveChanges();
            _notyf.Error("Deleted Successfully");
            return RedirectToAction("Index");

        }

        [Authorize]
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
        [Authorize]
        public IActionResult Details(int id)
        {
            var employee = Context.Employees.Find(id);
            var data = new ViewEmployeeVM()
            {
                Id = employee.Id,
                Email = employee.Email,
                Name = employee.Name,
                Address = employee.Address,
                Job = employee.Job,
                Phone = employee.Phone,
                Post = employee.Post,
                Dob = employee.Dob,
                Gender = employee.Gender,
                Image = employee.Image


            };
            return View(data);

        }

    }
}
