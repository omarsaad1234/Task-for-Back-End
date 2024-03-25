using Task_for_Back_End.Models;

namespace Task_for_Back_End.Services
{
    public interface IBranchService
    {
        public IEnumerable<Branch> AllBranches();
        public Branch GetById(int id);
    }
}
