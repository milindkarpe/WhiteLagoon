using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IVillaRepository VillaRepository { get; private set; }
        public IVillaNumberRepository VillaNumberRepository { get; private set; }
        public IAmenityRepository AmenityRepository { get; private set; }

        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            VillaRepository = new VillaRespository(_dbContext);
            VillaNumberRepository = new VillaNumberRespository(_dbContext);
            AmenityRepository = new AmenityRespository(_dbContext);
        }
    }
}
