using ConscriptionAdvent.Domain.Enums;
using ConscriptionAdvent.Domain.ExtensionMethods.EnumExtensions;
using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Presentation.Abstract;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ConscriptionAdvent.Presentation.Commands;
using ConscriptionAdvent.Domain.DomainModels.Medicine;

namespace ConscriptionAdvent.Presentation.Models.Cards
{
    public class MedicineCard : UIModel, IDataErrorInfo
    {
        public const string RankFieldName = "Категория годности";
        public const string AdditionalRequirementsTableFieldName = "Графы таблицы дополнительных требований";
        public const string DiseaseArticlesFieldName = "Статьи расписания болезней";
        public const string VisionFieldName = "Зрение";
        public const string BloodTypeFieldName = "Группа крови";
        public const string VaccinationTypeFieldName = "Тип вакцины";
        public const string VaccinationDateFieldName = "Дата";

        private Regex _additionalRequirementsTableRegex = new Regex(RegexConstants.AdditionalRequirementsTablePattern);
        private Regex _diseaseArticlesRegex = new Regex(RegexConstants.DiseaseArticlesPattern);

        private const string VisionExample = "1,0/1,0 или 1.0/1.0";

        private Regex _visionRegex = new Regex(RegexConstants.VisionPattern);

        public static IEnumerable<string> MedicineRankEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(MedicineRank)).Cast<MedicineRank>()
                    .Select(mr => mr.ToMedicineRankString());
            }
        }

        public static IEnumerable<string> BloodTypeEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(BloodType)).Cast<BloodType>()
                    .Select(bt => bt.ToBloodTypeString());
            }
        }

        public static IEnumerable<string> VaccinationTypeEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(VaccinationType)).Cast<VaccinationType>()
                    .Select(vt => vt.ToVaccinationTypeString());
            }
        }

        private string _rank;
        public string Rank
        {
            get { return _rank; }
            set
            {
                if (_rank == value) return;
                _rank = value;
                OnPropertyChanged();
            }
        }

        private string _additionalRequirementsTable;
        public string AdditionalRequirementsTable
        {
            get { return _additionalRequirementsTable; }
            set
            {
                if (_additionalRequirementsTable == value) return;
                _additionalRequirementsTable = value;
                OnPropertyChanged();
            }
        }

        private string _diseaseArticles;
        public string DiseaseArticles
        {
            get { return _diseaseArticles; }
            set
            {
                if (_diseaseArticles == value) return;
                _diseaseArticles = value;
                OnPropertyChanged();
            }
        }

        private string _vision;
        public string Vision
        {
            get { return _vision; }
            set
            {
                if (_vision == value) return;
                _vision = value;
                OnPropertyChanged();
            }
        }

        private string _bloodType;
        public string BloodType
        {
            get { return _bloodType; }
            set
            {
                if (_bloodType == value) return;
                _bloodType = value;
                OnPropertyChanged();
            }
        }

        private string _vaccinationType;
        public string VaccinationType
        {
            get { return _vaccinationType; }
            set
            {
                if (_vaccinationType == value) return;
                _vaccinationType = value;
                switch (value)
                {
                    case "К":
                    case "К-1":
                    case "К-2":
                        IsHaveVaccinationType = true;
                        break;
                    default:
                        IsHaveVaccinationType = false;
                        break;
                }
                OnPropertyChanged();
            }
        }

        private DateTime? _vaccinationDate;
        public DateTime? VaccinationDate
        {
            get { return _vaccinationDate; }
            set
            {
                if (_vaccinationDate == value) return;
                _vaccinationDate = value;
                OnPropertyChanged();
            }
        }

        private bool _isHaveVaccinationType;
        public bool IsHaveVaccinationType
        {
            get { return _isHaveVaccinationType; }
            set
            {
                if (_isHaveVaccinationType == value)
                {
                    return;
                }
                _isHaveVaccinationType = value;

                if (_isHaveVaccinationType)
                {
                    VaccinationDate = DateTime.Now;
                }
                else
                {
                    VaccinationDate = null;
                }

                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Rank):
                        {
                            if (string.IsNullOrWhiteSpace(Rank))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    RankFieldName);
                            }

                            break;
                        }
                    case nameof(AdditionalRequirementsTable):
                        {
                            if (string.IsNullOrWhiteSpace(AdditionalRequirementsTable))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    AdditionalRequirementsTableFieldName);
                            }

                            if (!_additionalRequirementsTableRegex.IsMatch(AdditionalRequirementsTable))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeAdditionalRequirementsTable,
                                    AdditionalRequirementsTableFieldName);
                            }

                            break;
                        }
                    case nameof(DiseaseArticles):
                        {
                            if (!string.IsNullOrWhiteSpace(DiseaseArticles) &&
                                !_diseaseArticlesRegex.IsMatch(DiseaseArticles))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeDiseaseArticles,
                                    DiseaseArticlesFieldName);
                            }

                            break;
                        }
                    case nameof(Vision):
                        {
                            if (string.IsNullOrWhiteSpace(Vision))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    VisionFieldName);
                            }

                            if (!_visionRegex.IsMatch(Vision))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeCorrectFormatWithExample,
                                    VisionFieldName, VisionExample);
                            }

                            break;
                        }
                    case nameof(VaccinationType):
                        {
                            if (string.IsNullOrWhiteSpace(VaccinationType))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    VaccinationTypeFieldName);
                            }

                            break;
                        }
                    case nameof(VaccinationDate):
                        {
                            if (IsHaveVaccinationType && !VaccinationDate.HasValue)
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    VaccinationDateFieldName);
                            }

                            break;
                        }
                }

                return string.Empty;
            }
        }

        public string Error
        {
            get
            {
                var errors = new List<string>()
                {
                    this[nameof(Rank)],
                    this[nameof(AdditionalRequirementsTable)],
                    this[nameof(DiseaseArticles)],
                    this[nameof(Vision)],
                    this[nameof(VaccinationType)],
                    this[nameof(VaccinationDate)]
                };

                if (IsHaveVaccinationType)
                {
                    errors.Add(this[nameof(VaccinationDate)]);
                }

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _resetAllMedicineCommand;
        public ICommand ResetAllMedicineCommand
        {
            get
            {
                return _resetAllMedicineCommand ?? (_resetAllMedicineCommand = new ActionCommand(vm =>
                {
                    if (!MedicineRankEnumValues.Contains(Rank) || Rank == MedicineRankExtensions.ToMedicineRankString(MedicineRank.None))
                        Rank = MedicineRankExtensions.ToMedicineRankString(MedicineRank.A1);
                    DiseaseArticles = "";

                    if(string.IsNullOrWhiteSpace(AdditionalRequirementsTable))
                        AdditionalRequirementsTable = Health.DefaultAdditionalRequirementsTableGraphs;

                    if (string.IsNullOrWhiteSpace(Vision) || !_visionRegex.IsMatch(Vision))
                        Vision = Health.DefaultVision;

                    VaccinationType = VaccinationTypeExtensions.ToVaccinationTypeString(Domain.Enums.VaccinationType.Otkaz);
                }));
            }
        }
    }
}
