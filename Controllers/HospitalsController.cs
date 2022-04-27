using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab5.Models;

namespace WebLab5.Controllers;

public class HospitalsController : Controller
{
    private readonly DataContext context;

    public HospitalsController(DataContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await context.Hospitals.ToListAsync());
    }

    public IActionResult Create()
    {
        return View(new Hospital());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Hospital model)
    {
        if (!ModelState.IsValid) return View(model);
        await context.Hospitals.AddAsync(model);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var hospital = await context.Hospitals.SingleOrDefaultAsync(m => m.Id == id);
        if (hospital is null)
        {
            return NotFound();
        }

        return View(hospital);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var hospital = await context.Hospitals.SingleOrDefaultAsync(m => m.Id == id);
        if (hospital is null)
        {
            return NotFound();
        }

        return View(hospital);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Hospital model)
    {
        var hospital = await context.Hospitals.SingleOrDefaultAsync(m => m.Id == id);
        if (hospital is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(model);
        hospital.Name = model.Name;
        hospital.Address = model.Address;
        hospital.Phone = model.Phone;

        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var hospital = await context.Hospitals.SingleOrDefaultAsync(m => m.Id == id);
        if (hospital is null)
        {
            return NotFound();
        }

        return View(hospital);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hospital = await context.Hospitals.SingleOrDefaultAsync(m => m.Id == id);
        context.Hospitals.Remove(hospital);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}