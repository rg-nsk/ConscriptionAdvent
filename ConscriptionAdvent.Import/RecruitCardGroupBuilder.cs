using ConscriptionAdvent.Domain.DomainModels.Common;
using ConscriptionAdvent.Domain.DomainModels.Military;
using ConscriptionAdvent.Domain.DomainModels.Passport;
using ConscriptionAdvent.Domain.Enums;
using ConscriptionAdvent.Domain.ExtensionMethods.EnumExtensions;
using ConscriptionAdvent.Import.Constants;
using ConscriptionAdvent.Presentation.Models.CardGroups;
using ConscriptionAdvent.Presentation.Models.Cards;
using System;
using System.Collections.ObjectModel;

namespace ConscriptionAdvent.Import
{
    public class RecruitCardGroupBuilder
    {
        private const string _defaultOccupation = "Не работает и не учится";

        private readonly string _personalPhotoDirectoryPath;

        private readonly string _rcp;
        private readonly string[] _words;

        public RecruitCardGroupBuilder(string personalPhotoDirectoryPath,
            string rcp, string[] words)
        {
            if (string.IsNullOrWhiteSpace(personalPhotoDirectoryPath))
            {
                throw new ArgumentNullException(nameof(personalPhotoDirectoryPath));
            }

            if (rcp == null)
            {
                throw new ArgumentNullException(nameof(rcp));
            }

            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            if (words.Length != PupaRecruitImporter.RecruitWordsCount)
            {
                throw new ArgumentException(nameof(words));
            }
            
            _personalPhotoDirectoryPath = personalPhotoDirectoryPath;
            _rcp = rcp;
            _words = words;
        }

        public RecruitCardGroup Build()
        {
            string metaInfo = string.Join("\n", _words);

            var serviceCard = CreateServiceCard();
            var firstCardGroup = CreateFirstCardGroup();
            var secondCardGroup = CreateSecondCardGroup();
            var thirdCardGroup = CreateThirdCardGroup();

            return new RecruitCardGroup(serviceCard, firstCardGroup, secondCardGroup, thirdCardGroup, metaInfo);
        }

        #region Service Card

        private ServiceCard CreateServiceCard()
        {
            return new ServiceCard() { RegionalCollectionPoint = _rcp };
        }

        #endregion

        #region First Card

        private FirstCardGroup CreateFirstCardGroup()
        {
            var passportInfoCard = CreatePassportInfoCard();
            var passportPersonInfoCard = CreatePassportPersonInfoCard();
            var passportAccommodationCard = CreatePassportAccommodationCard();
            var passportFamilyInfoCard = CreatePassportFamilyInfoCard();
            var criminalCard = CreateCriminalCard();

            return new FirstCardGroup(passportInfoCard,
                passportPersonInfoCard,
                passportAccommodationCard,
                passportFamilyInfoCard,
                criminalCard);
        }

        private PassportInfoCard CreatePassportInfoCard()
        {
            DateTime outIssueDate;
            DateTime? issueDate = DateTime.TryParse(_words[8], out outIssueDate)
                ? outIssueDate
                : (DateTime?)null;

            return new PassportInfoCard()
            {
                Code = _words[7],
                IssueBy = _words[9],
                IssueDate = issueDate,
                DevisionCode = _words[63].Split(new string[] { Environment.NewLine },
                    StringSplitOptions.None)[0]
            };
        }

        private PassportPersonInfoCard CreatePassportPersonInfoCard()
        {
            DateTime outBirthDate;
            DateTime? birthDate = DateTime.TryParse(_words[4], out outBirthDate)
                ? outBirthDate
                : (DateTime?)null;

            string birthPlace = string.IsNullOrWhiteSpace(_words[35]) ? string.Empty : _words[35].Replace(",", "");

            return new PassportPersonInfoCard(_personalPhotoDirectoryPath)
            {
                PhotoName = _words[51],
                Surname = _words[1],
                Name = _words[2],
                Patronymic = _words[3],
                BirthDate = birthDate,
                BirthPlace = birthPlace
            };
        }

