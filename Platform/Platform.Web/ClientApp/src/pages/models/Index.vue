<template>
  <q-page class="flex flex-center">
    {{ currentModel }}
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
import * as permissionHelper from '@/router/helpers/permissionHelper'

@Component
export default class ModelIndex extends Vue {
  modelProperty = 'name'

  currentModel = ''

  @Watch('$route', { immediate: true, deep: true })
  onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.modelProperty]
    if (!permissionHelper.Check(currentParam)) {
      this.$q.notify({
        message: 'Доступ запрещен',
        color: 'negative',
        timeout: 2000
      })
      this.$router.push('/')
    }
    this.currentModel = currentParam
  }
}
</script>
