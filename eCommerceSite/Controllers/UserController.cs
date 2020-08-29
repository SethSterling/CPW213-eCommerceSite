using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // Map data to user account instance
                UserAccount account = new UserAccount()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    Username = reg.Username
                };
                //Add to database
                _context.UserAccounts.Add(account);
                await _context.SaveChangesAsync();
                //Redirect to home page
                return RedirectToAction("Index", "Home");
            }

            return View(reg);
        }

        public IActionResult Login()
        {
            //Check if user is logged in
            if(HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //UserAccount account =
            //    await (from u in _context.UserAccounts
            //     where (u.Username == model.UsernameOrEmail ||
            //         u.Email == model.UsernameOrEmail) &&
            //         u.Password == model.Password
            //     select u).SingleOrDefaultAsync();

            UserAccount account = await _context.UserAccounts
                    .Where(userAcc => (userAcc.Username == model.UsernameOrEmail ||
                                      userAcc.Email == model.UsernameOrEmail) &&
                                      userAcc.Password == model.Password)
                    .SingleOrDefaultAsync();

            if(account == null)
            {
                //Custom Error message
                ModelState.AddModelError(string.Empty, "Credentials were not found");
                return View();
            }

            //Log user in
            HttpContext.Session.SetInt32("UserId", account.UserId);


            return RedirectToAction("Index", "Home");
        }
    }
}
