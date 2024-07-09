﻿using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.EventArguments;
using ConscriptionAdvent.Presentation.Models.CardGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands.Parameters
{
    public class SaveRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public RecruitOperationEventArgs RecruitOperationEventArgs { get; }
        public RecruitCardGroup RecruitCardGroup { get; }

        public SaveRecruitCommandParameters(RecruitOperationEventArgs recruitOperationEventArgs,
            RecruitCardGroup recruitCardGroup,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitOperationEventArgs == null)
            {
                throw new ArgumentNullException(nameof(recruitOperationEventArgs));
            }

            if (recruitCardGroup == null)
            {
                throw new ArgumentNullException(nameof(recruitCardGroup));
            }

            RecruitOperationEventArgs = recruitOperationEventArgs;
            RecruitCardGroup = recruitCardGroup;
        }
    }
}
