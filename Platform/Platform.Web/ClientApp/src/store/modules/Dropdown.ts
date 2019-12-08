import * as api from '@/store/api/Dropdown'
import { Module, VuexModule, Action, Mutation } from 'vuex-module-decorators'
import Store from '@/store/index'
import * as notify from '@/utils/notify'
import { ListParam } from '@/models/data/listParam'
import { RowData } from '~/src/models/rowData'

@Module({
  dynamic: true,
  name: 'dropdown',
  namespaced: true,
  store: Store
})
export default class DropdownModule extends VuexModule {
  data: RowData[] = []

  get Data() {
    return this.data
  }

  @Action({ commit: 'SET_DATA', rawError: true })
  async getData(listParam: ListParam) {
    let data!: any[]
    await api
      .getData(listParam)
      .then(response => {
        data = response
      })
      .catch(() => {
        notify.error('Произошла ошибка при загрузке данных')
      })

    return data
  }

  @Mutation
  SET_DATA(data: RowData[]) {
    this.data = data
  }
}
