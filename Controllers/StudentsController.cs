using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buoi5.Models;
using System.Linq;

namespace Buoi5.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppicationDbContext _context;

        public StudentsController(AppicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public IActionResult Index()
        {
            var students = _context.Students.Include(s => s.Grade).ToList();
            ViewBag.Grades = _context.Grades.OrderBy(g => g.gradeName).ToList();
            return View(students);
        }

        // GET: Students/Details/5
        public IActionResult Details(int id)
        {
            var student = _context.Students.Include(s => s.Grade).FirstOrDefault(s => s.studentId == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.Grades = _context.Grades.ToList();
            if (ViewBag.Grades == null || !((List<Grades>)ViewBag.Grades).Any())
            {
                ModelState.AddModelError("", "No grades available. Please add a grade first.");
            }
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Students student)
        {
            // Remove validation errors for Grade navigation property if any
            ModelState.Remove("Grade");

            // Check if gradeId is valid
            if (student.gradeId <= 0)
            {
                ModelState.AddModelError("gradeId", "Please select a valid grade.");
            }
            else
            {
                // Check if the selected grade exists
                var gradeExists = _context.Grades.Any(g => g.gradeId == student.gradeId);
                if (!gradeExists)
                {
                    ModelState.AddModelError("gradeId", "Selected grade does not exist.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(student);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Student created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the student.");
                    Console.WriteLine(ex.Message);
                }
            }

            // Debug ModelState errors
            foreach (var key in ModelState.Keys)
            {
                var entry = ModelState[key];
                foreach (var error in entry.Errors)
                {
                    Console.WriteLine($"Validation Error for {key}: {error.ErrorMessage}");
                }
            }

            ViewBag.Grades = _context.Grades.ToList();
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Grades = _context.Grades.ToList();
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Students student)
        {
            if (id != student.studentId)
            {
                return NotFound();
            }

            // Remove validation errors for Grade navigation property if any
            ModelState.Remove("Grade");

            // Check if gradeId is valid
            if (student.gradeId <= 0)
            {
                ModelState.AddModelError("gradeId", "Please select a valid grade.");
            }
            else
            {
                // Check if the selected grade exists
                var gradeExists = _context.Grades.Any(g => g.gradeId == student.gradeId);
                if (!gradeExists)
                {
                    ModelState.AddModelError("gradeId", "Selected grade does not exist.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            // Debug ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            ViewBag.Grades = _context.Grades.ToList();
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Include(s => s.Grade).FirstOrDefault(s => s.studentId == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}