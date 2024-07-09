using System;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();

        Task CommitAsync();
    }
}
