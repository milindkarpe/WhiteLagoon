using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Repository;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class AmenityController : Controller
    {
        IUnitOfWork _UnitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Amenity> amunities = _UnitOfWork.AmenityRepository.GetAll(includeProperties: "Villa");
            return View(amunities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new AmenityVM
            {
                VillaList = _UnitOfWork.VillaRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                })
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
            ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {
                _UnitOfWork.AmenityRepository.Add(obj.amenity);
                _UnitOfWork.AmenityRepository.Save();
                TempData["success"] = "The amenity has been saved successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int AmenityId)
        {
            AmenityVM amenityVM = new AmenityVM
            {
                VillaList = _UnitOfWork.VillaRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }),
                amenity = _UnitOfWork.AmenityRepository.Get(u => u.Id == AmenityId)
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Edit(AmenityVM obj)
        {
            ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {
                _UnitOfWork.AmenityRepository.Update(obj.amenity);
                _UnitOfWork.AmenityRepository.Save();
                TempData["success"] = "The amenity has been updated successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int AmenityId)
        {
            AmenityVM amenityVM = new AmenityVM
            {
                VillaList = _UnitOfWork.VillaRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }),
                amenity = _UnitOfWork.AmenityRepository.Get(u => u.Id == AmenityId)
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM obj)
        {
            var objDelete = _UnitOfWork.AmenityRepository.Get(u => u.Id == obj.amenity.Id);
            if (objDelete is not null)
            {
                _UnitOfWork.AmenityRepository.Remove(objDelete);
                _UnitOfWork.AmenityRepository.Save();
                TempData["success"] = "The amenity has been deleted successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
