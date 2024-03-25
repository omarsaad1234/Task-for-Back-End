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
            return _context.Cashier;
        }

        public void Create(Cashier cashier)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public void Update(int id, Cashier cashier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cashier> GetByBranchId(int id)
        {
            return _context.Cashier.Where(c => c.BranchID == id).ToList();
        }
    }
}
