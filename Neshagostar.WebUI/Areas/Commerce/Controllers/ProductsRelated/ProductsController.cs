using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Neshagostar.DAL.DataModel;
using Neshagostar.DAL.DataModel.CommerceRelated.ProductsRelated;

namespace Neshagostar.WebUI.Areas.Commerce.Controllers.ProductsRelated
{
    public class ProductsController : Controller
    {
        private NeshagostarContext db = new NeshagostarContext();

        // GET: Commerce/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.PipeDiameter).Include(p => p.PipeProfile).Include(p => p.ProductCategory).Include(p => p.RingStiffness);
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Index.cshtml", products.ToList());
        }

        // GET: Commerce/Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Details.cshtml",product);
        }

        // GET: Commerce/Products/Create
        public ActionResult Create()
        {
            ViewBag.PipeDiameterId = new SelectList(db.PipeDiameters, "Id", "Size");
            ViewBag.PipeProfileId = new SelectList(db.PipeProfiles, "Id", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            ViewBag.RingStiffnessId = new SelectList(db.RingStiffnesses, "Id", "Description");
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Create.cshtml");
        }

        // POST: Commerce/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductCategoryId,PipeProfileId,PipeDiameterId,RingStiffnessId,Title")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PipeDiameterId = new SelectList(db.PipeDiameters, "Id", "Size", product.PipeDiameterId);
            ViewBag.PipeProfileId = new SelectList(db.PipeProfiles, "Id", "Name", product.PipeProfileId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.ProductCategoryId);
            ViewBag.RingStiffnessId = new SelectList(db.RingStiffnesses, "Id", "Description", product.RingStiffnessId);
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Create.cshtml",product);
        }

        // GET: Commerce/Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.PipeDiameterId = new SelectList(db.PipeDiameters, "Id", "Size", product.PipeDiameterId);
            ViewBag.PipeProfileId = new SelectList(db.PipeProfiles, "Id", "Name", product.PipeProfileId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.ProductCategoryId);
            ViewBag.RingStiffnessId = new SelectList(db.RingStiffnesses, "Id", "Description", product.RingStiffnessId);
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Edit.cshtml",product);
        }

        // POST: Commerce/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductCategoryId,PipeProfileId,PipeDiameterId,RingStiffnessId,Title")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PipeDiameterId = new SelectList(db.PipeDiameters, "Id", "Size", product.PipeDiameterId);
            ViewBag.PipeProfileId = new SelectList(db.PipeProfiles, "Id", "Name", product.PipeProfileId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.ProductCategoryId);
            ViewBag.RingStiffnessId = new SelectList(db.RingStiffnesses, "Id", "Description", product.RingStiffnessId);
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Edit.cshtml",product);
        }

        // GET: Commerce/Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Areas/Commerce/Views/ProductsRelated/Products/Delete.cshtml",product);
        }

        // POST: Commerce/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        [HttpGet]
        public JsonResult GetProducts()
        {
            var products = db.Products.Select(p => new { id = p.Id, name = p.Title, category = p.ProductCategory.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

    }
}
