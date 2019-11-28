using System.Collections.Generic;
using System.Linq;
using Platform.Fodels.Attributes;
using Platform.Fodels.Entities;
using Platform.Fodels.Enums;
using Platform.Fodels.Models;
using Platform.Services.Common;
using Platform.Services.Helpers;

namespace Platform.Services.Services
{
    public class MenuService
    {
        public MenuEntity GenerateMenu(IEnumerable<Permission> permissions)
        {
            var permissionIds = permissions
                .Select(x => x.PermissionId)
                .ToList();

            var sections = GetSections();
            
            var attributeValues = TypeHelper.GetAttributes<MenuAttribute>()
                .Where(x => permissionIds.Contains(x.Value.PermissionId.GetString()))
                .ToList();

            var menuSections = new List<MenuEntity>();
            
            foreach (var section in sections)
            {
                var itemsFromSection = attributeValues
                    .Where(x => x.Value.Section.GetString() == section)
                    .Select(x => new MenuEntity()
                    {
                        Icon = x.Value.Icon.GetString(),
                        Link = "/" + x.Value.Link,
                        Title = x.Value.Description
                    })
                    .ToList();
                
                if (itemsFromSection.Any() &&
                    permissionIds.Contains(MenuExtensions.SectionPermissionIds[section]))
                {
                    menuSections.Add(new MenuEntity
                    {
                        Title = section,
                        Link = "/" + MenuExtensions.SectionLinks[section],
                        Children = itemsFromSection
                    });
                }
            }

            return new MenuEntity()
            {
                Title = "Меню",
                Children = menuSections
            };
        }
        
        private static IEnumerable<string> GetSections()
        {
            return new List<string>
            {
                Sections.Models.GetString(),
                Sections.Dictionary.GetString(),
                Sections.Administration.GetString()
            };
        }
    }
}