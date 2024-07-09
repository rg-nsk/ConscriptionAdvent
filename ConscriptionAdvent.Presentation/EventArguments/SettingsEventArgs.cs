﻿using System;
using System.Collections.Generic;

namespace ConscriptionAdvent.Presentation.EventArguments
{
    public class SettingsEventArgs : EventArgs
    {
        public IReadOnlyDictionary<string, string> Settings { get; }

        public SettingsEventArgs(IReadOnlyDictionary<string, string> settings)
        {
            Settings = settings;
        }
    }
}
