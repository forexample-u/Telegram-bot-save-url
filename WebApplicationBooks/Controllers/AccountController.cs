using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Models;

namespace WebApplicationBooks.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AccountUser> userManager;
        private readonly SignInManager<AccountUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<AccountUser> userManager, 
            SignInManager<AccountUser> signInManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            logger.LogInformation("Пользователь хочет зарегестрироватся");
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            logger.LogInformation("Пользователь хочет войти");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                AccountUser user = new AccountUser { UserName = model.Username, PasswordHash = model.Password, MessagerUsername = model.MessagerUsername };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    logger.LogInformation($"Пользователь с ником {user.UserName} зарегестрировался!");
                    return RedirectToAction("Index", "Home");
                }
                logger.LogInformation("Пользователь не смог зарегестрироватся!");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                AccountUser user = await userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    bool userIsSign = await userManager.CheckPasswordAsync(user, model.Password);
                    if (userIsSign)
                    {
                        await signInManager.SignInAsync(user, false);
                        logger.LogInformation($"Пользователь с ником {user.UserName} вошёл в портал!");
                        return RedirectToAction("Index", "Home");
                    }
                }
                logger.LogInformation("Пользователь не вошёл в портал!");
                ModelState.AddModelError(nameof(LoginUser.Username), "Неверный логин или пароль");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            logger.LogInformation("Пользователь вышел из портала");
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}