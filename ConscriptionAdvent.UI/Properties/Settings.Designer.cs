﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConscriptionAdvent.UI.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Z:\\Документы\\Призыв\\2023 год\\Осень\\Эл. База")]
        public string ImportDirectoryPath {
            get {
                return ((string)(this["ImportDirectoryPath"]));
            }
            set {
                this["ImportDirectoryPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("W:\\Фотоальбом\\PersonalPhoto2023-2")]
        public string PersonalPhotoDirectoryPath {
            get {
                return ((string)(this["PersonalPhotoDirectoryPath"]));
            }
            set {
                this["PersonalPhotoDirectoryPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("g:\\work\\mash\\new_form\\base\\FORM2023-2.GDB")]
        public string FirebirdLocalFilePath {
            get {
                return ((string)(this["FirebirdLocalFilePath"]));
            }
            set {
                this["FirebirdLocalFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("q:\\Data\\PupaParser\\Advent\\Advent\\Advent\\SQLite\\Main2023-2.db")]
        public string SqliteLocalFilePath {
            get {
                return ((string)(this["SqliteLocalFilePath"]));
            }
            set {
                this["SqliteLocalFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\ConscriptionAdvent\\Templates\\ABPattern.xls")]
        public string ExportTemplateFilePath {
            get {
                return ((string)(this["ExportTemplateFilePath"]));
            }
            set {
                this["ExportTemplateFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\ConscriptionAdvent\\Templates\\ExportTableTemplate.xlsx")]
        public string ExportTableTemplateFilePath {
            get {
                return ((string)(this["ExportTableTemplateFilePath"]));
            }
            set {
                this["ExportTableTemplateFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\ConscriptionAdvent\\ExportedFiles")]
        public string ExportDirectoryPath {
            get {
                return ((string)(this["ExportDirectoryPath"]));
            }
            set {
                this["ExportDirectoryPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LightGreen")]
        public string ThemeValue {
            get {
                return ((string)(this["ThemeValue"]));
            }
            set {
                this["ThemeValue"] = value;
            }
        }
    }
}