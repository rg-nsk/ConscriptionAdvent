using ConscriptionAdvent.Presentation.RecruitCommands.Parameters;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.Abstract
{
    public interface IParameterizedCommandAsync<T>
    {
        Task ExecuteAsync(T parameters);
    }
}
