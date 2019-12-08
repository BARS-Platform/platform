import { Property } from '@/models/property'

export function convertProperties(props: any): Property[] {
  let properties: Property[] = []

  let entries = Object.entries(props) as [string, Property][]

  for (let [key, value] of entries) {
    let property: Property = {
      propertyName: key,
      displayIn: value.displayIn,
      label: value.label,
      type: value.type,
      refModel: value.refModel
    }

    properties.push(property)
  }

  return properties
}
