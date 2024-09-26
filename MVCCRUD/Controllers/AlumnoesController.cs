using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class AlumnoesController : Controller
    {
        private readonly graceContext _context;

        public AlumnoesController(graceContext context)
        {
            _context = context;
        }

        // GET: Alumnoes
        public async Task<IActionResult> Index()
        {
            var graceContext = _context.Alumnos.Include(a => a.IdMateriaNavigation).Include(a => a.IdProfesorNavigation);
            return View(await graceContext.ToListAsync());
        }

        // GET: Alumnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.IdMateriaNavigation)
                .Include(a => a.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdAlumno == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnoes/Create
        public IActionResult Create()
        {
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "NombreMateria");
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "NombreProfesor");
            return View();
        }

        // POST: Alumnoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlumno,NombreAlumno,IdMateria,IdProfesor")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", alumno.IdMateria);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", alumno.IdProfesor);
            return View(alumno);
        }

        // GET: Alumnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "NombreMateria", alumno.IdMateria);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "NombreProfesor", alumno.IdProfesor);
            return View(alumno);
        }

        // POST: Alumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlumno,NombreAlumno,IdMateria,IdProfesor")] Alumno alumno)
        {
            if (id != alumno.IdAlumno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.IdAlumno))
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
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", alumno.IdMateria);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", alumno.IdProfesor);
            return View(alumno);
        }

        // GET: Alumnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.IdMateriaNavigation)
                .Include(a => a.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdAlumno == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumnos == null)
            {
                return Problem("Entity set 'graceContext.Alumnos'  is null.");
            }
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
          return (_context.Alumnos?.Any(e => e.IdAlumno == id)).GetValueOrDefault();
        }
    }
}
