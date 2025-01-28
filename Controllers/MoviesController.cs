using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithDapper.Data;
using WebApplicationWithDapper.Interfaces;
using WebApplicationWithDapper.Models;

namespace WebApplicationWithDapper.Controllers
{
    public class MoviesController : Controller
    {
        private readonly WebApplicationWithDapperContext _context;
        private readonly IMovie _movieRepository;
        //public MoviesController(WebApplicationWithDapperContext context)
        //{
        //    _context = context;
        //}
        public MoviesController(IMovie movie)
        {
            this._movieRepository = movie;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.Get();
            return View(movies);
            //return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var movie = await _context.Movie
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var movie = await _movieRepository.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,Year")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.Add(movie);
                return RedirectToAction(nameof(Index));
                //_context.Add(movie);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _movieRepository.Find(id);

            //var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Year")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieRepository.Update(movie);

                    //_context.Update(movie);
                    //await _context.SaveChangesAsync();
                }
                catch
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _movieRepository.Find(id);
            //var movie = await _context.Movie
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieRepository.Find(id);
            //var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                await _movieRepository.Remove(movie);
                //_context.Movie.Remove(movie);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
