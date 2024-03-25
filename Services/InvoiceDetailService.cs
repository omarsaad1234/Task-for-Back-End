using Microsoft.EntityFrameworkCore;
using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly AppDbContext _context;
        public InvoiceDetailService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<InvoiceDetail> AllInvoices()
        {
            return _context.InvoiceDetails.Include(i => i.InvoiceHeader)
                .ThenInclude(i => i.Cashier).ThenInclude(i => i.Branch).ToList()
                .DistinctBy(i=>i.InvoiceHeaderID).OrderBy(i=>i.InvoiceHeaderID);
            
        }
        public InvoiceDetail GetByID(int id)
        {
            return _context.InvoiceDetails.Find(id)!;
        }
        public IEnumerable<InvoiceDetail> GetByHeaderId(int headerId)
        {
            var invoice = _context.InvoiceDetails.Include(i => i.InvoiceHeader)
                .ThenInclude(i => i.Cashier).ThenInclude(i => i.Branch).Where(i=>i.InvoiceHeaderID== headerId).ToList();
            return invoice;
        }

        public void Create(InvoiceDetail invoiceDetail)
        {
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Add(invoiceDetail);
                _context.SaveChanges();
            }
        }

        public void Update(int id, InvoiceDetail invoiceDetail)
        {
            var invoice = _context.InvoiceDetails.FirstOrDefault(i => i.ID == id);
            if (invoice != null)
            {
                invoice.ItemName=invoiceDetail.ItemName;
                invoice.ItemPrice = invoiceDetail.ItemPrice;
                invoice.ItemCount = invoiceDetail.ItemCount;
                _context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var invoice = _context.InvoiceDetails.FirstOrDefault(i => i.ID == id);
            if (invoice != null)
            {
                _context.InvoiceDetails.Remove(invoice);
                _context.SaveChanges();
            }
            
        }

        
    }
}
