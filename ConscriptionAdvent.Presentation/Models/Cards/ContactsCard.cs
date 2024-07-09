using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Presentation.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ConscriptionAdvent.Presentation.Commands;
using System;

namespace ConscriptionAdvent.Presentation.Models.Cards
{
    public class ContactsCard : UIModel, IDataErrorInfo
    {
        public const string HomePhoneFieldName = "Домашний телефон";
        public const string MobilePhoneFieldName = "Мобильный телефон";

        private string _homePhone;
        public string HomePhone
        {
            get { return _homePhone; }
            set
            {
                if (_homePhone == value) return;
                _homePhone = value;
                OnPropertyChanged();
            }
        }

        private string _mobilePhone;
        public string MobilePhone
        {
            get { return _mobilePhone; }
            set
            {
                if (_mobilePhone == value) return;
                _mobilePhone = value;
                OnPropertyChanged();
            }
        }
        
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(HomePhone):
                        {
                            if (string.IsNullOrWhiteSpace(HomePhone))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    HomePhoneFieldName);
                            }

                            break;
                        }
                    case nameof(MobilePhone):
                        {
                            if (string.IsNullOrWhiteSpace(MobilePhone))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    MobilePhoneFieldName);
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
                    this[nameof(HomePhone)],
                    this[nameof(MobilePhone)]
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }

        private ICommand _upPhoneNumberCommand;
        public ICommand UpPhoneNumberCommand
        {
            get
            {
                return _upPhoneNumberCommand ?? (_upPhoneNumberCommand = new ActionCommand(vm =>
                {
                    if (!string.IsNullOrWhiteSpace(MobilePhone))
                        HomePhone = MobilePhone;
                }));
            }
        }

        private ICommand _downPhoneNumberCommand;
        public ICommand DownPhoneNumberCommand
        {
            get
            {
                return _downPhoneNumberCommand ?? (_downPhoneNumberCommand = new ActionCommand(vm =>
                {
                    if(!string.IsNullOrWhiteSpace(HomePhone))
                        MobilePhone = HomePhone;
                }));
            }
        }

        private ICommand _randomizeNumbersCommand;
        public ICommand RandomizeNumbersCommand
        {
            get
            {
                return _randomizeNumbersCommand ?? (_randomizeNumbersCommand = new ActionCommand(vm =>
                {
                    //MobilePhone = HomePhone;
                    string randomNumber = randomPhoneNumber();
                    MobilePhone = randomNumber;
                    HomePhone = randomNumber;
                }));
            }
        }

        private string randomPhoneNumber()
        {
            var rand = new Random();
            string result = "89";

            switch (rand.Next(0,3))
            {
                case 0:
                    result += "23";
                    break;
                case 1:
                    result += "53";
                    break;
                case 2:
                    result += "13";
                    break;
                case 3:
                    result += "99";
                    break;
            }
            result += rand.Next(1000000, 9999999);
            return result;
        }
    }
}
