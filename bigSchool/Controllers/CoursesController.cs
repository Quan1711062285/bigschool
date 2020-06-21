using bigSchool.Models;
using bigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace bigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]

        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place,

            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Create", "Courses");
        }
        public ActionResult Index()
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                return View(dbModel.Courses.ToList());
            }
        }
        public ActionResult Edit(int id)
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                return View(dbModel.Courses.Where(x => x.Id == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, Course course)
        {
            try
            {
                using (ApplicationDbContext dbModel = new ApplicationDbContext())
                {
                    dbModel.Entry(course).State = EntityState.Modified;
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                return View(dbModel.Courses.Where(x => x.Id == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, Course course)
        {
            try
            {
                using (ApplicationDbContext dbModel = new ApplicationDbContext())
                {
                    course = dbModel.Courses.Where(x => x.Id == id).FirstOrDefault();
                    dbModel.Courses.Remove(course);
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Create");
            }
            catch
            {

                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                return View(dbModel.Courses.Where(x => x.Id == id).FirstOrDefault());
            }
        }
    }
}