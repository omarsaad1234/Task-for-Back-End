using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_for_Back_End.Models
{
    public class Branch
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string BranchName { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("City")]
        public int CityID {  get; set; }
        public List<Cashier> Cashiers {  get; set; }
        public List<InvoiceHeader> InvoiceHeaders {  get; set; }
        


    }
}
