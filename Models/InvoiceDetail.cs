using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_for_Back_End.Models
{
    public class InvoiceDetail
    {
        [Required]
        public int ID {  get; set; }
        public virtual InvoiceHeader? InvoiceHeader {  get; set; }

        [ForeignKey("InvoiceHeader")]
        [Required]
        public int InvoiceHeaderID {  get; set; }
        [Required]
        public string ItemName {  get; set; }
        [Required]
        public float ItemCount {  get; set; }
        [Required]
        public float ItemPrice {  get; set; }
    }
}
