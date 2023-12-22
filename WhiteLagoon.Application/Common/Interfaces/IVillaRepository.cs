using System.Linq.Expressions;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Common.Interfaces
{
    public interface IVillaNumberRepository: IRepository<VillaNumber>
    {
        void Update(VillaNumber villa);
        void Save();
    }
}
