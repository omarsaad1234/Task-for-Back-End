using System.ComponentModel.DataAnnotations;

namespace Task_for_Back_End.Models
{
    public class City
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string CityName { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
