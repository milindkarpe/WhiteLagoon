using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class AmenityRespository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AmenityRespository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Amenity obj)
        {
            _dbContext.Update(obj);
        }

    }
}
