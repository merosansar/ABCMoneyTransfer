using ABCMoneyTransfer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    public class ExchangeRateController(ExchangeRateService exchangeRateService) : Controller
    {
        // GET: ExchangeRate
        private readonly ExchangeRateService _exchangeRateService = exchangeRateService;

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetRates()
        {
            string currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

            // Assign to variables
            string from = currentDate;
            string to = currentDate;

            try
            {
               
                var rates = await _exchangeRateService.GetExchangeRatesAsync(from, to);
                return View("Index", rates);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
        // GET: ExchangeRate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExchangeRate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExchangeRate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeRate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExchangeRate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeRate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExchangeRate/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
