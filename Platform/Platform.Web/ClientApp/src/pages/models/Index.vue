<template>
  <q-page class="flex flex-center"> </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import * as access from '@/utils/access'
import * as notify from '@/utils/notify'
import { error } from '../../utils/notify'

@Component
export default class ModelIndex extends Vue {
  private modelStore = getModule(ModelModule)
  modelProperty = 'name'

  @Watch('$route', { immediate: true, deep: true })
  onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.modelProperty]
    access.check(currentParam, this.$router)
    this.modelStore
      .getCurrentModel(currentParam)
      .then(model => {})
      .catch(error => {
        notify.error(error)
      })
  }
}
</script>
