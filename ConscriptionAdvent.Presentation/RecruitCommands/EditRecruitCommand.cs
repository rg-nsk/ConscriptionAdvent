﻿using ConscriptionAdvent.Domain.DomainModels;
using ConscriptionAdvent.Domain.Interfaces;
using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.Interfaces;
using ConscriptionAdvent.Presentation.RecruitCommands.Parameters;
using System;
using System.Threading.Tasks;

namespace ConscriptionAdvent.Presentation.RecruitCommands
{
    public class EditRecruitCommand : IParameterizedCommandAsync<EditRecruitCommandParameters>
    {
        public const string CommandName = "Редактирование призывника";
        public const string CommandSuccess = "Призывник обновлен";

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRecruitInfoRepository _recruitInfoRepository;
        private readonly IEventService _eventService;

        public EditRecruitCommand(IUnitOfWorkFactory unitOfWorkFactory,
            IRecruitInfoRepository recruitInfoRepository,
            IEventService eventService)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }

            if (recruitInfoRepository == null)
            {
                throw new ArgumentNullException(nameof(recruitInfoRepository));
            }

            if (eventService == null)
            {
                throw new ArgumentNullException(nameof(eventService));
            }

            _unitOfWorkFactory = unitOfWorkFactory;
            _recruitInfoRepository = recruitInfoRepository;
            _eventService = eventService;
        }

        public async Task ExecuteAsync(EditRecruitCommandParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                try
                {
                    var message = $"{CommandSuccess} - {parameters.RecruitInfo.Envelope.PassportInfo.PersonInfo.FullName.Value}";

                    await _recruitInfoRepository.ChangeAsync(parameters.RecruitInfo);
                    _eventService.Fire(message);

                    unitOfWork.Commit();
                    parameters.StateChanged.OnStateChanged(message, StateResult.Success);
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();

                    throw ex;
                }
            }
        }
    }
}
