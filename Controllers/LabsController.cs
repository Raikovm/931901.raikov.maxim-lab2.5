using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab5.Models;

namespace WebLab5.Controllers;

public class LabsController : Controller
{
    private readonly DataContext context;
    public LabsController(DataContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await context.Labs.ToListAsync());
    }

    public IActionResult Create()
    {
        return View(new Lab());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Lab model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await context.Labs.AddAsync(model);
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        Lab? lab = await context.Labs.SingleOrDefaultAsync(m => m.Id == id);
        if (lab is null)
        {
            return NotFound();
        }

        return View(lab);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Lab? lab = await context.Labs.SingleOrDefaultAsync(m => m.Id == id);
        context.Labs.Remove(lab);
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var lab = await context.Labs.SingleOrDefaultAsync(m => m.Id == id);
        if (lab == null)
        {
            return NotFound();
        }

        return View(lab);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Lab model)
    {
        var lab = await context.Labs.SingleOrDefaultAsync(m => m.Id == id);
        if (lab is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(model);
        lab.Name = model.Name;
        lab.Address = model.Address;

        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var lab = await context.Labs.SingleOrDefaultAsync(m => m.Id == id);
        if (lab is null)
        {
            return NotFound();
        }

        return View(lab);
    }
}