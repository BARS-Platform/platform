<template>
  <q-page class="flex" ref="page">
    <data-grid :model="Model" :height="tableHeight" :listResult="ListResult" :onRequest="onRequest" :loading="loading" />
    <q-resize-observer @resize="onResize" />
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import DataGrid from '@/components/grid/data-grid.vue'

import * as access from '@/utils/access'
import { ListParam } from '@/models/data/listParam'
import { Pagination } from '../../models/data/pagination'

@Component({
  components: {
    DataGrid
  }
})
export default class ModelIndex extends Vue {
  private modelStore = getModule(ModelModule)
  routeParam = 'name'
  currentParam = ''
  currentHeight = 0
  loading = false

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

  async onRequest(props: any) {
    await this.getcurrentData(props.pagination)
  }

  async getcurrentData(pagination?: Pagination) {
    if (this.Model.modelName) {
      let listParam = new ListParam(this.currentParam, pagination)
      this.loading = true
      this.modelStore.getData(listParam).finally(() => (this.loading = false))
    }
  }

  @Watch('$route', { immediate: true, deep: true })
  async onUrlChange(newVal: any) {
    this.currentParam = this.$router.currentRoute.params[this.routeParam]

    access.check(this.currentParam, this.$router)

    await this.modelStore.getCurrentModel(this.currentParam)

    await this.getcurrentData()
  }
}
</script>