using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public class BranchService : IBranchService
    {
        private readonly AppDbContext _context;
        public BranchService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Branch> AllBranches()
        {
            return _context.Branches;
        }

        public Branch GetByHeaderId(int id)
        {
            return _context.Branches.FirstOrDefault(b => b.ID == id)!;
        }
    }
}
