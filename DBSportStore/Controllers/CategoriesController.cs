
using DBSportStore.Filters;
using DBSportStore.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class CategoriesController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();

        // GET: Categories
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        // GET: Categories/Details/5
        // Giữ công khai để Guest và Customer vẫn xem được
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Category.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Create(
            [Bind(Include = "CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Category.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Edit(
            [Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category oldCategory =
                db.Category.Find(category.CategoryID);

            if (oldCategory == null)
            {
                return HttpNotFound();
            }

            oldCategory.CategoryName = category.CategoryName;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Category.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Category.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            db.Category.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}