using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands.Parameters
{
    public class RemoveRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public RecruitShortUIModel RecruitShortUIModel { get; }

        public RemoveRecruitCommandParameters(RecruitShortUIModel recruitShortUIModel,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitShortUIModel == null)
            {
                throw new ArgumentNullException(nameof(recruitShortUIModel));
            }

            RecruitShortUIModel = recruitShortUIModel;
        }
    }
}
