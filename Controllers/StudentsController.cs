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
            if (ViewBag.Grades == null )
            {
                ModelState.AddModelError("", "No grades available. Please add a grade first.");
            }
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Students student, IFormCollection form)
        {
            Console.WriteLine($"Form data - gradeId: {form["gradeId"]}");
            Console.WriteLine($"Model data - studentId: {student.studentId}, FirstName: {student.FirstName}, LastName: {student.LastName}, gradeId: {student.gradeId}");

            // Kiểm tra giá trị gradeId trước khi validation
            if (student.gradeId <= 0)
            {
                ModelState.AddModelError("gradeId", "Please select a valid grade.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Student created successfully!";
                return RedirectToAction(nameof(Index));
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                }
                catch (DbUpdateException ex)
                {
                    // In lỗi nếu có vấn đề khi lưu vào database
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, hiển thị lỗi để debug
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage); // In lỗi ra console để debug
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
            }
            return RedirectToAction(nameof(Index));
        }
    }
}