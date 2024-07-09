using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands.Parameters
{
    public class ExportTableRecruitCommandParameters : BaseRecruitCommandParameter
    {
        public IEnumerable<RecruitShortUIModel> RecruitShortUIModels { get; }
        public string ConscriptionDate { get; }
        public string RegionalCollectionPoint { get; }

        public bool PrintAfterExport { get; }

        public ExportTableRecruitCommandParameters(IEnumerable<RecruitShortUIModel> recruitShortUIModels, 
            string conscriptionDate, 
            string regionalCollectionPoint,
            IStateChanged stateChanged, bool printAfterExport) : base(stateChanged)
        {
            if (recruitShortUIModels == null)
            {
                throw new ArgumentNullException(nameof(recruitShortUIModels));
            }

            RecruitShortUIModels = recruitShortUIModels;
            ConscriptionDate = conscriptionDate;
            RegionalCollectionPoint = regionalCollectionPoint;
            PrintAfterExport = printAfterExport;
        }
    }
}
