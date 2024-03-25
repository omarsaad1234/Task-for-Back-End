using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_for_Back_End.Models
{
    public class Cashier
    {
        [Required]
        public int ID {  get; set; }
        [Required]
        public string CashierName { get; set; }
        public virtual Branch? Branch {  get; set; }
        [ForeignKey("Branch")]
        public int BranchID { get; set; }
        public List<InvoiceHeader> InvoiceHeaders {  get; set; }
    }
}