        private PassportAccommodationCard CreatePassportAccommodationCard()
        {
            string registerLocation = string.IsNullOrWhiteSpace(_words[37]) ? string.Empty : _words[37].Replace("НСО", "Новосибирская область");
            string actuallyLocation = string.IsNullOrWhiteSpace(_words[38]) ? string.Empty : _words[38].Replace("НСО", "Новосибирская область");

            return new PassportAccommodationCard()
            {
                RegisterLocation = registerLocation,
                ActuallyLocation = actuallyLocation
            };
        }

        private PassportFamilyInfoCard CreatePassportFamilyInfoCard()
        {
            return new PassportFamilyInfoCard()
            {
                FamilyStatus = _words[23],
                IsHaveBaby = _words[24] == PassportFamilyInfo.HaveBaby
            };
        }

        private CriminalCard CreateCriminalCard()
        {
            var criminalStatus = _words[21];
            //var registerStatus = _words[22];

            return new CriminalCard()
            {
                //RegisterStatus = !string.IsNullOrWhiteSpace(registerStatus) ? registerStatus : RegisterStatus.WasNot.ToRegisterStatusString(),
                RegisterStatus = RegisterStatus.WasNot.ToRegisterStatusString(),
                CriminalStatus = !string.IsNullOrWhiteSpace(criminalStatus) ? criminalStatus : CriminalStatus.HaveNot.ToCriminalStatusString()
            };
        }

        #endregion

        #region Second Card

        private SecondCardGroup CreateSecondCardGroup()
        {
            var militaryDocumentCard = CreateMilitaryDocumentCard();
            var proficiencyCard = CreateProficiencyCard();
            var driverCard = CreateDriverCard();
            var distributionCard = CreateDistributionCard();
            var civilCard = CreateCivilCard();

            return new SecondCardGroup(militaryDocumentCard,
                proficiencyCard,
                driverCard,
                distributionCard,
                civilCard);
        }

        private MilitaryDocumentCard CreateMilitaryDocumentCard()
        {
            int serieLength = 2;
            var personalNumber = !string.IsNullOrWhiteSpace(_words[5])
                ? _words[5].Insert(serieLength, ControlChars.Space.ToString()).Trim(' ').Replace("-", "") : string.Empty;
            string militaryBillet = !string.IsNullOrWhiteSpace(_words[6]) && _words[6].Trim(' ') != "0" ? _words[6].Trim(' ') : string.Empty;

            var militaryDocumentCard = new MilitaryDocumentCard()
            {
                PersonalNumber = personalNumber,
                MilitaryBilletCode = militaryBillet
            };

            bool isHaveSecretAccess = !string.IsNullOrWhiteSpace(_words[14]) && _words[14].Replace(" ", "") != "*" && _words[14].Replace(" ", "") != "-";
            if (isHaveSecretAccess)
            {
                militaryDocumentCard.IsHaveSecretAccess = isHaveSecretAccess;
                militaryDocumentCard.AccessForm = _words[14];
                
                string accessNumber = _words[15].Replace(" ", "");

                if (!string.IsNullOrWhiteSpace(accessNumber))
                {
                    militaryDocumentCard.SecretAccessNumber = accessNumber.Substring(accessNumber.Length - 5) ?? accessNumber;
                }
                else
                    militaryDocumentCard.SecretAccessNumber = string.Empty;

                DateTime outIssueDate;
                militaryDocumentCard.SecretAccessIssueDate = DateTime.TryParse(_words[16], out outIssueDate)
                    ? outIssueDate
                    : (DateTime?)null;
            }

            return militaryDocumentCard;
        }

