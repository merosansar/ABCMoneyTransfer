using ABCMoneyTransfer.Model;
using ABCMoneyTransfer.Models;
using ABCMoneyTransfer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    public class TransactionController(TransactionService transactionService) : Controller
    {
        private readonly TransactionService _transactionService = transactionService;
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new SendTransaction();
            var transactions = await _transactionService.GetTransactionsAsync("s" ,model.SenderName ?? "",
                    model.ReceiverName??"",
                    model.SenderAddress ?? "",
                    model.ReceiverAddress ?? "",
                    model.BankName ?? "",
                    model.AccountNumber ?? "",
                    model.TransferAmount ?? 0,
                    model.PayoutAmount ?? 0,
                    model.Currency ?? "",
                    model.SenderCountry ?? "",
                    model.ReceiverCountry ?? "",
                    model.Status ?? "",
                    model.SIdentity ?? "",
                    model.RIdentity ?? "",
                    model.SMobile ?? "",
                    model.RMobile ?? "" ,
                    model.ExchangeRate ?? 0,
                    model.ServiceCharge ?? 0);
            return View(transactions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var m = new SendTransaction();
            return View(m);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SendTransaction model)
        {
            model.SenderName  = $"{model.SFirstName} {model.SMidName} {model.SLastName}".Trim();
            model.ReceiverName =    $"{model.RFirstName} {model.RMidName} {model. RLastName}".Trim();
            //model.PayoutAmount = model.Amount * 

            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors
            }

            try
            {
                // Call the AddTransactionAsync method in TransactionService
                var (returnCode, returnStatus) = await _transactionService.AddTransactionAsync(
                    model.SenderName,
                    model.ReceiverName,
                    model.SenderAddress,
                    model.ReceiverAddress,
                    model.BankName,
                    model.AccountNumber,
                    model.TransferAmount ?? 0,
                    model.PayoutAmount ?? 0,
                    model.Currency,
                    model.SenderCountry,
                    model.ReceiverCountry,
                    model.Status,
                    model.SIdentity,
                    model.RIdentity,
                    model.SMobile,
                    model.RMobile,
                    model.ExchangeRate ?? 0,
                    model.ServiceCharge ?? 0
                );

                // Check the result
                if (returnCode == 0) // Assuming 0 means success
                {
                    TempData["SuccessMessage"] = "Transaction added successfully!";
                    return RedirectToAction("Index"); // Redirect to Index or another page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, returnStatus);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(model); // Return the view with the model and errors

        }
    }
}
