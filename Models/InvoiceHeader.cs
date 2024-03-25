using System.ComponentModel.DataAnnotations.Schema;

namespace Task_for_Back_End.Models
{
    public class InvoiceHeader
    {
        public int ID {  get; set; }
        public string CustomerName {  get; set; }
        public DateTime InvoiceDate {  get; set; }
        public virtual Cashier? Cashier {  get; set; }
        public virtual Branch? Branch {  get; set; }

        [ForeignKey("Cashier")]
        public int CashierID {  get; set; }

        [ForeignKey("Branch")]
        public int BranchID {  get; set; }
        public List<InvoiceDetail> InvoiceDetails {  get; set; }
    }
}
