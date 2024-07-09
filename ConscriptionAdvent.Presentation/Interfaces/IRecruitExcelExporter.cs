using ConscriptionAdvent.Domain.DomainModels;
using ConscriptionAdvent.Presentation.Models;
using System.Collections.Generic;

namespace ConscriptionAdvent.Presentation.Interfaces
{
    public interface IRecruitExcelExporter
    {
        void ExportRecruitInfoesToExcel(bool isPrintAfterExport, RecruitInfo firstRecruit, 
            RecruitInfo secondRecruit = null);

        void ExportRecruitShortUIModelsToExcelTable(IEnumerable<RecruitShortUIModel> recruitShortUIModels,
            string regionalCollectionPoint, 
            string conscriptionDate, bool isPrintAfterExport);
    }
}
