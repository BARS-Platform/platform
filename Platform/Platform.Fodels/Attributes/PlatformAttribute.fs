namespace Platform.Fodels.Attributes

open Platform.Configuration.Enums
open System

type PlatformAttribute(newVal: AttributesEnum) =
   inherit Attribute()
   member __.Value = newVal
