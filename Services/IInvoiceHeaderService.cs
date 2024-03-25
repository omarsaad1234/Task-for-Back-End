using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public interface IInvoiceHeaderService
    {
        public IEnumerable<InvoiceHeader> AllInvoiceHeaders();
        public InvoiceHeader GetById(int id);
        public void Create(InvoiceHeader invoiceHeader);
        public void Update(int id, InvoiceHeader invoiceHeader);
        public void Delete(int id);

        public int GetLastElementID();

    }
}
