using ConscriptionAdvent.Presentation.Enums;
using System;

namespace ConscriptionAdvent.Presentation.Abstract
{
    public interface IStateChanged
    {
        void OnStateChanged(string state, StateResult stateResult, Exception ex = null);
    }
}
