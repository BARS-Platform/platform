import { RefModel } from './refModel'

export interface Property {
  propertyName: string
  label: string
  type: string
  displayIn: {
    grid: Boolean
    form: Boolean
  }
  refModel?: RefModel
}
