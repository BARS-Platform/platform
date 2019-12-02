import { Property } from '@/models/property'
import * as model from '@/utils/model'
import { PlatformSchemaObject } from '@/models/OpenAPIV3/PlatformSchemaObject'
export interface IModel {
  modelName: string
  properties: Property[]
}

export class Model implements IModel {
  public modelName: string

  public properties: Property[]

  constructor(schema: PlatformSchemaObject) {
    this.modelName = schema.modelName
    this.properties = model.convertProperties(schema.properties!)
  }
}
