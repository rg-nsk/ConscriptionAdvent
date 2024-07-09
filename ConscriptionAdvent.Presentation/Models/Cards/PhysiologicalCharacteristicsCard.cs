using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Commands;
using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Presentation.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ConscriptionAdvent.Presentation.Models.Cards
{
    public class PhysiologicalCharacteristicsCard : UIModel, IDataErrorInfo
    {
        public const string HeightFieldName = "Рост";
        public const string WeightFieldName = "Вес";
        public const string HeadSizeFieldName = "Головной убор";
        public const string ClothingSizeFieldName = "Обхват/рост";
        public const string ShoesSizeFieldName = "Обувь";

        private Regex _physiologicalCharacteristicRegex = new Regex(RegexConstants.PhysiologicalCharacteristicPattern);
        private Regex _clothingSizeRegex = new Regex(RegexConstants.ClothingSizePattern);

        private const string ClothingSizeExample = "54/3";

        private string _height;
        public string Height
        {
            get { return _height; }
            set
            {
                if (_height == value) return;
                _height = value;
                OnPropertyChanged();
            }
        }

        private string _weight;
        public string Weight
        {
            get { return _weight; }
            set
            {
                if (_weight == value) return;
                _weight = value;
                OnPropertyChanged();
            }
        }

        private string _headSize;
        public string HeadSize
        {
            get { return _headSize; }
            set
            {
                if (_headSize == value) return;
                _headSize = value;
                OnPropertyChanged();
            }
        }

        private string _clothingSize;
        public string ClothingSize
        {
            get { return _clothingSize; }
            set
            {
                if (_clothingSize == value) return;
                _clothingSize = value;
                OnPropertyChanged();
            }
        }

        private string _shoesSize;
        public string ShoesSize
        {
            get { return _shoesSize; }
            set
            {
                if (_shoesSize == value) return;
                _shoesSize = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Height):
                        {
                            if (!string.IsNullOrWhiteSpace(Height) &&
                                !_physiologicalCharacteristicRegex.IsMatch(Height))
                            {
                                return string.Format(ErrorConstants.FieldShouldBePhysiologicalCharacteristic,
                                    HeightFieldName);
                            }

                            break;
                        }
                    case nameof(Weight):
                        {
                            if (!string.IsNullOrWhiteSpace(Weight) &&
                                !_physiologicalCharacteristicRegex.IsMatch(Weight))
                            {
                                return string.Format(ErrorConstants.FieldShouldBePhysiologicalCharacteristic,
                                    WeightFieldName);
                            }

                            break;
                        }
                    case nameof(HeadSize):
                        {
                            if (!string.IsNullOrWhiteSpace(HeadSize) &&
                                !_physiologicalCharacteristicRegex.IsMatch(HeadSize))
                            {
                                return string.Format(ErrorConstants.FieldShouldBePhysiologicalCharacteristic,
                                    HeadSizeFieldName);
                            }

                            break;
                        }

                    case nameof(ClothingSize):
                        {
                            if (!string.IsNullOrWhiteSpace(ClothingSize) &&
                                !_clothingSizeRegex.IsMatch(ClothingSize))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeCorrectFormatWithExample,
                                    ClothingSizeFieldName, ClothingSizeExample);
                            }

                            break;
                        }
                    case nameof(ShoesSize):
                        {
                            if (!string.IsNullOrWhiteSpace(ShoesSize) &&
                                !_physiologicalCharacteristicRegex.IsMatch(ShoesSize))
                            {
                                return string.Format(ErrorConstants.FieldShouldBePhysiologicalCharacteristic,
                                    ShoesSizeFieldName);
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
                    this[nameof(Height)],
                    this[nameof(Weight)],
                    this[nameof(HeadSize)],
                    this[nameof(ClothingSize)],
                    this[nameof(ShoesSize)],
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _resetAllPhysiologicalCommand;
        public ICommand ResetAllPhysiologicalCommand
        {
            get
            {
                return _resetAllPhysiologicalCommand ?? (_resetAllPhysiologicalCommand = new ActionCommand(vm =>
                {
                    if(string.IsNullOrWhiteSpace(Height)) 
                        Height = "170";
                    if (string.IsNullOrWhiteSpace(Weight))
                        Weight = "70";
                    if (string.IsNullOrWhiteSpace(HeadSize))
                        HeadSize = "56";
                    if (string.IsNullOrWhiteSpace(ClothingSize) || ClothingSize == "/")
                        ClothingSize = "52/3";
                    if (string.IsNullOrWhiteSpace(ShoesSize))
                        ShoesSize = "43";
                }));
            }
        }
    }
}
