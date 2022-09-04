using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class EmployeeVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter correct email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your eddress")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter your job")]

        public string? Job { get; set; }
 
         
        public long Phone { get; set; }

        

        public string? Post { get; set; }

     
        public DateTime Dob { get; set; }


        public string? Gender { get; set; }

        public IFormFile Image { get; set; }

    }
}
