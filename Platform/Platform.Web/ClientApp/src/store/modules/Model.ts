import * as api from '@/store/api/Model'
import { Module, VuexModule, Action, Mutation } from 'vuex-module-decorators'
import Store from '@/store/index'
import SwaggerParser from 'swagger-parser'
import { OpenAPIV3 } from 'openapi-types'
import { Model } from '@/models/model'
import { PlatformSchemaObject } from '@/models/OpenAPIV3/PlatformSchemaObject'
import * as notify from '@/utils/notify'
import { ListResult } from '@/models/data/listResult'

@Module({
  dynamic: true,
  name: 'model',
  namespaced: true,
  store: Store
})
export default class ModelModule extends VuexModule {
  model: Model = new Model()
  listResult: ListResult = new ListResult()

  get Model() {
    return this.model
  }

  get ListResult() {
    return this.listResult
  }

  @Action({ commit: 'SET_MODEL', rawError: true })
  async getCurrentModel(modelName: string) {
    const response = (await SwaggerParser.dereference('/swagger/v1/swagger.json')) as OpenAPIV3.Document

    let schemaModel = (response.components!.schemas![`${modelName}Dto`] as unknown) as PlatformSchemaObject

    let model: Model

    model = new Model(schemaModel)

    if (model.modelName === '' || model.properties.length === 0) {
      notify.error('Данная модель не найдена в схеме swagger')
      model = new Model()
    }

    return model
  }

  @Action({ commit: 'SET_DATA', rawError: true })
  async getData(listResult: ListResult) {
    let result = listResult
    await api
      .getData(listResult)
      .then(response => {
        console.log(response)
        result = response
      })
      .catch(() => {
        notify.error('Произошла ошибка при загрузке данных')
        result = {
          modelName: listResult.modelName,
          data: [],
          pagination: {
            page: 1,
            rowsNumber: 5,
            rowsPerPage: 5
          }
        }
      })

    return result
  }

  @Mutation
  SET_MODEL(model: Model) {
    this.model = model
  }

  @Mutation
  SET_DATA(listResult: ListResult) {
    this.listResult = listResult
  }
}
