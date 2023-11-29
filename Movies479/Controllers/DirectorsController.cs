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
using Business.Services;
using Business.Models;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class DirectorsController : Controller
    {
        // TODO: Add service injections here
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        // GET: Directors
        public IActionResult Index()
        {
            List<DirectorModel> directorList = _directorService.Query().ToList(); // TODO: Add get list service logic here
            return View(directorList);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(m => m.Id == id); ; // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");// TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                bool result = _directorService.Add(director);
                if (result)
                {
                    TempData["Message"] = "Director added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Director couldn't be added!");
            }
           
            return View(director);
        }

        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(s => s.Id == id); ; // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(director);
        }

        // POST: Directors/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorModel director)
        {
            bool result = _directorService.Update(director);
            if (result)
            {
                TempData["Message"] = "Director updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Director couldn't be updated!");
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(director);
        }

        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            bool result = _directorService.Delete(id);

            if (result)
                TempData["Message"] = "Director deleted successfully.";

            TempData["Message"] = "Director cannot be deleted because it has Movies.";
            return RedirectToAction(nameof(Index));
        }

	}
}
