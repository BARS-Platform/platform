namespace Platform.Fodels.Attributes

open Platform.Fodels.Entities
open System

type MenuAttribute(description: string, permissionId: string, sectionName: string, link: string, icon: string) =
   inherit Attribute()
   member __.Value = new MenuInfoEntity(description, permissionId, sectionName, link, icon)
