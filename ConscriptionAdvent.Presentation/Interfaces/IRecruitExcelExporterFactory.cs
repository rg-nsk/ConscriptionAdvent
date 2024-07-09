namespace ConscriptionAdvent.Presentation.Interfaces
{
    public interface IRecruitExcelExporterFactory
    {
        IRecruitExcelExporter Create(string templateFilePath, string filePath);
    }
}
