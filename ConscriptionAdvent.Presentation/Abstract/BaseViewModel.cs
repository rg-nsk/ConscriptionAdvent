﻿using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.EventArguments;
using System;

namespace ConscriptionAdvent.Presentation.Abstract
{
    public abstract class BaseViewModel : UIModel, IStateChanged
    {
        #region IStateChanged implementation

        public void OnStateChanged(string state, StateResult stateResult, Exception ex = null)
        {
            StateChanged?.Invoke(this, new StateEventArgs(state, stateResult, ex));
        }

        public event EventHandler<StateEventArgs> StateChanged;

        #endregion
    }
}
