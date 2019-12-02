<template>
  <q-page class="flex flex-center"><q-table :columns="Columns" :data="Data" v-if="Model.modelName && Columns && Data"> </q-table></q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import * as access from '@/utils/access'
import * as notify from '@/utils/notify'
import * as grid from '@/utils/grid'
import { Model } from '@/models/model'

@Component
export default class ModelIndex extends Vue {
  private modelStore = getModule(ModelModule)
  routeParam = 'name'

  get Model() {
    return this.modelStore.Model
  }

  get Data() {
    return this.modelStore.Data
  }

  get Columns() {
    return grid.getColumns(this.Model.properties)
  }

  @Watch('$route', { immediate: true, deep: true })
  async onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.routeParam]

    access.check(currentParam, this.$router)

    await this.modelStore.getCurrentModel(currentParam)

    if (this.Model.modelName) {
      await this.modelStore.getData(currentParam)
    }
  }
}
</script>
