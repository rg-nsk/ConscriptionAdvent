using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Commands;
using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.Models.Cards;
using ConscriptionAdvent.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace ConscriptionAdvent.Presentation.Models.CardGroups
{
    public class RecruitCardGroup : IValidCardGroup
    {
        public ServiceCard ServiceCard { get; }
        public FirstCardGroup FirstCardGroup { get; }
        public SecondCardGroup SecondCardGroup { get; }
        public ThirdCardGroup ThirdCardGroup { get; }
        public string MetaInfo { get; }

        public RecruitCardGroup(string personalPhotoDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            ServiceCard = new ServiceCard();
            FirstCardGroup = new FirstCardGroup(personalPhotoDirectoryPath);
            SecondCardGroup = new SecondCardGroup();
            ThirdCardGroup = new ThirdCardGroup();
        }

        public RecruitCardGroup(ServiceCard serviceCard,
            FirstCardGroup firstCardGroup,
            SecondCardGroup secondCardGroup,
            ThirdCardGroup thirdCardGroup, string metaInfo = null)
        {
            if (serviceCard == null)
            {
                throw new ArgumentNullException(nameof(serviceCard));
            }

            if (firstCardGroup == null)
            {
                throw new ArgumentNullException(nameof(firstCardGroup));
            }

            if (secondCardGroup == null)
            {
                throw new ArgumentNullException(nameof(secondCardGroup));
            }

            if (thirdCardGroup == null)
            {
                throw new ArgumentNullException(nameof(thirdCardGroup));
            }

            ServiceCard = serviceCard;
            FirstCardGroup = firstCardGroup;
            SecondCardGroup = secondCardGroup;
            ThirdCardGroup = thirdCardGroup;
            MetaInfo = metaInfo;
        }

        private ICommand _showMetaInfoCommand;
        public ICommand ShowMetaInfoCommand
        {
            get
            {
                return _showMetaInfoCommand ?? (_showMetaInfoCommand = new ActionCommand(vm =>
                {
                Console.WriteLine(MetaInfo);
                }));
            }
        }

        public bool IsValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(ServiceCard.Error) &&
                       FirstCardGroup.IsValid &&
                       SecondCardGroup.IsValid &&
                       ThirdCardGroup.IsValid;
            }
        }

        public string Error
        {
            get
            {
                var errors = new List<string>()
                {
                    ServiceCard.Error,
                    FirstCardGroup.Error,
                    SecondCardGroup.Error,
                    ThirdCardGroup.Error,
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
