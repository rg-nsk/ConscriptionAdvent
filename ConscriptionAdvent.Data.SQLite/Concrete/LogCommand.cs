using ConscriptionAdvent.Data.SQLite.Abstract;
using ConscriptionAdvent.Data.SQLite.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Data.SQLite.Concrete
{
    public class LogCommand : ILogCommand
    {
        private IDbContextCache _dbContextCache;

        public LogCommand(IDbContextCache dbContextCache)
        {
            if (dbContextCache == null)
            {
                throw new ArgumentNullException(nameof(dbContextCache));
            }

            _dbContextCache = dbContextCache;
        }

        public void Insert(log message)
        {
            _dbContextCache.Context.Set<log>().Add(message);
        }
    }
}
