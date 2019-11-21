namespace Platform.Fodels.Attributes

open Platform.Fodels.Entities
open System

type MenuAttribute(description: string, link: string, icon: string) =
   inherit Attribute()
   member __.Value = new MenuInfoEntity(description, link, icon)
