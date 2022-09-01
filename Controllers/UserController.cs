using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers


{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext Context;
       
        public readonly IWebHostEnvironment WebHostEnvironment;
        private readonly INotyfService _notyf;

        public UserController(ApplicationDbContext _context, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }

        public IActionResult Landing()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User u)
        {
            string HashPassword = BCrypt.Net.BCrypt.HashPassword(u.Password);

            var data = new User
            {
                Name = u.Name,
                Email = u.Email,
                Password = HashPassword

            };
            Context.Users.Add(data);
            Context.SaveChanges();
            /*_notyf.Success("Registered Successfully");*/
             return RedirectToAction("Landing");
        

        }
    }
}
