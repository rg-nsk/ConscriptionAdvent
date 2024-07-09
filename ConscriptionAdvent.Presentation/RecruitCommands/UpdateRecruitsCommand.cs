using ConscriptionAdvent.Domain.Enums;
using ConscriptionAdvent.Domain.ExtensionMethods;
using ConscriptionAdvent.Domain.Interfaces;
using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.Interfaces;
using ConscriptionAdvent.Presentation.Models;
using ConscriptionAdvent.Presentation.RecruitCommands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands
{
    public class UpdateRecruitsCommand : IParameterizedCommandAsync<UpdateRecruitsCommandParameters>
    {
        public const string CommandName = "Обновление списка призывников";
        public string CommandSuccess = "Список обновлен";

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRecruitInfoRepository _recruitInfoRepository;
        private readonly IImportedRecruitRepository _importedRecruitRepository;
        private readonly IEventService _eventService;

        public UpdateRecruitsCommand(IUnitOfWorkFactory unitOfWorkFactory,
            IRecruitInfoRepository recruitInfoRepository,
            IImportedRecruitRepository importedRecruitRepository,
            IEventService eventService)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            if (recruitInfoRepository == null)
                throw new ArgumentNullException(nameof(recruitInfoRepository));

            if (importedRecruitRepository == null)
                throw new ArgumentNullException(nameof(importedRecruitRepository));

            if (eventService == null)
                throw new ArgumentNullException(nameof(eventService));

            _unitOfWorkFactory = unitOfWorkFactory;
            _recruitInfoRepository = recruitInfoRepository;
            _importedRecruitRepository = importedRecruitRepository;
            _eventService = eventService;
        }

        public async Task ExecuteAsync(UpdateRecruitsCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var recruitShortUIModels = new List<RecruitShortUIModel>();

            var recruitInfoes = await _recruitInfoRepository.GetAsync(parameters.SelectedRegionalCollectionPoint, 
                parameters.ConscriptionDate, 
                parameters.Surname);

            foreach (var recruitInfo in recruitInfoes)
            {
                recruitShortUIModels.Add(new RecruitShortUIModel(
                    surname: recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Surname,
                    name: recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Name,
                    patronymic: recruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Patronymic,
                    passportCode: recruitInfo.Envelope.PassportInfo.Code.Value,
                    birthDate: recruitInfo.Envelope.PassportInfo.PersonInfo.BirthInfo.Date,
                    regionalCollectionPoint: recruitInfo.ServiceInfo.RegionalCollectionPoint,
                    conscriptionDate: recruitInfo.ServiceInfo.ConscriptionDate,
                    storage: recruitInfo.Storage,
                    sqliteId: recruitInfo.ServiceInfo.SqliteId,
                    firebirdId: recruitInfo.ServiceInfo.FirebirdId
                ));
            }

            var imported = _importedRecruitRepository.Get(parameters.SelectedRegionalCollectionPoint,
                parameters.ConscriptionDate,
                parameters.Surname);

            recruitShortUIModels.AddRange(imported);
            


            var filteredRecruits = recruitShortUIModels
                .GroupBy(r => new { r.Storage, r.Surname, r.Name, r.Patronymic, r.RegionalCollectionPoint })
                .Select(g => g.First())
                .Where(r => !string.IsNullOrWhiteSpace(parameters.SelectedStorage)
                    ? r.Storage == parameters.SelectedStorage.ToEnum<Storage>()
                    : true)
                .OrderByDescending(r => r.Storage)
                .ThenBy(r => r.Surname);

            var count = 0;
            parameters.RecruitShortUIModels.Clear();

            foreach (var recruit in filteredRecruits)
            {
                recruit.Number = (++count).ToString();

                parameters.RecruitShortUIModels.Add(recruit);
            }

            parameters.StateChanged.OnStateChanged(CommandSuccess + (string.IsNullOrWhiteSpace(parameters.SelectedStorage) ? string.Empty : $" ({parameters.SelectedStorage})"), StateResult.Success);
        }
    }
}
