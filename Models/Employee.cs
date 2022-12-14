using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    

    [Table("Employee")]
    public class Employee
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

        public string Image { get; set; }



    }
}
