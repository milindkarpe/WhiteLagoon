using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Web.ViewModels
{
    public class AmenityVM
    {
        public Amenity? amenity { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList { get; set; }

    }
}
