using System;
using System.Collections.Generic;
using Platform.Fodels.Enums;

namespace Platform.Services.Common
{
    public static class MenuExtensions
    {
        public static readonly Dictionary<string, string> SectionPermissionIds = new Dictionary<string, string>
        {
            {Sections.Models.GetString(), "ViewModels"},
            {Sections.Dictionary.GetString(), "ViewDictionaries"},
            {Sections.Administration.GetString(), "ViewAdmin"}
        }; 
        
        public static string GetString(this Icons icon)
        {
            return icon switch
            {
                Icons.Home => "home",
                Icons.Cart => "shopping_cart",
                Icons.Star => "star",
                _ => throw new ArgumentOutOfRangeException(nameof(icon), icon, null)
            };
        }
        
        public static string GetString(this Sections section)
        {
            return section switch
            {
                Sections.Models => "Модели",
                Sections.Dictionary => "Справочники",
                Sections.Administration => "Администрирование",
                _ => throw new ArgumentOutOfRangeException(nameof(section), section, null)
            };
        }
    }
}