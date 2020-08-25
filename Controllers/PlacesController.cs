using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Waka.Brokers;
using Waka.Managers;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,FullAddress,Latitude,Longitude,Description")] PublicPlaces publicPlaces)
        {
            if (ModelState.IsValid)
            {
                publicPlaces.Id = Guid.NewGuid();
                publicPlaces.PostedBy = PersistUser.userName;
                storage.Post(publicPlaces);
                return RedirectToAction(nameof(Index));
            }
            return View(publicPlaces);
        }

        // GET: PublicPlaces/Edit/5
        public  ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var publicPlaces = storage.GetAll();
            var onePlace = publicPlaces.Where(s => s.Id == id).FirstOrDefault();

            if (onePlace == null)
            {
                return NotFound();
            }
            return View(onePlace);
        }

        // POST: PublicPlaces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind("Id,Name,FullAddress,Latitude,Longitude,Description")] PublicPlaces publicPlaces)
        {
            if (id != publicPlaces.Id)
            {
                return NotFound();
            }

            try
            {

                var places = storage.GetAll();
                var comparePlace = storage.GetAll().Where(c => c.Id == id).FirstOrDefault();
                var placeToUpdate = places.Where(d => d.Id == id).FirstOrDefault();

                placeToUpdate.FullAddress = string.IsNullOrWhiteSpace(publicPlaces.FullAddress) ? placeToUpdate.FullAddress : publicPlaces.FullAddress;
                placeToUpdate.Name = string.IsNullOrWhiteSpace(publicPlaces.Name) ? placeToUpdate.Name : publicPlaces.Name;
                placeToUpdate.Description = string.IsNullOrWhiteSpace(publicPlaces.Description) ? placeToUpdate.Description : publicPlaces.Description;
                placeToUpdate.PostedBy = PersistUser.userName;
               

                storage.Update(placeToUpdate, comparePlace);
            }
            catch (Exception ex)
            {
                throw ex;
               
            }

            return RedirectToAction(nameof(Index));
        }

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
