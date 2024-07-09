using ConscriptionAdvent.Domain.DomainModels;
using ConscriptionAdvent.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands.Parameters
{
    public class TransmitRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public IEnumerable<long> RecruitIds { get; }

        public TransmitRecruitCommandParameters(IEnumerable<long> recruitIds,
            IStateChanged stateChanged) : base(stateChanged)
        {
            if (recruitIds == null)
            {
                throw new ArgumentNullException(nameof(recruitIds));
            }

            RecruitIds = recruitIds;
        }
    }
}
