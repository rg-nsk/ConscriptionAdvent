﻿using ConscriptionAdvent.Data.SQLite.Abstract;
using ConscriptionAdvent.Domain.Interfaces;
using System;
using System.Data.Entity;

namespace ConscriptionAdvent.Data.SQLite.Implementations
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextCache _dbContextCache;

        public UnitOfWorkFactory(IDbContextCache dbContextCache)
        {
            if (dbContextCache == null)
            {
                throw new ArgumentNullException(nameof(dbContextCache));
            }

            _dbContextCache = dbContextCache;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_dbContextCache);
        }
    }
}
