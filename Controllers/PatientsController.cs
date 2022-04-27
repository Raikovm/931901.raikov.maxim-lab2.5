using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLab5.Models;

namespace WebLab5.Controllers
{
    public class PatientsController : Controller
    {
        private readonly DataContext context;
        public PatientsController(DataContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Patients.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Patient());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient model)
        {
            if (!ModelState.IsValid) return View(model);
            await context.Patients.AddAsync(model);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var patient = await context.Patients
                .SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await context.Patients.SingleOrDefaultAsync(m => m.Id == id);
            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await context.Patients
                .SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Patient model)
        {
            var patient = await context.Patients
                .SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(model);
            patient.Name = model.Name;
            patient.Diagnosis = model.Diagnosis;

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await context.Patients
                .SingleOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
    }
}
