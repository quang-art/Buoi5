using Buoi5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi5.Controllers
{
    public class GradeController : Controller
    {
        private readonly AppicationDbContext _context;
        public GradeController(AppicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Grades> listGrade = _context.Grades.ToList();
            return View(listGrade);
        }
    }
}
