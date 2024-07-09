using System.Windows.Controls;
using ConscriptionAdvent.Data.Firebird.Dto;
using ConscriptionAdvent.Data.Firebird;
using ConscriptionAdvent.UI.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConscriptionAdvent.UI.Views
{
    /// <summary>
    /// Interaction logic for RecruitSecondView.xaml
    /// </summary>
    public partial class RecruitSecondView : UserControl
    {
        private readonly static UserSettings UserSettings = new UserSettings();
        public EnumDictionary FormDictionary = new EnumDictionary(UserSettings.Value["FirebirdLocalFilePath"]);

        public RecruitSecondView()
        {
            InitializeComponent();
            SpecBox.ItemsSource = FormDictionary.Speclist;
        }
    }
}
