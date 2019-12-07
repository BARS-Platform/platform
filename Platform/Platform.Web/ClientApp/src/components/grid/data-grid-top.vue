<template>
  <div class="flex">
    <q-btn
      v-for="action in actions"
      :key="action.label"
      :icon="action.icon"
      :label="action.label"
      @click="action.action"
      class="q-mr-lg"
      color="primary"
      no-caps
      flat
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { Action } from '@/models/action'

@Component({})
export default class DataGridTop extends Vue {
  @Prop() actions!: Action[]
}
</script>