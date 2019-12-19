import { Property } from '@/models/property'
import * as model from '@/utils/model'
import { PlatformSchemaObject } from '@/models/OpenAPIV3/PlatformSchemaObject'
import { modelApi } from './modelApi'

export interface IModel {
  modelLabel: string
  properties: Property[]
  modelApi: modelApi
}

export class Model implements IModel {
  public modelLabel: string
  public modelApi: modelApi = {
    controller: '',
    controllerMethod: 'GetAll'
  }
  public properties: Property[]

  constructor(schema?: PlatformSchemaObject) {
    this.modelLabel = (schema && schema.modelLabel) || ''
    this.modelApi = (schema && schema.modelApi) || this.modelApi
    this.properties = model.convertProperties((schema && schema.properties) || [])
  }
}
