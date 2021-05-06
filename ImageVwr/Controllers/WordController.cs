using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImageVwr.Models;

namespace ImageVwr.Controllers
{
    public class WordController : Controller
    {
        private readonly ImageDbContext _context;

        public WordController(ImageDbContext context)
        {
            _context = context;
        }

        // GET: Word
        public async Task<IActionResult> Index()
        {
            return View(await _context.Words.ToListAsync());
        }

        // GET: Word/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordModel = await _context.Words
                .FirstOrDefaultAsync(m => m.WordId == id);
            if (wordModel == null)
            {
                return NotFound();
            }

            return View(wordModel);
        }

        // GET: Word/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Word/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordId,Title,WordName")] WordModel wordModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wordModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wordModel);
        }

        // GET: Word/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordModel = await _context.Words.FindAsync(id);
            if (wordModel == null)
            {
                return NotFound();
            }
            return View(wordModel);
        }

        // POST: Word/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("WordId,Title,WordName")] WordModel wordModel)
        {
            if (id != wordModel.WordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wordModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordModelExists(wordModel.WordId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wordModel);
        }

        // GET: Word/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordModel = await _context.Words
                .FirstOrDefaultAsync(m => m.WordId == id);
            if (wordModel == null)
            {
                return NotFound();
            }

            return View(wordModel);
        }

        // POST: Word/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var wordModel = await _context.Words.FindAsync(id);
            _context.Words.Remove(wordModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordModelExists(long id)
        {
            return _context.Words.Any(e => e.WordId == id);
        }

        public IActionResult ShowTop10()
        {
            var words = _context.Words;

            var groups = words.GroupBy(i => i.WordName)
                   .Select(grp => new {
                       word = grp.Key,
                       total = grp.Count(),
                   })
                   .ToArray();

            var listOrder = new List<WordModel>();

            var list = groups.OrderByDescending(o => o.total).Take(10);
            foreach (var item in list)
            {
                listOrder.Add(new WordModel 
                {
                    WordName = item.word,
                    Occurrences = item.total
                });
            }

            return View(listOrder);
        }

    }
}
