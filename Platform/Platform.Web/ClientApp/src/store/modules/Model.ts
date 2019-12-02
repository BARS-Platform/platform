import { Module, VuexModule, Action } from 'vuex-module-decorators'
import Store from '@/store/index'
import SwaggerParser from 'swagger-parser'
import { OpenAPIV3 } from 'openapi-types'
import { Model } from '@/models/model'
import { PlatformSchemaObject } from '@/models/OpenAPIV3/PlatformSchemaObject'
import * as model from '@/utils/model'

@Module({
  dynamic: true,
  name: 'model',
  namespaced: true,
  store: Store
})
export default class ModelModule extends VuexModule {
  @Action({ rawError: true })
  async getCurrentModel(modelName: string) {
    const response = (await SwaggerParser.dereference('/swagger/v1/swagger.json')) as OpenAPIV3.Document

    let schemaModel = (response.components!.schemas![`${modelName}Dto`] as unknown) as PlatformSchemaObject

    let model: Model

    model = new Model(schemaModel)

    if (model.modelName === '' || model.properties.length === 0) {
      throw 'Данная модель не найдена в схеме swagger'
    }

    return model
  }
}
