using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repository;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;


        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: VillaController
        public ActionResult Index()
        {
            IEnumerable<Villa> villas = _unitOfWork.VillaRepository.GetAll();
            return View(villas);
        }

        // GET: VillaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VillaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VillaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Villa villa)
        {
            try
            {
                if (villa.Name == villa.Description)
                {
                    ModelState.AddModelError("name", "The description cannot exactly match the Name.");
                }
                if (ModelState.IsValid)
                {
                    if (villa.Image is not null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");
                        using (var filesteram = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                        {
                            villa.Image.CopyTo(filesteram);
                            villa.ImageUrl = @"\images\VillaImage\" + fileName;
                        }
                    }
                    
                    _unitOfWork.VillaRepository.Add(villa);
                    _unitOfWork.VillaRepository.Save();
                    TempData["success"] = "The villa has been created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: VillaController/Edit/5
        public ActionResult Edit(int villaId)
        {
            var obj = _unitOfWork.VillaRepository.Get(u => u.Id == villaId);

            if (obj.Id > 0)
            {

                return View(obj);
            }
            else
                RedirectToAction("Index");

            return View();
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Villa villa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (villa.Image is not null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");
                        using (var filesteram = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                        {
                            villa.Image.CopyTo(filesteram);
                            villa.ImageUrl = @"\images\VillaImage\" + fileName;
                        }
                    }

                    _unitOfWork.VillaRepository.Update(villa);
                    _unitOfWork.VillaRepository.Save();
                    TempData["success"] = "The villa has been updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(villa);
            }
            catch
            {
                return View();
            }
        }

        // GET: VillaController/Delete/5
        public ActionResult Delete(int villaId)
        {
            var obj = _unitOfWork.VillaRepository.Get(u => u.Id == villaId);

            if (obj.Id > 0)
                return View(obj);
            else
                RedirectToAction("Index");

            return View();
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Villa obj)
        {
            try
            {
                var objDelete = _unitOfWork.VillaRepository.Get(u => u.Id == obj.Id);
                if (objDelete is not null)
                {
                    _unitOfWork.VillaRepository.Remove(objDelete);
                    _unitOfWork.VillaRepository.Save();
                    TempData["success"] = "The villa has been deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
