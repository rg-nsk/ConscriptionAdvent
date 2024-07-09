using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Presentation.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ConscriptionAdvent.Domain.Enums;
using System;
using ConscriptionAdvent.Domain.ExtensionMethods.EnumExtensions;
using System.Linq;

namespace ConscriptionAdvent.Presentation.Models.Cards
{
    public class SettingsCard : UIModel, IDataErrorInfo
    {
        public const string SqliteLocalFilePathFieldName = "Путь до базы SQLite";
        public const string FirebirdLocalFilePathFieldName = "Путь до базы Firebird";
        public const string PersonalPhotoDirectoryPathFieldName = "Путь до папки с фотографиями";
        public const string ImportDirectoryPathFieldName = "Путь до папки с файлами для импорта";
        public const string ExportDirectoryPathFieldName = "Путь до папки с файлами для экспорта";
        public const string ExportTemplateFilePathFieldName = "Файл шаблона для экспорта алфавитки";
        public const string ExportTableTemplateFilePathFieldName = "Файл шаблона для экспорта таблицы призывников";
        public const string SelectThemeFieldName = "Выбор темы оформления";

        public static IEnumerable<string> ThemesEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(Themes)).Cast<Themes>()
                    .Select(theme => theme.ToThemeString());
            }
        }

        private string _sqliteLocalFilePath;
        public string SqliteLocalFilePath
        {
            get { return _sqliteLocalFilePath; }
            set
            {
                if (_sqliteLocalFilePath == value) return;
                _sqliteLocalFilePath = value;
                OnPropertyChanged();
            }
        }

        private string _firebirdLocalFilePath;
        public string FirebirdLocalFilePath
        {
            get { return _firebirdLocalFilePath; }
            set
            {
                if (_firebirdLocalFilePath == value) return;
                _firebirdLocalFilePath = value;
                OnPropertyChanged();
            }
        }

        private string _personalPhotoDirectoryPath;
        public string PersonalPhotoDirectoryPath
        {
            get { return _personalPhotoDirectoryPath; }
            set
            {
                if (_personalPhotoDirectoryPath == value) return;
                _personalPhotoDirectoryPath = value;
                OnPropertyChanged();
            }
        }

        private string _importDirectoryPath;
        public string ImportDirectoryPath
        {
            get { return _importDirectoryPath; }
            set
            {
                if (_importDirectoryPath == value) return;
                _importDirectoryPath = value;
                OnPropertyChanged();
            }
        }

        private string _exportDirectoryPath;
        public string ExportDirectoryPath
        {
            get { return _exportDirectoryPath; }
            set
            {
                if (_exportDirectoryPath == value) return;
                _exportDirectoryPath = value;
                OnPropertyChanged();
            }
        }

        private string _exportTemplateFilePath;
        public string ExportTemplateFilePath
        {
            get { return _exportTemplateFilePath; }
            set
            {
                if (_exportTemplateFilePath == value) return;
                _exportTemplateFilePath = value;
                OnPropertyChanged();
            }
        }

        private string _exportTableTemplateFilePath;
        public string ExportTableTemplateFilePath
        {
            get { return _exportTableTemplateFilePath; }
            set
            {
                if (_exportTableTemplateFilePath == value) return;
                _exportTableTemplateFilePath = value;
                OnPropertyChanged();
            }
        }

        private string _themeValue;
        public string ThemeValue
        {
            get { return _themeValue; }
            set
            {
                if (_themeValue == value) return;
                _themeValue = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(PersonalPhotoDirectoryPath):
                        {
                            if (!Directory.Exists(PersonalPhotoDirectoryPath))
                            {
                                return string.Format(ErrorConstants.FieldShouldContainsExistsPath,
                                    PersonalPhotoDirectoryPathFieldName);
                            }

                            break;
                        }
                    case nameof(ImportDirectoryPath):
                        {
                            if (!Directory.Exists(ImportDirectoryPath))
                            {
                                return string.Format(ErrorConstants.FieldShouldContainsExistsPath,
                                    ImportDirectoryPathFieldName);
                            }

                            break;
                        }
                    case nameof(ExportDirectoryPath):
                        {
                            if (!Directory.Exists(ExportDirectoryPath))
                            {
                                return string.Format(ErrorConstants.FieldShouldContainsExistsPath,
                                    ExportDirectoryPathFieldName);
                            }

                            break;
                        }
                    case nameof(ExportTemplateFilePath):
                        {
                            if (!File.Exists(ExportTemplateFilePath))
                            {
                                return string.Format(ErrorConstants.FieldShouldContainsExistsPath,
                                    ExportTemplateFilePathFieldName);
                            }

                            break;
                        }
                    case nameof(ExportTableTemplateFilePath):
                        {
                            if (!File.Exists(ExportTableTemplateFilePath))
                            {
                                return string.Format(ErrorConstants.FieldShouldContainsExistsPath,
                                    ExportTableTemplateFilePathFieldName);
                            }

                            break;
                        }
                    case nameof(ThemeValue):
                        {
                            if (string.IsNullOrWhiteSpace(ThemeValue))
                            {
                                return string.Format(ErrorConstants.FieldShouldBeNotEmpty,
                                    SelectThemeFieldName);
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
                    this[nameof(PersonalPhotoDirectoryPath)],
                    this[nameof(ImportDirectoryPath)],
                    this[nameof(ExportDirectoryPath)],
                    this[nameof(ExportTemplateFilePath)],
                    this[nameof(ExportTableTemplateFilePath)],
                    this[nameof(SelectThemeFieldName)]
                };

                errors.RemoveAll(e => string.IsNullOrWhiteSpace(e));

                return string.Join(SeparatorConstants.CommaSeparator, errors);
            }
        }
    }
}
