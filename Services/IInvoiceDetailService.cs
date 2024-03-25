using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public interface IInvoiceDetailService
    {
        public IEnumerable<InvoiceDetail> AllInvoices();
        public InvoiceDetail GetByID(int id);
        public IEnumerable<InvoiceDetail> GetByHeaderId(int headerId);
        public void Create(InvoiceDetail invoiceDetail);
        public void Update(int id,InvoiceDetail invoiceDetail);
        public void Delete(int id);

    }
}
