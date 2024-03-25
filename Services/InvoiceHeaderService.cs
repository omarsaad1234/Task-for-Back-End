using Microsoft.EntityFrameworkCore;
using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public class InvoiceHeaderService : IInvoiceHeaderService
    {
        private readonly AppDbContext _context;
        public InvoiceHeaderService(AppDbContext context)
        {
            _context = context;
            
        }
        public IEnumerable<InvoiceHeader> AllInvoiceHeaders()
        {
            return _context.InvoiceHeader.Include(i => i.Cashier).Include(i => i.Branch).ToList();
        }

        public InvoiceHeader GetById(int id)
        {
            var header = _context.InvoiceHeader.FirstOrDefault(i=>i.ID == id);
            return header;
        }
        public void Create(InvoiceHeader invoiceHeader)
        {
            if (invoiceHeader != null)
            {
                _context.InvoiceHeader.Add(invoiceHeader);
                _context.SaveChanges();
            }
            else
                throw new InvalidOperationException();
        }

        public void Update(int id, InvoiceHeader invoiceHeader)
        {
            var header = _context.InvoiceHeader.FirstOrDefault(i => i.ID == id);
            if (header != null)
            {
                header.CustomerName = invoiceHeader.CustomerName;
                header.CashierID = invoiceHeader.CashierID;
                header.BranchID = invoiceHeader.BranchID;
                header.InvoiceDate = invoiceHeader.InvoiceDate;
                _context.SaveChanges();
            }
            else
                throw new InvalidOperationException();
        }
        public void Delete(int id)
        {
            var header = _context.InvoiceHeader.FirstOrDefault(i => i.ID == id);
            if (header != null)
            {
                _context.InvoiceHeader.Remove(header);
                _context.SaveChanges();
            }
            else
                throw new InvalidOperationException();
        }

        public int GetLastElementID()
        {
            return _context.InvoiceHeader.OrderBy(x=>x.ID).Last().ID;
        }
    }
}
