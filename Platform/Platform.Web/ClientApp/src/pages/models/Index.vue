<template>
  <q-page class="flex flex-center">
    <q-table
      :columns="Columns"
      :data="ListResult.data"
      v-if="Model.modelName && Columns && ListResult"
      :pagination.sync="ListResult.pagination"
      @request="onRequest"
    />
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import * as access from '@/utils/access'
import * as notify from '@/utils/notify'
import * as grid from '@/utils/grid'
import { Model } from '@/models/model'
import { ListResult } from '@/models/data/listResult'

@Component
export default class ModelIndex extends Vue {
  private modelStore = getModule(ModelModule)
  routeParam = 'name'

  get Model() {
    return this.modelStore.Model
  }

  get ListResult() {
    return this.modelStore.ListResult
  }

  get Columns() {
    return grid.getColumns(this.Model.properties)
  }

  async onRequest(props: any) {
    let currentParam = this.$router.currentRoute.params[this.routeParam]
    if (this.Model.modelName) {
      this.ListResult.modelName = currentParam
      this.ListResult.pagination = props.pagination
      await this.modelStore.getData(this.ListResult)
    }
  }

  @Watch('$route', { immediate: true, deep: true })
  async onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.routeParam]

    access.check(currentParam, this.$router)

    await this.modelStore.getCurrentModel(currentParam)

    if (this.Model.modelName) {
      this.ListResult.modelName = currentParam
      this.ListResult.pagination = {
        page: 1,
        rowsPerPage: 5,
        rowsNumber: 5
      }
      await this.modelStore.getData(this.ListResult)
    }
  }
}
</script>
