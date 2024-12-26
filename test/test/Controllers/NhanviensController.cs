using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using test.Models;

namespace test.Controllers
{
    public class NhanviensController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string manv, string matkhau)
        {
            var nhanvien = db.Nhanviens.FirstOrDefault(n => n.manv.ToString() == manv.ToString() && n.matkhau == matkhau);
            if (nhanvien != null)
            {
                Session["User"] = nhanvien;
                TempData["SuccessMessage"] = "Đăng nhập thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Mã nhân viên hoặc mật khẩu không đúng!";
                return View();
            }    
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        // GET: Nhanviens
        public ActionResult Index()
        {
            var nhanviens = db.Nhanviens.Include(n => n.Phongban);
           
            return View(nhanviens.ToList());
        }
        public ActionResult menu()
        {
            var phongbans = db.Phongbans.ToList();
            return PartialView(phongbans);
        }
        public ActionResult DanhSachNhanVien(int id)
        {
            var nhanviens = db.Nhanviens.Where(n => n.maphong == id);
            return View(nhanviens);
        }
        // GET: Nhanviens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // GET: Nhanviens/Create
        public ActionResult Create()
        {
            ViewBag.maphong = new SelectList(db.Phongbans, "maphong", "tenphong");
            return View();
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAjax([Bind(Include = "hotennv,tuoi,diachi,luongnv,maphong,matkhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Nhanviens.Add(nhanvien);
                db.SaveChanges();
                return Json(new { success = true, message = "Thêm nhân viên thành công!"});
            }
            else
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng kiểm tra lại!" });
            } 
                

           
        }

        // GET: Nhanviens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.maphong = new SelectList(db.Phongbans, "maphong", "tenphong", nhanvien.maphong);
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditAjax([Bind(Include = "manv,hotennv,tuoi,diachi,luongnv,maphong,matkhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Sửa nhân viên thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng kiểm tra lại!" });
            }
        }

        // GET: Nhanviens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nhanvien = db.Nhanviens.Find(id);
            if (nhanvien != null)
            {
                db.Nhanviens.Remove(nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng kiểm tra lại!" });
            }
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
