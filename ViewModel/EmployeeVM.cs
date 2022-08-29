namespace WebApplication1.ViewModel
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Job { get; set; }

        public long Phone { get; set; }

        public string Post { get; set; }
        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public IFormFile Image { get; set; }

    }
}
