#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business;
using Business.Models;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        // TODO: Add service injections here
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.Query().ToList(); // TODO: Add get list service logic here
            return View(userList);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(u => u.Id == id);  // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {

                bool result = _userService.Add(user);
                if (result)
                {
                    // Way 1:
                    //return RedirectToAction("GetList");
                    // Way 2:
                    TempData["Message"] = "User added";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "User could not be added");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // 404 HTTP Status Code
            }
        
            return View(user);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                bool result = _userService.Update(user);
                if (result)
                {
                    TempData["Message"] = "User information changed";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "User Information could not be changed");
                // TODO: Add update service logic here
                return RedirectToAction(nameof(Index));
            }
           
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(u => u.Id == id);// TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
