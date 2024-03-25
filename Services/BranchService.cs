using Microsoft.EntityFrameworkCore;
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
            return _context.Branches.OrderBy(b=>b.BranchName).Include(b=>b.City).ToList();
        }

        public Branch GetById(int id)
        {
            return _context.Branches.FirstOrDefault(b => b.ID == id)!;
        }
    }
}
