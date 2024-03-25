using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_for_Back_End.Models;
using Task_for_Back_End.Services;

namespace Task_for_Back_End.Controllers
{
    public class CashierController : Controller
    {
        private readonly ICashierService _cashierService;
        private readonly IBranchService _branchService;
        public CashierController(ICashierService cashierService, IBranchService branchService)
        {
            _cashierService = cashierService;
            _branchService = branchService;
            
        }
        // GET: CashierController
        public ActionResult Index()
        {
            return View(_cashierService.AllCashiers());
        }

        // GET: CashierController/Details/5
        public ActionResult Details(int id)
        {
            return View(_cashierService.GetByID(id));
        }

        // GET: CashierController/Create
        public ActionResult Create()
        {
            SelectList list = new SelectList(_branchService.AllBranches(),"ID", "BranchName");
            ViewBag.Branches = list;
            return View();
        }

        // POST: CashierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Cashier cashier = new Cashier
                {
                    CashierName = collection["CashierName"]!,
                    BranchID = Convert.ToInt32(collection["BranchID"])
                };

                _cashierService.Create(cashier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CashierController/Edit/5
        public ActionResult Edit(int id)
        {
            
            var cashier = _cashierService.GetByID(id);
            SelectList list = new SelectList(_branchService.AllBranches(), "ID", "BranchName",_cashierService.GetByID(id).BranchID);
            ViewBag.Branches = list;
            return View(cashier);
        }

        // POST: CashierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var cashier = _cashierService.GetByID(id);
                if (cashier != null)
                {
                    cashier.CashierName = collection["CashierName"]!;
                    cashier.BranchID = Convert.ToInt32(collection["BranchID"]);
                    _cashierService.Update(id, cashier);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CashierController/Delete/5
        public ActionResult Delete(int id)
        {
            var cashier = _cashierService.GetByID(id);
            return View(cashier);
        }

        // POST: CashierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _cashierService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
