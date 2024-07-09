using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Windows;
using ConscriptionAdvent.Presentation.Models;
using ConscriptionAdvent.Presentation.EventArguments;
using ConscriptionAdvent.Presentation.Enums;
using ConscriptionAdvent.Presentation.RecruitCommands;
using ConscriptionAdvent.Presentation.RecruitCommands.Parameters;
using ConscriptionAdvent.Presentation.Interfaces;
using ConscriptionAdvent.Presentation.Commands;
using ConscriptionAdvent.Presentation.Abstract;
using ConscriptionAdvent.Presentation.Constants;
using ConscriptionAdvent.Domain.DomainModels;
using ConscriptionAdvent.Domain.Interfaces;
using ConscriptionAdvent.Domain.Indexes;
using ConscriptionAdvent.Domain.ExtensionMethods;
using ConscriptionAdvent.Domain.Constants;
using ConscriptionAdvent.Domain.Enums;
using Ionic.Zip;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace ConscriptionAdvent.Presentation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const int MaxCharsCountInState = 250;

        #region Filter Names

        public const string ConscriptionDateFieldName = "Дата призыва";
        public const string RegionalCollectionPointsFieldName = "Военкомат";
        public const string SurnameFieldName = "Фамилия";

        #endregion

        private const string EmptyConscriptionDateOrRVKError = "Не заполнены поля даты призыва или военного комиссариата";
        public const string UpdateRecuitUIModelsCommandName = "Обновить";
        public const string ClearFiltersCommandName = "Очистить фильтры";

        public const string ClearLogCommandName = "Очистить вывод логов";
        public const string ClearLogSuccess = "Вывод логов очищен";
        public const string OpenFolderError = "Невозможно открыть папку";
        public static string Message;

        private readonly IParameterizedCommandAsync<ImportRecruitCommandParameters> _importRecruitCommand;
        private readonly IParameterizedCommandAsync<RemoveRecruitCommandParameters> _removeRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<TransmitRecruitCommandParameters> _transmitRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<ExportRecruitCommandParameters> _exportRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<ExportTableRecruitCommandParameters> _exportTableRecruitParameterizedCommand;
        private readonly IParameterizedCommandAsync<UpdateRecruitsCommandParameters> _updateRecruitsParameterizedCommand;
        private readonly string _importDirectoryPath;

        private readonly Func<bool> _messageCallback;

        public event EventHandler SettingsViewShowed;
        public void OnSettingsViewShowed()
        {
            SettingsViewShowed?.Invoke(this, new EventArgs());
        }

        public event EventHandler<RecruitOperationEventArgs> RecruitViewShowed;
        public void OnRecruitViewShowed(RecruitOperationEventArgs args)
        {
            RecruitViewShowed?.Invoke(this, args);
        }


        #region Filters

        private DateTime? _conscriptionDate;
        public DateTime? ConscriptionDate
        {
            get { return _conscriptionDate; }
            set
            {
                if (_conscriptionDate == value) return;
                _conscriptionDate = value;
                OnPropertyChanged();

                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }
        
        public IEnumerable<string> RegionalCollectionPoints { get; }

        private string _selectedRegionalCollectionPoint;
        public string SelectedRegionalCollectionPoint
        {
            get { return _selectedRegionalCollectionPoint; }
            set
            {
                if (_selectedRegionalCollectionPoint == value) return;
                _selectedRegionalCollectionPoint = value;
                OnPropertyChanged();

                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname == value) return;
                _surname = value;
                OnPropertyChanged();
                
                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }

        public IEnumerable<string> Storages { get; }

        private string _selectedStorage;
        public string SelectedStorage
        {
            get { return _selectedStorage; }
            set
            {
                if (_selectedStorage == value) 
                    return;
                _selectedStorage = value;
                OnPropertyChanged();
                UpdateRecuitUIModelsCommand.Execute(null);
            }
        }

        #endregion

        private ObservableCollection<RecruitShortUIModel> _recruitShortUIModels;
        public ObservableCollection<RecruitShortUIModel> RecruitShortUIModels
        {
            get { return _recruitShortUIModels; }
            set
            {
                if (_recruitShortUIModels == value) return;
                _recruitShortUIModels = value;
                OnPropertyChanged();
            }
        }

        private RecruitShortUIModel _selectedRecruitShortUIModel;
        public RecruitShortUIModel SelectedRecruitShortUIModel
        {
            get { return _selectedRecruitShortUIModel; }
            set
            {
                if (_selectedRecruitShortUIModel == value) return;
                _selectedRecruitShortUIModel = value;
                OnPropertyChanged();
            }
        }

        #region Loaders

        private bool _isImportingRecruits;
        public bool IsImportingRecruits
        {
            get { return _isImportingRecruits; }
            set
            {
                if (_isImportingRecruits == value) return;
                _isImportingRecruits = value;
                OnPropertyChanged();
            }
        }


        private bool _isUpdatingRecruitShortUIModels;
        public bool IsUpdatingRecruitShortUIModels
        {
            get { return _isUpdatingRecruitShortUIModels; }
            set
            {
                if (_isUpdatingRecruitShortUIModels == value) return;
                _isUpdatingRecruitShortUIModels = value;
                OnPropertyChanged();
            }
        }

        private bool _isExportRecruit;
        public bool IsExportRecruit
        {
            get { return _isExportRecruit; }
            set
            {
                if (_isExportRecruit == value) return;
                _isExportRecruit = value;
                OnPropertyChanged();
            }
        }

        private bool _isExportTableRecruit;
        public bool IsExportTableRecruit
        {
            get { return _isExportTableRecruit; }
            set
            {
                if (_isExportTableRecruit == value) return;
                _isExportTableRecruit = value;
                OnPropertyChanged();
            }
        }

        private bool _showImportMessage;
        public bool ShowImportMessage
        {
            get { return _showImportMessage; }
            set
            {
                if (_showImportMessage == value) return;
                _showImportMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                if (_state == value) return;
                _state = value;
                OnPropertyChanged();
            }
        }

        private string _log;
        public string Log
        {
            get { return _log; }
            set
            {
                if (_log == value) return;
                _log = value;

                OnPropertyChanged();
            }
        }

        private string _vkListToday;
        public string VKListToday
        {
            get { return _vkListToday; }
            set
            {
                if (_vkListToday == value)
                    return;
                _vkListToday = value;
                OnPropertyChanged();
            }
        }


        private string _adventToday;
        public string AdventToday
        {
            get { return _adventToday; }
            set
            {
                if (_adventToday == value) 
                    return;
                _adventToday = FormatPeopleCount(value, TitleConstants.AdventTodayTemplate);
                OnPropertyChanged();
            }
        }

        private string _сonscriptionCount;
        public string ConscriptionCount
        {
            get { return _сonscriptionCount; }
            set
            {
                if (_сonscriptionCount == value) 
                    return;
                _сonscriptionCount = FormatPeopleCount(value, TitleConstants.ConscriptionCountTemplate);
                OnPropertyChanged();
            }
        }

        private string _onOspCount;
        public string OnOspCount
        {
            get { 

                return _onOspCount; 
            }
            set
            {
                if (_onOspCount == value) 
                    return;
                _onOspCount = FormatPeopleCount(value, TitleConstants.OnOspCountTemplate);
                OnPropertyChanged();
            }
        }

        private bool _adventVKPopupState;
        public bool AdventVKPopupState
        {
            get
            {
                return _adventVKPopupState;
            }
            set
            {
                if (_adventVKPopupState == value)
                    return;
                _adventVKPopupState = value;
                OnPropertyChanged();
            }
        }

        private string FormatPeopleCount(string count, string template)
        {
            int intVal = 0;
            int.TryParse(count, out intVal);
            string word = (intVal % 100 > 19 || intVal % 100 < 10) && (intVal % 10 == 2 || intVal % 10 == 3 || intVal % 10 == 4) ? "человекa" : "человек";
            return string.Format(template, $"{count} {word}");
        }
        

        public MainViewModel(IParameterizedCommandAsync<ImportRecruitCommandParameters> importRecruitCommand,
            IParameterizedCommandAsync<RemoveRecruitCommandParameters> removeRecruitParameterizedCommand,
            IParameterizedCommandAsync<TransmitRecruitCommandParameters> transmitRecruitParameterizedCommand,
            IParameterizedCommandAsync<ExportRecruitCommandParameters> exportRecruitParameterizedCommand,
            IParameterizedCommandAsync<ExportTableRecruitCommandParameters> exportTableRecruitParameterizedCommand,
            IParameterizedCommandAsync<UpdateRecruitsCommandParameters> updateRecruitsParameterizedCommand,
            string ImportDirectoryPath,
            Func<bool> MessageCallback = null)
        {
            if (importRecruitCommand == null)
            {
                throw new ArgumentNullException(nameof(importRecruitCommand));
            }

            if (removeRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(removeRecruitParameterizedCommand));
            }

            if (transmitRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(transmitRecruitParameterizedCommand));
            }

            if (exportRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(exportRecruitParameterizedCommand));
            }

            if (exportTableRecruitParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(exportTableRecruitParameterizedCommand));
            }

            if (updateRecruitsParameterizedCommand == null)
            {
                throw new ArgumentNullException(nameof(updateRecruitsParameterizedCommand));
            }

            if (string.IsNullOrWhiteSpace(ImportDirectoryPath))
            {
                throw new ArgumentNullException(nameof(ImportDirectoryPath));
            }

            _importRecruitCommand = importRecruitCommand;
            _removeRecruitParameterizedCommand = removeRecruitParameterizedCommand;
            _transmitRecruitParameterizedCommand = transmitRecruitParameterizedCommand;
            _exportRecruitParameterizedCommand = exportRecruitParameterizedCommand;
            _exportTableRecruitParameterizedCommand = exportTableRecruitParameterizedCommand;
            _updateRecruitsParameterizedCommand = updateRecruitsParameterizedCommand;
            _importDirectoryPath = ImportDirectoryPath;
            _messageCallback = MessageCallback;

            RecruitShortUIModels = new ObservableCollection<RecruitShortUIModel>();

            RegionalCollectionPoints = RcpConstants.RegionalCollectionPoints;
            Storages = GenericEnumExtensions.GetStringValues<Storage>(withEmptyValue: false);

            _selectedStorage = Storage.File.ToString();
            ConscriptionDate = DateTime.Today;
        }

        public void HandleDrop(System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

                if (SelectedStorage == Storage.File.ToString())
                {

                    if (!string.IsNullOrWhiteSpace(ConscriptionDate.Value.ToString(DateConstants.RecruitDateFormat)) && SelectedRegionalCollectionPoint != null)
                    {
                        string finalFolderPath = GetImportDirectoryPath();

                        copyFilesToImportDirectoryFolder(files, finalFolderPath);

                        if (SelectedRegionalCollectionPoint == "Доволенский")
                        {
                            foreach (string file in Directory.GetFiles(finalFolderPath))
                            {
                                if(Path.GetExtension(file) == ".plain")
                                {
                                    OnStateChanged($"Исправлен файл {file} для Доволенского военного комиссариата", StateResult.Success);
                                    string dovolenskContent = System.IO.File.ReadAllText(file);
                                    System.IO.File.WriteAllText(file, dovolenskContent.Replace("Краснозерский", "Доволенский"));
                                }
                            }
                        }

                        ImportCommand.Execute(null);
                    }
                    else
                    {
                        OnStateChanged(EmptyConscriptionDateOrRVKError, StateResult.Error);
                    }
                } 
            }
        }

        private void copyFilesToImportDirectoryFolder(string[] files, string finalFolderPath)
        {
            foreach (string filePath in files)
            {
                string newFilePath = Path.Combine(finalFolderPath, Path.GetFileName(filePath));

                switch (Path.GetExtension(filePath))
                {
                    case ".plain":
                        FileStream newFile = System.IO.File.Create(newFilePath);
                        newFile.Close();
                        System.IO.File.Copy(filePath, newFile.Name, true);
                        OnStateChanged($"Копирование {Path.GetFileName(newFile.Name)}", StateResult.Success);
                        break;
                    case ".zip":
                        ZipFile zip = ZipFile.Read(filePath);

                        foreach (var item in zip)
                        {
                            if (Path.GetExtension(item.FileName) == ".plain")
                            {
                                item.Extract(finalFolderPath, ExtractExistingFileAction.OverwriteSilently);
                                OnStateChanged($"Извлечение {item.FileName}", StateResult.Success);
                            }
                        }
                        break;
                    case "":
                        copyFilesToImportDirectoryFolder(Directory.GetFiles(filePath), finalFolderPath);
                        break;
                }
            }
        }

        private ICommand _importCommand;
        public ICommand ImportCommand
        {
            get
            {
                return _importCommand ?? (_importCommand = new AsyncCommand(async vm =>
                {
                    IsImportingRecruits = true;

                    try
                    {
                        var parameters = new ImportRecruitCommandParameters(ConscriptionDate,
                            SelectedRegionalCollectionPoint,
                            this);

                        await _importRecruitCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsImportingRecruits = false;
                    }


                    UpdateRecuitUIModelsCommand.Execute(null);
                },
                this,
                vm => SelectedStorage == Storage.File.ToString()));
            }
        }

        private ICommand _addRecruitCommand;
        public ICommand AddRecruitCommand
        {
            get
            {
                return _addRecruitCommand ?? (_addRecruitCommand = new ActionCommand(vm => {
                    OnRecruitViewShowed(new RecruitOperationEventArgs(RecruitOperation.Add));
                },
                this,
                vm => SelectedStorage == Storage.Sqlite.ToString() && !SaveToFormDatabaseCommand.IsExecuting));
            }
        }

        private ICommand _editRecruitCommand;
        public ICommand EditRecruitCommand
        {
            get
            {
                return _editRecruitCommand ?? (_editRecruitCommand = new ActionCommand(vm =>
                {
                    if (SelectedRecruitShortUIModel == null) return;

                    switch (SelectedRecruitShortUIModel.Storage)
                    {
                        case Storage.File:
                            OnRecruitViewShowed(new RecruitOperationEventArgs(RecruitOperation.Import, SelectedRecruitShortUIModel));
                            break;
                        case Storage.Sqlite:
                        case Storage.Firebird:
                        default:
                            OnRecruitViewShowed(new RecruitOperationEventArgs(RecruitOperation.Edit, SelectedRecruitShortUIModel));
                            break;
                    }
                },
                this,
                vm => SelectedRecruitShortUIModel != null &&
                      !SaveToFormDatabaseCommand.IsExecuting));
            }
        }

        private ICommand _removeRecruitCommand;
        public ICommand RemoveRecruitCommand
        {
            get
            {
                return _removeRecruitCommand ?? (_removeRecruitCommand = new AsyncCommand(async vm =>
                {
                    if (_messageCallback != null)
                    {
                        Message = TitleConstants.AreYouSureDialogText;
                        if (!_messageCallback()) return;
                    }

                    var selectedShortUIModel = SelectedRecruitShortUIModel;

                    RecruitShortUIModels.Remove(SelectedRecruitShortUIModel);

                    var parameters = new RemoveRecruitCommandParameters(selectedShortUIModel, this);
                    await _removeRecruitParameterizedCommand.ExecuteAsync(parameters);
                },
                this,
                vm => SelectedRecruitShortUIModel != null &&
                      !SaveToFormDatabaseCommand.IsExecuting));
            }
        }

        private AsyncCommand _saveToFormDatabaseCommand;
        public AsyncCommand SaveToFormDatabaseCommand
        {
            get
            {
                return _saveToFormDatabaseCommand ?? (_saveToFormDatabaseCommand = new AsyncCommand(async vm =>
                {
                    var list = vm as IList;
                    if (list == null) return;

                    var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();
                    if (selectedRecruitShortUIModels == null) 
                        return;
                    
                    bool isAllHaveSqliteIds = selectedRecruitShortUIModels.All(r => r.SqliteId.HasValue);
                    if (isAllHaveSqliteIds)
                    {
                        var ids = selectedRecruitShortUIModels.Select(r => r.SqliteId.Value);

                        var parameters = new TransmitRecruitCommandParameters(ids, this);

                        await _transmitRecruitParameterizedCommand.ExecuteAsync(parameters);

                        UpdateRecuitUIModelsCommand.Execute(null);

                        if(RecruitShortUIModels.Count == 0) 
                            SelectedStorage = Storage.Firebird.ToString();

                        await Console.Out.WriteLineAsync($"Количество призывников: {RecruitShortUIModels.Count}, Хранилище: {SelectedStorage} ({DateTime.Now})!!!ASYNC");
                        Console.WriteLine($"Количество призывников: {RecruitShortUIModels.Count}, Хранилище: {SelectedStorage} ({DateTime.Now})");
                    }
                },
                this,
                vm => 
                {
                    var list = vm as IList;
                    if (list == null) 
                        return false;

                    var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();
                    if (selectedRecruitShortUIModels == null) 
                        return false;

                    return selectedRecruitShortUIModels.Count() > 0 &&
                           selectedRecruitShortUIModels.All(r => r.Storage == Storage.Sqlite);
                }));
            }
        }

        private ICommand _exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                return _exportCommand ?? (_exportCommand = new AsyncCommand(async vm =>
                {
                    Message = TitleConstants.AutoPrintText;
                    bool _printAfterExport = _messageCallback(); 

                    var list = vm as IList;
                    if (list == null) return;

                    IsExportRecruit = true;

                    try
                    {
                        var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();
                        if (selectedRecruitShortUIModels == null) return;

                        var ids = selectedRecruitShortUIModels.Select(r => r.SqliteId.Value).ToList();

                        var parameters = new ExportRecruitCommandParameters(ids, _printAfterExport, this);
                        await _exportRecruitParameterizedCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsExportRecruit = false;
                    }
                },
                this,
                vm =>
                {
                    var list = vm as IList;
                    if (list == null) return false;

                    var selectedRecruitShortUIModels = list.Cast<RecruitShortUIModel>();

                    return selectedRecruitShortUIModels.Count() > 0 && selectedRecruitShortUIModels.All(r => r.SqliteId.HasValue);
                }));
            }
        }

        private ICommand _exportTableCommand;
        public ICommand ExportTableCommand
        {
            get
            {
                return _exportTableCommand ?? (_exportTableCommand = new AsyncCommand(async vm =>
                {
                    Message = TitleConstants.AutoPrintText;
                    IsExportTableRecruit = true;

                    try
                    {
                        var recruitShortUIModels = RecruitShortUIModels.ToList();

                        var conscriptionDate = ConscriptionDate.HasValue
                            ? ConscriptionDate.Value.ToString("D")
                            : string.Empty;

                        var regionalCollectionPoint = SelectedRegionalCollectionPoint;

                        var parameters = new ExportTableRecruitCommandParameters(recruitShortUIModels,
                            conscriptionDate,
                            regionalCollectionPoint,
                            this, false);

                        await _exportTableRecruitParameterizedCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsExportTableRecruit = false;
                    }
                },
                this,
                vm => RecruitShortUIModels.Count > 0));
            }
        }

        private ICommand _clearLogCommand;
        public ICommand ClearLogCommand
        {
            get
            {
                return _clearLogCommand ?? (_clearLogCommand = new ActionCommand(vm =>
                {
                    Log = string.Empty;
                    State = string.Empty;
                },
                this,
                vm => !string.IsNullOrWhiteSpace(Log)));
            }
        }

        private ICommand _selectAll;
        public ICommand SelectAll
        {
            get
            {
                return _selectAll ?? (_selectAll = new ActionCommand(vm =>
                {
                    foreach (var item in RecruitShortUIModels)
                    {
                        item.IsSelected = true;
                    }
                    OnPropertyChanged("IsSelected");
                },
                this,
                vm => RecruitShortUIModels.Count > 0));
            }
        }

        private ICommand _clearFiltersCommand;
        public ICommand ClearFiltersCommand
        {
            get
            {
                return _clearFiltersCommand ?? (_clearFiltersCommand = new ActionCommand(vm =>
                {
                    SelectedRegionalCollectionPoint = string.Empty;
                    ConscriptionDate = DateTime.Today;
                    Surname = null;
                    SelectedStorage = Storage.File.ToString();

                    UpdateRecuitUIModelsCommand.Execute(null);
                },
                this,
                vm => ConscriptionDate != DateTime.Today || 
                !string.IsNullOrWhiteSpace(SelectedRegionalCollectionPoint) || 
                !string.IsNullOrWhiteSpace(Surname) || 
                SelectedStorage != "File"));
            }
        }

        private ICommand _showSettingsViewCommand;
        public ICommand ShowSettingsViewCommand
        {
            get
            {
                return _showSettingsViewCommand ?? (_showSettingsViewCommand = new ActionCommand(vm =>
                {
                    OnSettingsViewShowed();
                },
                this));
            }
        }

        private ICommand _showAdventVKPopupCommand;
        public ICommand ShowAdventVKPopupCommand
        {
            get
            {
                return _showAdventVKPopupCommand ?? (_showAdventVKPopupCommand = new ActionCommand(vm =>
                {
                    AdventVKPopupState = true;
                },
                this,
                vm => VKListToday?.Length > 0)); //Добавить условие
            }
        }

        private ICommand _showRecruitFolderCommand;
        public ICommand ShowRecruitFolderCommand
        {
            get
            {
                return _showRecruitFolderCommand ?? (_showRecruitFolderCommand = new ActionCommand(vm =>
                {
                    try
                    {
                        Process.Start("explorer.exe", GetImportDirectoryPath());
                    }
                    catch 
                    {
                        throw new ArgumentNullException(OpenFolderError);
                    }
                },
                this));
            }
        }

        private string GetImportDirectoryPath()
        {
            string conscriptionDateRegionalDirectoryPath;

            if (ConscriptionDate != null)
                conscriptionDateRegionalDirectoryPath = Path.Combine(_importDirectoryPath, ConscriptionDate.Value.ToString(DateConstants.RecruitDateFormat) ?? string.Empty, SelectedRegionalCollectionPoint ?? string.Empty);
            else 
                conscriptionDateRegionalDirectoryPath = Path.Combine(_importDirectoryPath);

            if (!Directory.Exists(conscriptionDateRegionalDirectoryPath))
                Directory.CreateDirectory(conscriptionDateRegionalDirectoryPath);

            return conscriptionDateRegionalDirectoryPath;
        }

        public void RecruitSaved(object sender, RecruitOperationEventArgs e)
        {
            UpdateRecuitUIModelsCommand.Execute(null);
        }

        private ICommand _updateRecuitUIModelsCommand;
        public ICommand UpdateRecuitUIModelsCommand
        {
            get
            {
                return _updateRecuitUIModelsCommand ?? (_updateRecuitUIModelsCommand = new AsyncCommand(async vm =>
                {
                    IsUpdatingRecruitShortUIModels = true;
                    
                    try
                    {
                        var parameters = new UpdateRecruitsCommandParameters(ConscriptionDate,
                            SelectedRegionalCollectionPoint,
                            Surname,
                            SelectedStorage,
                            RecruitShortUIModels,
                            this);

                        await _updateRecruitsParameterizedCommand.ExecuteAsync(parameters);
                    }
                    finally
                    {
                        IsUpdatingRecruitShortUIModels = false;
                        ShowImportMessage = RecruitShortUIModels.ToList().Count == 0 && SelectedStorage == Storage.File.ToString();
                    }

                },
                this));
            }
        }
    }
}
