import { OpenAPIV3 } from 'openapi-types'
import { modelApi } from '@/models/modelApi'

export interface PlatformSchemaObject extends OpenAPIV3.BaseSchemaObject {
  modelLabel: string
  modelApi: modelApi
}
