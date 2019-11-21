using System.Collections.Generic;
using System.Linq;
using Platform.Fodels.Attributes;
using Platform.Fodels.Entities;
using Platform.Fodels.Models;
using Platform.Services.Helpers;

namespace Platform.Services.Services
{
    public class MenuService
    {
        public MenuEntity GenerateMenu(IEnumerable<Permission> permissions)
        {
            var permissionIds = permissions
                .Select(x => x.PermissionId);
            var sections = new List<string>
            {
                "Модели", "Администрирование"
            };
            
            var attributeValues = TypeHelper.GetAttributes<MenuAttribute>()
                .Where(x => permissionIds.Contains(x.Value.PermissionId))
                .ToList();

            var menuSections = new List<MenuEntity>();
            
            foreach (var section in sections)
            {
                var itemsFromSection = attributeValues
                    .Where(x => x.Value.SectionName == section)
                    .Select(x => new MenuEntity()
                    {
                        Icon = x.Value.Icon,
                        Link = x.Value.Link,
                        Title = x.Value.Description
                    });
                
                menuSections.Add(new MenuEntity()
                {
                    Title = section,
                    Children = itemsFromSection
                });
            }

            return new MenuEntity()
            {
                Children = menuSections
            };
        }
    }
}