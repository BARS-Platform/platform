import * as api from '@/store/api/Model'
import { Module, VuexModule, Action, Mutation } from 'vuex-module-decorators'
import Store from '@/store/index'
import SwaggerParser from 'swagger-parser'
import { OpenAPIV3 } from 'openapi-types'
import { Model } from '@/models/model'
import { PlatformSchemaObject } from '@/models/OpenAPIV3/PlatformSchemaObject'
import * as notify from '@/utils/notify'
import { ListResult } from '@/models/data/listResult'
import { ListParam } from '@/models/data/listParam'
import { ModelDto } from '~/src/models/modelDto'

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
  async getData(listParam: ListParam) {
    let result!: ListResult
    await api
      .getData(listParam)
      .then(response => {
        result = response
      })
      .catch(() => {
        notify.error('Произошла ошибка при загрузке данных')
        result = {
          data: [],
          listParam: listParam
        }
      })

    return result
  }

  @Action({ rawError: true })
  async createModel(dto: ModelDto) {
    let result!: any
    await api.createModel(dto).catch(() => {
      notify.error('Произошла ошибка при загрузке данных')
      result = {}
    })

    return result
  }

  @Action({ rawError: true })
  async deleteEntry({ modelName, entryId }: { modelName: string; entryId: number }) {
    await api.deleteEntry({ modelName, entryId }).catch(() => {
      notify.error('Произошла ошибка при  удалении')
    })
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
