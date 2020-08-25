using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Waka.Brokers;
using Waka.Managers;
using Waka.Models;
using Waka.ViewModels;

namespace Waka.Controllers
{
    public class UserController : Controller
    {
        private readonly IStorageBroker<User> storage;
        private readonly UserManager userManager;

        public UserController(IStorageBroker<User> storage, UserManager userManager)
        {
            this.storage = storage;
            this.userManager = userManager;
        }

        // GET: PublicPlaces

        public ActionResult<IEnumerable<User>> Index()
        {
            return View(storage.GetAll());
        }

        // GET: PublicPlaces/Details/5


        // GET: PublicPlaces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublicPlaces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId,UserName,Email,Password,IsSignedIn")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid();
                user.IsSignedIn = true;
                storage.Post(user);
                PersistUser.userId = user.UserId;
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn([Bind("Email,Password")] SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
               var yes = userManager.SignIn(model);
                if (yes)
                {
                    var signUser = storage.GetAll().Where(a => a.Email == model.Email).FirstOrDefault();
                    PersistUser.userId = signUser.UserId;
                    PersistUser.userName = signUser.UserName;
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignOut()
        {
            var currentUserId = PersistUser.userId;
            if (currentUserId != default)
            {
                var yes = userManager.UserIsSignedIn(currentUserId);
                if (yes)
                {
                    userManager.Logout(currentUserId);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: PublicPlaces/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var userToUpdate = storage.GetAll();
            var oneUser = userToUpdate.Where(s => s.UserId == id).FirstOrDefault();

            if (oneUser == null)
            {
                return NotFound();
            }
            return View(oneUser);
        }

        // POST: PublicPlaces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind("UserId,UserName,Email,Password,IsSignedIn")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            try
            {

                var users = storage.GetAll();
                var compareUser = storage.GetAll().Where(c => c.UserId == id).FirstOrDefault();
                var userToUpdate = users.Where(d => d.UserId == id).FirstOrDefault();

                userToUpdate.UserName = string.IsNullOrWhiteSpace(user.UserName) ? userToUpdate.UserName : user.UserName;
                

                storage.Update(userToUpdate, compareUser);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return RedirectToAction(nameof(Index));
        }

        
    }
}