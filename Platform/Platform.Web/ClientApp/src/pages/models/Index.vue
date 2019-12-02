<template>
  <q-page class="flex flex-center"><q-table :columns="Columns" v-if="Columns && model.modelName"> </q-table></q-page>
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
  model: Model = new Model()

  get Columns() {
    return grid.getColumns(this.model.properties)
  }

  @Watch('$route', { immediate: true, deep: true })
  onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.routeParam]

    access.check(currentParam, this.$router)

    this.modelStore
      .getCurrentModel(currentParam)
      .then(model => {
        this.model = model
      })
      .catch(error => {
        console.log(error)
        notify.error(error)
        this.model = new Model()
      })
  }
}
</script>
