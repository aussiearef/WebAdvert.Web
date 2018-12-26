using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Models.Accounts;

namespace WebAdvert.Web.Controllers
{
    public class Accounts : Controller
    {
        private readonly CognitoUserPool _pool;

        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;

        public Accounts(SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager,
            CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;
        }

        public IActionResult Signup()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _pool.GetUser(model.Email);
                if (user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "User with this email already exists");
                    return View(model);
                }

                user.Attributes.Add(CognitoAttributesConstants.Name, model.Email);
                var createdUser = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);

                if (createdUser.Succeeded) RedirectToAction("Confirm");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Confirm(ConfirmModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Confirm")]
        public async Task<IActionResult> ConfirmPost(ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "A user with the given email address was not found");
                    return View(model);
                }

                var result = await ((CognitoUserManager<CognitoUser>) _userManager)
                    .ConfirmSignUpAsync(user, model.Code, true).ConfigureAwait(false);
                if (result.Succeeded) return RedirectToAction("Index", "Home");

                foreach (var item in result.Errors) ModelState.AddModelError(item.Code, item.Description);

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(LoginModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email,
                    model.Password, model.RememberMe, false).ConfigureAwait(false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("LoginError", "Email and password do not match");
            }

            return View("Login", model);
        }

        public async Task<IActionResult> Signout()
        {
            if (User.Identity.IsAuthenticated) await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction("Login");
        }
    }
}