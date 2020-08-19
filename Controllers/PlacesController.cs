using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Waka.Brokers;
using Waka.Models;

namespace Waka.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IStorageBroker<PublicPlaces> storage;

        public PlacesController(IStorageBroker<PublicPlaces> storage)
        {
            this.storage = storage;
        }

        // GET: PublicPlaces
       
        public ActionResult<IEnumerable<PublicPlaces>> Index()
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,FullAddress,Latitude,Longitude,Description")] PublicPlaces publicPlaces)
        {
            if (ModelState.IsValid)
            {
                publicPlaces.Id = Guid.NewGuid();
                storage.Post(publicPlaces);
                //_context.Add(publicPlaces);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicPlaces);
        }

        // GET: PublicPlaces/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var publicPlaces = await _context.PublicPlaces.FindAsync(id);
        //    if (publicPlaces == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(publicPlaces);
        //}

        //// POST: PublicPlaces/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,FullAddress,Latitude,Longitude,Description")] PublicPlaces publicPlaces)
        //{
        //    if (id != publicPlaces.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(publicPlaces);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PublicPlacesExists(publicPlaces.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(publicPlaces);
        //}

        // GET: PublicPlaces/Delete/5
    //    public async Task<IActionResult> Delete(Guid? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var publicPlaces = await _context.PublicPlaces
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (publicPlaces == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(publicPlaces);
    //    }

    //    // POST: PublicPlaces/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(Guid id)
    //    {
    //        var publicPlaces = await _context.PublicPlaces.FindAsync(id);
    //        _context.PublicPlaces.Remove(publicPlaces);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool PublicPlacesExists(Guid id)
    //    {
    //        return _context.PublicPlaces.Any(e => e.Id == id);
    //    }
    }
}
