using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaNumberRespository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaNumberRespository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(VillaNumber villa)
        {
            _dbContext.Update(villa);
        }
    }
}
