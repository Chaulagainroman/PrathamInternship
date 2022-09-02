using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter eour eddress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your job")]

        public string Job { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", 
            ErrorMessage = "Not a valid phone number")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Please Select Your Post")]

        public string Post { get; set; }

        [Required(ErrorMessage = "Please Enter Your Dob")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Please Select Your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Select Your Image")]
        public IFormFile Image { get; set; }

    }
}
