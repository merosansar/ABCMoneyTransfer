using ABCMoneyTransfer.Model;
using ABCMoneyTransfer.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AbcremittanceDbContext _context;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AbcremittanceDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        //public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        // GET: Register
        public IActionResult Register() => View();

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                //if (string.IsNullOrWhiteSpace(model.Email) || !model.UserName.All(char.IsLetterOrDigit)) { ModelState.AddModelError(string.Empty, "Username is invalid, can only contain letters or digits."); }


                var user = new User { UserName = model.UserName.Trim(), Email = model.Email };

                // Use UserManager to create the user, which will also hash the password
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Optionally add user to roles
                    await _userManager.AddToRoleAsync(user, "User");  // Add role as per your logic

                    return RedirectToAction("Login"); // Redirect to login after successful registration
                }

                //if (result.Succeeded)
                //{
                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    return RedirectToAction("Index", "Home");
                //}

                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }
            
            return View(model);


        }

        // GET: Login
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        // POST: Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
