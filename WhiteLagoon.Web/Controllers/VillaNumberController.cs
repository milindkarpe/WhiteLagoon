using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: VillaController
        public ActionResult Index()
        {
            IEnumerable<VillaNumber> villas = _unitOfWork.VillaNumberRepository.GetAll(includeProperties: "Villa");
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
            VillaNumberVM vm = new VillaNumberVM
            {
                VillaList = _unitOfWork.VillaRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(vm);
        }

        // POST: VillaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VillaNumberVM obj)
        {
            try
            {
                if (_unitOfWork.VillaNumberRepository.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number))
                {
                    TempData["error"] = "The villa number is already exists.";
                    goto Exit;
                }

                ModelState.Remove("Villa");
                if (ModelState.IsValid)
                {
                    _unitOfWork.VillaNumberRepository.Add(obj.VillaNumber);
                    _unitOfWork.VillaNumberRepository.Save();
                    TempData["success"] = "The villa number has been created successfully.";
                    return RedirectToAction(nameof(Index));
                }

            Exit:
                obj.VillaList = _unitOfWork.VillaRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
            catch
            {
                return View();
            }
        }

        // GET: VillaController/Edit/5
        public ActionResult Edit(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.VillaRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumberRepository.Get(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VillaNumberVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.VillaNumberRepository.Update(obj.VillaNumber);
                    _unitOfWork.VillaNumberRepository.Save();
                    TempData["success"] = "The villa number has been updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(obj);
            }
            catch
            {
                return View();
            }
        }

        // GET: VillaController/Delete/5
        public ActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.VillaRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumberRepository.Get(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(VillaNumberVM obj)
        {
            try
            {
                var objDelete = _unitOfWork.VillaNumberRepository.Get(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
                if (objDelete is not null)
                {
                    _unitOfWork.VillaNumberRepository.Remove(objDelete);
                    _unitOfWork.VillaNumberRepository.Save();
                    TempData["success"] = "The villa number has been deleted successfully.";
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
