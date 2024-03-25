using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using Task_for_Back_End.Models;
using Task_for_Back_End.Services;

namespace Task_for_Back_End.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IInvoiceDetailService _invoiceDetailService;
        private readonly IInvoiceHeaderService _invoiceHeaderService;
        private readonly ICashierService _cashierService;
        private readonly IBranchService _branchService;
        public InvoiceController(AppDbContext context, IInvoiceDetailService invoiceDetailService, IInvoiceHeaderService invoiceHeaderService, ICashierService cashierService, IBranchService branchService) {
        
            _context=context;
            _invoiceDetailService = invoiceDetailService;
            _invoiceHeaderService=invoiceHeaderService;
            _cashierService = cashierService;
            _branchService = branchService;
        }
        // GET: InvoiceController
        public ActionResult Index()
        {
            return View(_invoiceDetailService.AllInvoices());
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            return View(_invoiceDetailService.GetByHeaderId(id));
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            
            SelectList ListOfBranches = new SelectList(_branchService.AllBranches(), "ID", "BranchName");
            ViewBag.Branch = ListOfBranches;
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                InvoiceHeader header = new InvoiceHeader
                {
                    CustomerName = collection["InvoiceHeader.CustomerName"]!,
                    InvoiceDate = Convert.ToDateTime(collection["InvoiceHeader.InvoiceDate"]),
                    BranchID = Convert.ToInt32(collection["InvoiceHeader.BranchID"]),
                    CashierID = Convert.ToInt32(collection["InvoiceHeader.CashierID"])
                };
                _invoiceHeaderService.Create(header);
                InvoiceDetail detail = new InvoiceDetail
                {
                    InvoiceHeaderID = _invoiceHeaderService.GetLastElementID(),
                    ItemName = collection["ItemName"]!,
                    ItemCount = float.Parse(collection["ItemCount"]!, CultureInfo.InvariantCulture.NumberFormat),
                    ItemPrice = float.Parse(collection["ItemPrice"]!, CultureInfo.InvariantCulture.NumberFormat)

                };
                _invoiceDetailService.Create(detail);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
            
        {
            SelectList list = new SelectList(_branchService.AllBranches()
                , "ID", "BranchName", _invoiceHeaderService.GetById(id).BranchID);

            ViewBag.Branch = list;

            var items = _invoiceDetailService.GetByHeaderId(id);

            ViewBag.Items = items;
            
            return View(_invoiceHeaderService.GetById(id));
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var invoiceHeader = _invoiceHeaderService.GetById(id);
                if (invoiceHeader != null)
                {
                    InvoiceHeader header = new InvoiceHeader
                    {
                        BranchID = Convert.ToInt32(collection["BranchID"]),
                        CashierID = Convert.ToInt32(collection["CashierID"]),
                        CustomerName = collection["CustomerName"]!,
                        InvoiceDate = Convert.ToDateTime(collection["InvoiceDate"])

                    };
                    _invoiceHeaderService.Update(id, header);
                }
                var invoiceItems = _invoiceDetailService.GetByHeaderId(id);
                InvoiceDetail detail = new();
                var counter = 0;
                foreach (var item in invoiceItems)
                {
                    counter++;
                    detail.ItemName = collection[$"ItemName_{counter}"]!;
                    detail.ItemCount = float.Parse(collection[$"ItemCount_{counter}"]!, CultureInfo.InvariantCulture.NumberFormat);
                    detail.ItemPrice = float.Parse(collection[$"ItemPrice_{counter}"]!, CultureInfo.InvariantCulture.NumberFormat);
                    _invoiceDetailService.Update(item.ID, detail);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            var invoice = _invoiceDetailService.GetByHeaderId(id);
            return View(invoice);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var invoice = _invoiceDetailService.GetByHeaderId(id);
                foreach(var item in invoice)
                {
                    _invoiceDetailService.Delete(item.ID);
                }
                _invoiceHeaderService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult GetCashiers(int BranchID)
        {
            var cashiers = _cashierService.GetByBranchId(BranchID);

            return Ok(cashiers);
        }

        public ActionResult AddItem(int ID)
        {
            ViewBag.headerID = ID;
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(IFormCollection collection)
        {
            try
            {
                InvoiceDetail detail = new InvoiceDetail
                {
                    InvoiceHeaderID = Convert.ToInt32(collection["ID"]),
                    ItemName = collection["ItemName"]!,
                    ItemCount = float.Parse(collection["ItemCount"]!, CultureInfo.InvariantCulture.NumberFormat),
                    ItemPrice = float.Parse(collection["ItemPrice"]!, CultureInfo.InvariantCulture.NumberFormat)

                };
                _invoiceDetailService.Create(detail);
                return RedirectToAction("Edit",new { id=detail.InvoiceHeaderID});
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteItem(int ID)
        {
            var Item = _invoiceDetailService.GetByID(ID);
            return View(Item);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id, IFormCollection collection)
        {
            try
            {
                _invoiceDetailService.Delete(id);
                return RedirectToAction("Edit",new { id = Convert.ToInt32(collection["InvoiceHeaderID"])});
            }
            catch
            {
                return View();
            }
        }
    }
}
