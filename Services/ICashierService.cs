using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public interface ICashierService
    {
        public IEnumerable<Cashier> AllCashiers();
        public IEnumerable<Cashier> GetByBranchId(int id);
        public Cashier GetByID(int id);
        public void Create(Cashier cashier);
        public void Update(int id, Cashier cashier);
        public void Delete(int id);
    }
}
