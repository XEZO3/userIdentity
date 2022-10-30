using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _hostEnviroment;
        
        public CoursesController(CoursesContext context, IWebHostEnvironment hostEnviroment)
        {

            _context = context;
            _hostEnviroment = hostEnviroment;
        }
        // GET: CoursesController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        // GET: CoursesController/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: CoursesController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Courses collection)
        {
            var file = HttpContext.Request.Form.Files;
            collection.CreateDate = DateTime.Now;
            collection.ImageUrl = uploadfile(file);
            _context.Courses.Add(collection);

             _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CoursesController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoursesController/Delete/5
        
        public string uploadfile(dynamic file)
        {
            string rootpath = _hostEnviroment.WebRootPath;

            string filename = Guid.NewGuid().ToString();
            var upload = Path.Combine(rootpath, @"images\courses");
            var extention = Path.GetExtension(file[0].FileName);
            using (var filestrem = new FileStream(Path.Combine(upload, filename + extention), FileMode.Create))
            {
                file[0].CopyTo(filestrem);
            }
            return filename + extention;

        }

    }
}
