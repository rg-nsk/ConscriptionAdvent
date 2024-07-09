using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Data.SQLite.Abstract
{
    public interface IDbContextCache : IDbContextFactory, IDbContextRepository, IDisposable { }
}
