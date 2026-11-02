using System;
using System.Collections.Generic;
using DBSportStore.Filters;
using DBSportStore.Models;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class OrderProesController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();

        // GET: OrderProes
        [AdminAuthorize]
        public ActionResult Index()
        {
            var orderPro = db.OrderPro.Include(o => o.Customer);
            return View(orderPro.ToList());
        }

        // GET: OrderProes/Details/5

        [AdminAuthorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderPro orderPro = db.OrderPro
                .Include(o => o.Customer)
                .Include(o => o.OrderDetail.Select(d => d.Product))
                .FirstOrDefault(o => o.OrderID == id);

            if (orderPro == null)
            {
                return HttpNotFound();
            }

            return View(orderPro);
        }

        // GET: OrderProes/Create
        [AdminAuthorize]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(
                db.Customer, "CustomerID", "FullName");

            return View();
        }

        // POST: OrderProes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Create(
            [Bind(Include = "OrderID,OrderDate,CustomerID,ShippingAddress,TotalAmount,Status")]
            OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                db.OrderPro.Add(orderPro);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(
                db.Customer,
                "CustomerID",
                "FullName",
                orderPro.CustomerID);

            return View(orderPro);
        }

        // GET: OrderProes/Edit/5
        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderPro orderPro = db.OrderPro.Find(id);

            if (orderPro == null)
            {
                return HttpNotFound();
            }

            return View(orderPro);
        }

        // POST: OrderProes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Edit(int OrderID, string Status)
        {
            string[] allowedStatus =
            {
                "Chờ xử lý",
                "Đã xác nhận",
                "Đang giao",
                "Hoàn thành",
                "Đã hủy"
            };

            if (!allowedStatus.Contains(Status))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderPro orderPro = db.OrderPro.Find(OrderID);

            if (orderPro == null)
            {
                return HttpNotFound();
            }

            orderPro.Status = Status;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: OrderProes/Delete/5
        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderPro orderPro = db.OrderPro.Find(id);

            if (orderPro == null)
            {
                return HttpNotFound();
            }

            return View(orderPro);
        }

        // POST: OrderProes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPro orderPro = db.OrderPro.Find(id);

            if (orderPro == null)
            {
                return HttpNotFound();
            }

            db.OrderPro.Remove(orderPro);
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