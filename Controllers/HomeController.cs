using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext Context { get; }
        public readonly IWebHostEnvironment WebHostEnvironment;
        private readonly INotyfService _notyf;

        public HomeController(ApplicationDbContext _context, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
/*        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
*/

        public IActionResult Dashboard()
        {
            return View();

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ErrorPage()
        {
            return View();
        }
        
        public IActionResult Login(User u)
        {
            var data = Context.Users.ToList();
            string HashPassword = BCrypt.Net.BCrypt.HashPassword(u.Password);

            foreach (var item in data)
            {
                if (u.Email == item.Email) 
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(u.Password, item.Password);
                    if (verified == true)
                    {
                    _notyf.Success("Login in Successfully");
                       return RedirectToAction("Index");
                    }
                   
                    else
                    {
                        RedirectToAction("ErrorPage");
                    }
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
            return RedirectToAction("ErrorPage");
        }
        public IActionResult About()
        {
            return View();
        }
        

        /* public IActionResult Data(string email, string password)
         {*/
        /* ViewData["Message"] = "Contact Page";
         var ViewModel = new Employee()
         {
             Name = "Rohit raut",
             Street = "Jenny ko Ghar",
             City = "Redmond",
             State = "WA",
             PostalCode = "98052-6399"*/


        /*
                    };

                    *//*return View(ViewModel);*/


        /*}*/
        [HttpPost]
        public IActionResult Form(Employee e)
        {
            var ViewModel = new Employee()
            {
                Name  = e.Name,
                Email = e.Email,
               Address = e.Address,
               Job = e.Job,
        };
            
            return View(ViewModel);

        }
              

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}