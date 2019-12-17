import { RefProperty } from './refProperty'

export interface Property {
  propertyName: string
  label: string
  type: string
  displayIn: {
    grid: Boolean
    form: Boolean
  }
  refProperty?: RefProperty
}
