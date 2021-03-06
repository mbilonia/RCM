﻿using Microsoft.EntityFrameworkCore;
using RCM.Domain.Models.BancoModels;
using RCM.Domain.Repositories;
using RCM.Infra.Data.Context;
using System;
using System.Linq;

namespace RCM.Infra.Data.Repositories
{
    public class BancoRepository : BaseRepository<Banco>, IBancoRepository
    {
        public BancoRepository(RCMDbContext dbContext) : base(dbContext)
        {
        }

        public override Banco GetById(Guid id, bool loadRelatedData = true)
        {
            if (loadRelatedData)
            {
                return _dbSet
                    .Include(c => c.Cheques)
                    .FirstOrDefault(b => b.Id == id);
            }
            else
                return base.GetById(id, false);
        }
    }
}