        private ProficiencyCard CreateProficiencyCard()
        {
            return new ProficiencyCard()
            {
                ProficiencyCategory = _words[17] == "Технологические" ? "Технические" : _words[17],
                OfficialStatus = _words[49],
                NervouslyPsychologicalStability = _words[18],
                GeneralPsychologicalStability = _words[48]
            };
        }

        private DriverCard CreateDriverCard()
        {
            var driverCard = new DriverCard();
            driverCard.IsDriver = !string.IsNullOrWhiteSpace(_words[10]) && _words[10].Trim(' ') != "0";

            if (driverCard.IsDriver)
            {
                driverCard.DriverLicenseCode = _words[10];

                DateTime outIssueDate;
                driverCard.DriverLicenseIssueDate = DateTime.TryParse(_words[42], out outIssueDate)
                    ? outIssueDate
                    : (DateTime?)null;
            }

            return driverCard;
        }

        private DistributionCard CreateDistributionCard()
        {
            string teamMode = !string.IsNullOrWhiteSpace(_words[13]) ? _words[13].Trim(' ') : "НЕРЕЖ.";

            return new DistributionCard()
            {
                Speciality = null, //_words[11]
                TeamMode = teamMode
            };
        }

        private CivilCard CreateCivilCard()
        {
            OccupationStatus _occupation = OccupationStatusExtensions.ToOccupationStatusEnum(_words[25]);
            OccupationStatus occupationEdit = _occupation == OccupationStatus.None ? OccupationStatus.NotWorkNotStudy : _occupation;

            if (_words[19].ToEducationStatusEnum() == EducationStatus.SecondaryVocational
                && occupationEdit == OccupationStatus.NotWorkNotStudy ||
                _words[19].ToEducationStatusEnum() == EducationStatus.HigherVocational
                && occupationEdit == OccupationStatus.NotWorkNotStudy)
                occupationEdit = OccupationStatus.StudyInEducationInstitution;

            return new CivilCard()
            {
                Education = _words[19],
                Profession = _words[12],
                Occupation = occupationEdit.ToOccupationStatusString()
            };
        }

        #endregion

        #region Third Card

        private ThirdCardGroup CreateThirdCardGroup()
        {
            var medicineCard = CreateMedicineCard();
            var physiologicalCharacteristicsCard = CreatePhysiologicalCharacteristicsCard();
            var sportCard = CreateSportCard();
            var contactsCard = CreateContactsCard();
            var familyCard = CreateFamilyCard();

            return new ThirdCardGroup(medicineCard,
                physiologicalCharacteristicsCard,
                sportCard,
                contactsCard,
                familyCard);
        }

        private MedicineCard CreateMedicineCard()
        {

            return new MedicineCard()
            {
                Rank = _words[26],
                AdditionalRequirementsTable = _words[27],
                DiseaseArticles = "", //_words[31]
                Vision = _words[28],
                BloodType = _words[41],
                VaccinationType = VaccinationType.Otkaz.ToVaccinationTypeString(),
                VaccinationDate = (DateTime?)null
            };
        }

        private PhysiologicalCharacteristicsCard CreatePhysiologicalCharacteristicsCard()
        {
            int.TryParse(_words[29], out int height);
            int.TryParse(_words[30], out int weight);
            

            return new PhysiologicalCharacteristicsCard()
            {
                Height = string.IsNullOrWhiteSpace(_words[29]) || height == 0 ? string.Empty : height.ToString(),
                Weight = string.IsNullOrWhiteSpace(_words[30]) || weight == 0 ? string.Empty : weight.ToString(),
                HeadSize = _words[32],
                ClothingSize = _words[33],
                ShoesSize = _words[34]
            };
        }

        private SportCard CreateSportCard()
        {
            return new SportCard()
            {
                Rank = SportRank.HaveNot.ToSportRankString(),
                Kind = ""
            };
        }

        private ContactsCard CreateContactsCard()
        {
            return new ContactsCard()
            {
                MobilePhone = NormalizePhoneNumber(_words[39]),
                HomePhone = NormalizePhoneNumber(_words[40])
            };
        }

