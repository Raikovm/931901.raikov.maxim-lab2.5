using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab5.Models;

namespace WebLab5.Controllers;

public class DoctorsController : Controller
{
    private readonly DataContext context;
    public DoctorsController(DataContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await context.Doctors.ToListAsync());
    }

    public IActionResult Create()
    {
        return View(new Doctor());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Doctor model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await context.Doctors.AddAsync(model);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
        if (doctor is null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var doctor = await context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
        context.Doctors.Remove(doctor);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var doctor = await context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
        if (doctor is null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Doctor model)
    {
        var doctor = await context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
        if (doctor is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(model);
        doctor.Name = model.Name;
        doctor.Specialization = model.Specialization;

        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var doctor = await context.Doctors.SingleOrDefaultAsync(m => m.Id == id);
        if (doctor is null)
        {
            return NotFound();
        }

        return View(doctor);
    }
}