using ConscriptionAdvent.Presentation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands.Parameters
{
    public abstract class BaseRecruitCommandParameter
    {
        public IStateChanged StateChanged { get; }

        public BaseRecruitCommandParameter(IStateChanged stateChanged)
        {
            if (stateChanged == null)
            {
                throw new ArgumentNullException(nameof(stateChanged));
            }

            StateChanged = stateChanged;
        }
    }
}
