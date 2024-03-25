using Microsoft.EntityFrameworkCore;
using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public class CashierService : ICashierService
    {
        private readonly AppDbContext _context;
        public CashierService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Cashier> AllCashiers()
        {
            return _context.Cashier.OrderBy(c=>c.CashierName).Include(c=>c.Branch).ToList();
        }
        public Cashier GetByID(int id)
        {
            return _context.Cashier.Include(c=>c.Branch).ThenInclude(b=>b.City).FirstOrDefault(c => c.ID==id)!;
        }

        public IEnumerable<Cashier> GetByBranchId(int id)
        {
            return _context.Cashier.Where(c => c.BranchID == id).OrderBy(c => c.ID).ToList();
        }

        public void Create(Cashier cashier)
        {
            if (cashier != null) 
            {
                _context.Cashier.Add(cashier);
                _context.SaveChanges();
            }
            else
                throw new NullReferenceException();

        }

        public void Update(int id, Cashier cashier)
        {
            var selectedCashier = _context.Cashier.Find(id);
            if (selectedCashier != null)
            {
                selectedCashier.CashierName = cashier.CashierName;
                selectedCashier.BranchID = cashier.BranchID;
                _context.SaveChanges();
            }
            else
                throw new NullReferenceException();
        }

        public void Delete(int id)
        {
            var cashier = _context.Cashier.Find(id);

            if (cashier != null)
            {
                _context.Cashier.Remove(cashier);
                _context.SaveChanges();

            }
            else
                throw new NullReferenceException();
        }



    }
}
