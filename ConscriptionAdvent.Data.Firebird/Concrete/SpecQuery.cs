using ConscriptionAdvent.Data.Firebird.Abstract;
using ConscriptionAdvent.Data.Firebird.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Data.Firebird.Concrete
{
    public class SpecQuery : ISpecQuery
    {
        private readonly IDbContextFactory _dbContextFactory;

        public SpecQuery(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextFactory));
            }

            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<SPEC> GetAll()
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<SPEC>().ToList();
            }
        }

        public SPEC Get(int id)
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.Set<SPEC>().Find(id);
            }
        }
    }
}
