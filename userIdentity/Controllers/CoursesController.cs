using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using userIdentity.Data;
using userIdentity.Models;

namespace userIdentity.Controllers
{

    public class CoursesController : Controller
    {
        private readonly CoursesContext _context;
        public CoursesController(CoursesContext context)
        {

            _context = context;
        }
        // GET: CoursesController
        public ActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        // GET: CoursesController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: CoursesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost]

        public async Task<ActionResult> Create(Courses collection)
        {
            collection.CreateDate = DateTime.Now;
            _context.Courses.Add(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: CoursesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoursesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoursesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
