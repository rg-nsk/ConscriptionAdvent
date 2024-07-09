using ConscriptionAdvent.Domain.DomainModels;
using ConscriptionAdvent.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands.Parameters
{
    public class AddRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public RecruitInfo RecruitInfo { get; }

        public AddRecruitCommandParameters(RecruitInfo recruitInfo, 
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitInfo == null)
            {
                throw new ArgumentNullException(nameof(recruitInfo));
            }

            RecruitInfo = recruitInfo;
        }
    }
}
