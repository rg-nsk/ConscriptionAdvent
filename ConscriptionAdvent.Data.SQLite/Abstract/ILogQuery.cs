using ConscriptionAdvent.Data.SQLite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Data.SQLite.Abstract
{
    public interface ILogQuery
    {
        log Get(long id);
    }
}
