namespace Platform.Fodels.Attributes
open System

type LabelAttribute(newVal: string ) =
   inherit Attribute()
   member __.Value = newVal
