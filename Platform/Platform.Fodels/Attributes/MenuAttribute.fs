namespace Platform.Fodels.Attributes

open Platform.Fodels.Entities
open Platform.Fodels.Enums
open System

type MenuAttribute(description: string, permissionId: PermissionNamesForFodels, sectionName: Sections, link: string, icon: Icons) =
   inherit Attribute()
   member __.Value = new MenuInfoEntity(description, permissionId, sectionName, link, icon)
