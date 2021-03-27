using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore01.Data;
using WebApplicationCore01.Models;

namespace WebApplicationCore01.Controllers
{
    public class LibrosController : Controller
    {
        //Invocar el contexto para acceder a la db
            private readonly ApplicationDbContext _context;

        //Creada por mi
        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }
   
        //Http Get Index
        public IActionResult Index()
        {
            //Crearndo una lista con los libros de la db
            IEnumerable<Libro> listLibros = _context.Libro;
            return View(listLibros); // retornando en la vista la lista de libros
        }

        //Http Get Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libro.Add(libro);
                _context.SaveChanges();
            }

            TempData["Mensaje"] = "El libro se ha creado correctamente";
            return RedirectToAction("Index");
        }

        // HTTP GET Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Obtener el libro
            var libro = _context.Libro.Find(id);

            if(libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libro.Update(libro);
                _context.SaveChanges();
            }

            TempData["Mensaje"] = "El libro se ha actualizado correctamente";
            return RedirectToAction("Index");
        }

        // HTTP GET Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Obtener el libro
            var libro = _context.Libro.Find(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLibro(int? id)
        {
            //Obtener el libro por id
            var libro = _context.Libro.Find(id);

            if(libro == null)
            {
                return NotFound();
            }

                _context.Libro.Remove(libro);
                _context.SaveChanges();


            TempData["Mensaje"] = "El libro se ha eliminado correctamente";
            return RedirectToAction("Index");
        }


    }
}
