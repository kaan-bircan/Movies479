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
    public class GenresController : Controller
    {
        // TODO: Add service injections here
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: Genres
        public IActionResult Index()
        {
            List<GenreModel> genreList = _genreService.Query().ToList(); // TODO: Add get list service logic here
            return View(genreList);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(m => m.Id == id);  // TODO: Add get item service logic here
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_genreService.Query().ToList(), "Id", "Name");// TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                bool result = _genreService.Add(genre);
                if (result)
                {
                    TempData["Message"] = "Genre added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Genre couldn't be added!");
            }
            ViewData["AlbumId"] = new SelectList(_genreService.Query().ToList(), "Id", "Name");
            return View(genre);
        }

        // GET: Genres/Edit/5
        public IActionResult Edit(int id)
        {
            GenreModel genre = _genreService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (genre == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["DirectorId"] = new SelectList(_genreService.Query().ToList(), "Id", "Name");
            return View(genre);
        }

        // POST: Genres/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                bool result = _genreService.Update(genre);
                if (result)
                {
                    TempData["Message"] = "Genre updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Genre couldn't be updated!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["DirectorId"] = new SelectList(_genreService.Query().ToList(), "Id", "Name");
            return View(genre);
        }

        // GET: Genres/Delete/5
        public IActionResult Delete(int id)
        {
            _genreService.Delete(id);
            TempData["Message"] = "Genre deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
