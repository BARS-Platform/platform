<template>
  <q-page class="flex flex-center">
    {{ currentModel }}
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
import * as access from '@/pages/utils/access'

@Component
export default class ModelIndex extends Vue {
  modelProperty = 'name'

  currentModel = ''

  @Watch('$route', { immediate: true, deep: true })
  onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.modelProperty]
    access.check(currentParam, this.$router)
    this.currentModel = currentParam
  }
}
</script>
