﻿using ConscriptionAdvent.Presentation.Interfaces;

namespace ConscriptionAdvent.Export
{
    public class RecruitExcelExporterFactory : IRecruitExcelExporterFactory
    {
        private readonly bool _isOpenFile;

        public RecruitExcelExporterFactory(bool isOpenFile = false)
        {
            _isOpenFile = isOpenFile;
        }

        public IRecruitExcelExporter Create(string templateFilePath, string filePath)
        {
            return new RecruitExcelExporter(templateFilePath, filePath, _isOpenFile);
        }
    }
}
