<template>
  <q-page class="flex" ref="page">
    <form-dialog :dialog.sync="dialog" :model="Model" :modelName="currentParam" :modelValues.sync="modelValues" :isForCreate="editWindowIsForCreate">
    </form-dialog>
    <data-grid-params />
    <data-grid
      :model="Model"
      :height="tableHeight"
      :onFilter="onFilter"
      :listResult="ListResult"
      :onUpdate="onUpdate"
      :onRequest="onRequest"
      :onCreate="onCreate"
      :onEdit="onEdit"
      :onDelete="onDelete"
      :loading="loading"
    />
    <q-resize-observer @resize="onResize" />
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import DataGrid from '@/components/grid/data-grid.vue'
import DataGridParams from '@/components/grid/data-grid-params.vue'

import * as access from '@/utils/access'
import { ListParam } from '@/models/data/listParam'
import { Pagination } from '@/models/data/pagination'
import { Sorting } from '@/models/data/sorting'
import FormDialog from '@/components/form/form-dialog.vue'
import { FormField } from '@/models/formField'

@Component({
  components: {
    DataGrid,
    DataGridParams,
    FormDialog
  }
})
export default class ModelIndex extends Vue {
  private modelStore = getModule(ModelModule)
  routeParam = 'name'
  currentParam = ''
  currentHeight = 0
  loading = false
  dialog: Boolean = false
  editWindowIsForCreate: Boolean = true
  modelValues: FormField[] = []

  get Model() {
    return this.modelStore.Model
  }

  onResize(size: { width: number; height: number }) {
    this.currentHeight = size.height
  }

  get ListResult() {
    return this.modelStore.ListResult
  }

  get tableHeight() {
    const bottomHeight = 49
    const headerHeight = 65
    return this.currentHeight - bottomHeight - headerHeight
  }

  onUpdate() {
    this.loading = true
    this.modelStore.getData(this.ListResult.listParam).finally(() => (this.loading = false))
  }

  onFilter() {
    this.ListResult.listParam.pagination.page = 1
    this.getcurrentData(this.ListResult.listParam.pagination)
  }

  onDelete(entryId: number) {
    this.loading = true
    let entry = {
      modelName: this.currentParam,
      entryId: entryId
    }
    this.modelStore
      .deleteEntry(entry)
      .then(() => {
        this.modelStore.getData(this.ListResult.listParam)
      })
      .finally(() => (this.loading = false))
  }

  async onRequest(props: any) {
    await this.getcurrentData(props.pagination)
  }

  onCreate() {
    this.dialog = true
    this.editWindowIsForCreate = true
  }

  onEdit(props: any) {
    this.dialog = true
    this.editWindowIsForCreate = false
    let entries = Object.entries(props) as [string, string][]

    for (let [key, value] of entries) {
      let property = this.Model.properties.find(x => x.propertyName === key)
      let isRefField = false
      let fieldValue = value
      if (property) {
        if (property.refModel) {
          let refValue = entries.find(x => x.find(y => y === property!.refModel!.propertyName))
          if (refValue) {
            let obj: any = {
              id: value,
              [property.refModel.propertyName]: refValue[1]
            }

            fieldValue = obj
            isRefField = true
          }
        }
      }
      this.modelValues.push({
        fieldName: key,
        isRefField: isRefField,
        value: fieldValue
      })
    }
  }

  async getcurrentData(pagination?: Pagination) {
    if (this.Model.modelName) {
      let listParam = new ListParam(this.currentParam, pagination || this.ListResult.listParam.pagination, this.ListResult.listParam.filters)
      this.loading = true
      this.modelStore.getData(listParam).finally(() => (this.loading = false))
    }
  }

  @Watch('$route', { immediate: true, deep: true })
  async onUrlChange(newVal: any) {
    this.currentParam = this.$router.currentRoute.params[this.routeParam]

    access.check(this.currentParam, this.$router)

    await this.modelStore.getCurrentModel(this.currentParam)

    this.ListResult.listParam = new ListParam()

    await this.getcurrentData()
  }
}
</script>
