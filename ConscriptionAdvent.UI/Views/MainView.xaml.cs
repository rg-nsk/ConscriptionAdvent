using ConscriptionAdvent.Presentation.Models;
using ConscriptionAdvent.Presentation.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConscriptionAdvent.Data.Firebird;
using System.Collections.Generic;
using ConscriptionAdvent.UI.Configurations;

namespace ConscriptionAdvent.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            HideLoaders();
            
            SubscribeLoaders(viewModel);
            SubscribeRecruitDoubleClick(viewModel);
            SubscribeDragAndDrop(viewModel);

            DataContext = viewModel;
        }

        private void HideLoaders()
        {
            importLoader.Visibility = Visibility.Collapsed;
            updateLoader.Visibility = Visibility.Collapsed;
            exportRecruitLoader.Visibility = Visibility.Collapsed;
            exportTableRecruitLoader.Visibility = Visibility.Collapsed;
            ImportOrDragAndDrop.Visibility = Visibility.Collapsed;
        }

        private void SubscribeLoaders(MainViewModel viewModel)
        {
            viewModel.PropertyChanged += (s, e) =>
            {

                if (e.PropertyName == nameof(viewModel.IsImportingRecruits))
                {
                    importLoader.Visibility = viewModel.IsImportingRecruits
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }

                if (e.PropertyName == nameof(viewModel.IsUpdatingRecruitShortUIModels))
                {
                    updateLoader.Visibility = viewModel.IsUpdatingRecruitShortUIModels
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }

                if (e.PropertyName == nameof(viewModel.IsExportRecruit))
                {
                    exportRecruitLoader.Visibility = viewModel.IsExportRecruit
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }

                if (e.PropertyName == nameof(viewModel.IsExportTableRecruit))
                {
                    exportTableRecruitLoader.Visibility = viewModel.IsExportTableRecruit
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }

                if (e.PropertyName == nameof(viewModel.ShowImportMessage))
                {
                    ImportOrDragAndDrop.Visibility = viewModel.ShowImportMessage
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }
            };
        }

        private void SubscribeRecruitDoubleClick(MainViewModel viewModel)
        {
            recruits.MouseDoubleClick += (s, e) =>
            {
                viewModel.EditRecruitCommand.Execute(null);
            };
        }

        private void SubscribeDragAndDrop(MainViewModel viewModel)
        {
            recruits.Drop += (s, e) =>
            {
                viewModel.HandleDrop(e);
            };
        }

    }
}
