import * as api from '@/store/api/Model'
import { Module, VuexModule, Action, Mutation } from 'vuex-module-decorators'
import Store from '@/store/index'
import SwaggerParser from 'swagger-parser'
import { OpenAPIV3 } from 'openapi-types'
import { Model } from '@/models/model'
import { PlatformSchemaObject } from '@/models/OpenAPIV3/PlatformSchemaObject'
import { RowData } from '@/models/rowData'
import * as notify from '@/utils/notify'

@Module({
  dynamic: true,
  name: 'model',
  namespaced: true,
  store: Store
})
export default class ModelModule extends VuexModule {
  model: Model = new Model()
  data: RowData[] = []

  get Model() {
    return this.model
  }

  get Data() {
    return this.data
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
  async getData(modelName: string) {
    let rowData: RowData[] = []
    await api
      .getData(modelName)
      .then(data => {
        rowData = data
      })
      .catch(() => {
        notify.error('Произошла ошибка при загрузке данных')
        rowData = []
      })

    return rowData
  }

  @Mutation
  SET_MODEL(model: Model) {
    this.model = model
  }

  @Mutation
  SET_DATA(data: RowData[]) {
    this.data = data
  }
}
