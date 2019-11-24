<template>
  <q-page class="flex flex-center">
    {{ currentModel }}
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

@Component
export default class ModelIndex extends Vue {
  modelProperty = 'name'

  currentModel = ''

  @Watch('$route', { immediate: true, deep: true })
  onUrlChange(newVal: any) {
    this.currentModel = this.$router.currentRoute.params[this.modelProperty]
  }
}
</script>
