using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.EventArguments;
using ConscriptionAdvent.Presentation.Models;
using ConscriptionAdvent.Presentation.Models.CardGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.Interfaces
{
    public interface IRecruitCardGroupFactory
    {
        RecruitCardGroup Create(RecruitOperationEventArgs e);
    }
}
