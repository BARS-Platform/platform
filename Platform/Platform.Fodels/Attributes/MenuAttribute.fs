namespace Platform.Fodels.Attributes

open Platform.Fodels.Entities
open System

type MenuAttribute(newVal: MenuInfoEntity) =
   inherit Attribute()
   member __.Value = newVal
