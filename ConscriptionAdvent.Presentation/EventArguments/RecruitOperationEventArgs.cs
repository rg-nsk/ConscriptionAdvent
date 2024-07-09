using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.Models;
using System;

namespace ConscriptionAdvent.Presentation.EventArguments
{
    public class RecruitOperationEventArgs : EventArgs
    {
        public RecruitOperation RecruitOperation { get; }
        public RecruitShortUIModel RecruitShortUIModel { get; }

        public RecruitOperationEventArgs(RecruitOperation recruitOperation, RecruitShortUIModel recruitShortUIModel = null)
        {
            RecruitOperation = recruitOperation;
            RecruitShortUIModel = recruitShortUIModel;
        }
    }
}
