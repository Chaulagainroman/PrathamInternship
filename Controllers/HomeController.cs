using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;

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

        [HttpPost]
        public IActionResult Login(User u)
        {
            var data = Context.Users.ToList();
            string HashPassword = BCrypt.Net.BCrypt.HashPassword(u.Password);

    

            try
            {
                var res = (from verified in data where verified.Email.Equals(u.Email) select new { verified.Email, verified.Password }).First();

                if (res != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(u.Password, res.Password);
                    if (verified)
                    {
                        HttpContext.Session.SetString("Email", res.Email);
                        _notyf.Success("Login in Successfully");
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        Redirect("ErrorPage");
                    }
                }
            }
            catch (Exception ex)
            {
                return Redirect("ErrorPage");
            }
            return Redirect("ErrorPage");
        }
        public IActionResult About()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Form(Employee e)
        {
            var data = new Employee()
            {
                Name = e.Name,
                Email = e.Email,
                Address = e.Address,
                Job = e.Job,
            };

            return View(data);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}