        private string NormalizePhoneNumber(string phoneNumber)
        {
            string result = phoneNumber;

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;
            else
            {
                if(result.Length > 11)
                    result = phoneNumber.Substring(result.Length-11);
                result = result.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "");

                if (result.StartsWith("7"))
                    result = "8" + result.Remove(0, 1);
                if (result.Length == 10)
                    result = "8" + result;
                if (result.Length == 9)
                    result = "89" + result;
            }

            return result;
        }

        private FamilyCard CreateFamilyCard()
        {
            var relativeInfoUIModels = new ObservableCollection<RelativeInfoCard>();

            if (!string.IsNullOrWhiteSpace(_words[43]))
            {
                string birthDate = _words[45].Replace(" ", "");
                if (birthDate.Length == 4)
                    birthDate = "01.01." + birthDate;

                DateTime outParentBirthDate;
                DateTime? parentBirthDate = DateTime.TryParse(birthDate, out outParentBirthDate)
                    ? outParentBirthDate
                    : (DateTime?)null;

                var birthPlace = _words[46];
                var workPlace = _words[47];

                relativeInfoUIModels.Add(new RelativeInfoCard()
                {
                    RelativeStatus = _words[43],
                    FullName = _words[44],
                    BirthDate = parentBirthDate,
                    BirthPlace = !string.IsNullOrWhiteSpace(birthPlace) ? birthPlace : BirthInfo.UnknownPlace,
                    WorkPlace = !string.IsNullOrWhiteSpace(workPlace) ? workPlace : RelativeInfo.NotWorking
                });
            }

            if (!string.IsNullOrWhiteSpace(_words[53]))
            {
                string birthDate = _words[55].Replace(" ", "");
                if (birthDate.Length == 4)
                    birthDate = "01.01." + birthDate;

                DateTime outParentBirthDate;
                DateTime? parentBirthDate = DateTime.TryParse(birthDate, out outParentBirthDate)
                    ? outParentBirthDate
                    : (DateTime?)null;

                var birthPlace = _words[56];
                var workPlace = _words[57];

                relativeInfoUIModels.Add(new RelativeInfoCard()
                {
                    RelativeStatus = _words[53],
                    FullName = _words[54],
                    BirthDate = parentBirthDate,
                    BirthPlace = !string.IsNullOrWhiteSpace(birthPlace) ? birthPlace : BirthInfo.UnknownPlace,
                    WorkPlace = !string.IsNullOrWhiteSpace(workPlace) ? workPlace : RelativeInfo.NotWorking
                });
            }

            if (!string.IsNullOrWhiteSpace(_words[58]))
            {
                string birthDate = _words[60].Replace(" ", "");
                if (birthDate.Length == 4)
                    birthDate = "01.01." + birthDate;

                DateTime outParentBirthDate;
                DateTime? parentBirthDate = DateTime.TryParse(birthDate, out outParentBirthDate)
                    ? outParentBirthDate
                    : (DateTime?)null;

                var birthPlace = _words[61];
                var workPlace = _words[62];

                relativeInfoUIModels.Add(new RelativeInfoCard()
                {
                    RelativeStatus = _words[58],
                    FullName = _words[59],
                    BirthDate = parentBirthDate,
                    BirthPlace = !string.IsNullOrWhiteSpace(birthPlace) ? birthPlace : BirthInfo.UnknownPlace,
                    WorkPlace = !string.IsNullOrWhiteSpace(workPlace) ? workPlace : RelativeInfo.NotWorking
                });
            }

            return new FamilyCard()
            {
                ParentFamilyStatus = _words[36],
                RelativeInfoUIModels = relativeInfoUIModels,
                SelectedRelativeInfoUIModel = relativeInfoUIModels.Count > 0
                    ? relativeInfoUIModels[0]
                    : null
            };
        }

        #endregion
    }
}
