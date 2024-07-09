using ConscriptionAdvent.Domain.DomainModels.Civil;
using ConscriptionAdvent.Domain.Interfaces;
using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.EventArguments;
using ConscriptionAdvent.Presentation.Mappers;
using ConscriptionAdvent.Presentation.Models;
using ConscriptionAdvent.Presentation.Models.CardGroups;
using ConscriptionAdvent.Presentation.Models.Cards;
using ConscriptionAdvent.Presentation.RecruitCommands;
using ConscriptionAdvent.Presentation.Commands;
using System;
using System.Windows.Input;
using ConscriptionAdvent.Presentation.Interfaces;
using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Domain.DomainModels;
using ConscriptionAdvent.Presentation.RecruitCommands.Parameters;

namespace ConscriptionAdvent.Presentation.ViewModels
{
    public class RecruitViewModel : BaseViewModel
    {
        private readonly IParameterizedCommandAsync<SaveRecruitCommandParameters> _saveParameterizedRecruitCommand;

        private readonly RecruitOperationEventArgs _recruitOperationEventArgs;
        private readonly Action<string> _notValidCallback;

        public event EventHandler<RecruitOperationEventArgs> RecruitSaved;
        public void OnRecruitSaved(RecruitOperationEventArgs recruitOperationEventArgs)
        {
            var recruitOperation = recruitOperationEventArgs.RecruitOperation;
            var recruitShortUIModel = recruitOperationEventArgs.RecruitShortUIModel;

            RecruitSaved?.Invoke(this, new RecruitOperationEventArgs(recruitOperation, recruitShortUIModel));
        }

        public RecruitCardGroup RecruitCardGroup { get; }

        public RecruitViewModel(IRecruitCardGroupFactory recruitCardGroupFactory,
            IParameterizedCommandAsync<SaveRecruitCommandParameters> saveParameterizedRecruitCommand,
            RecruitOperationEventArgs recruitOperationEventArgs, 
            Action<string> notValidCallback)
        {
            if (recruitCardGroupFactory == null)
            {
                throw new ArgumentNullException(nameof(recruitCardGroupFactory));
            }

            if (saveParameterizedRecruitCommand == null)
            {
                throw new ArgumentNullException(nameof(saveParameterizedRecruitCommand));
            }

            if (recruitOperationEventArgs == null)
            {
                throw new ArgumentNullException(nameof(recruitOperationEventArgs));
            }

            if (notValidCallback == null)
            {
                throw new ArgumentNullException(nameof(notValidCallback));
            }

            _saveParameterizedRecruitCommand = saveParameterizedRecruitCommand;
            _recruitOperationEventArgs = recruitOperationEventArgs;
            _notValidCallback = notValidCallback;

            RecruitCardGroup = recruitCardGroupFactory.Create(_recruitOperationEventArgs);
        }

        private ICommand _saveRecruitCommand;
        public ICommand SaveRecruitCommand
        {
            get
            {
                return _saveRecruitCommand ?? (_saveRecruitCommand = new AsyncCommand(async vm =>
                {
                    if (!IsValid)
                    {
                        _notValidCallback(RecruitCardGroup.Error);
                        return;
                    }
                    
                    var parameters = new SaveRecruitCommandParameters(_recruitOperationEventArgs, 
                        RecruitCardGroup, 
                        this);

                    await _saveParameterizedRecruitCommand.ExecuteAsync(parameters);

                    OnRecruitSaved(_recruitOperationEventArgs);
                }, 
                this));
            }
        }

        private bool IsValid
        {
            get { return RecruitCardGroup.IsValid; }
        }
    }
}
