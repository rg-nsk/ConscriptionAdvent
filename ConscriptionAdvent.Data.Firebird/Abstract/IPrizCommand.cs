using ConscriptionAdvent.Data.Firebird.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Data.Firebird.Abstract
{
    public interface IPrizCommand
    {
        void Insert(PRIZ entity);
        void Insert(IEnumerable<PRIZ> entities);
        void Delete(int id);
    }
}
