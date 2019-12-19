<template>
  <div class="flex">
    <div class="text-h5 flex items-center q-mr-xl">
      {{ model.modelLabel }}
    </div>
    <q-btn
      v-for="action in actions"
      :key="action.label"
      :icon="action.icon"
      :label="action.label"
      :hidden="action.hidden"
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
import { Model } from '@/models/model'

@Component({})
export default class DataGridTop extends Vue {
  @Prop() actions!: Action[]
  @Prop() model!: Model
}
</script>