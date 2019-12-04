<template>
  <div class="flex">
    <data-grid-multifilter-dialog />
    <q-btn    
      v-for="action in actions"      
      :key="action.label"
      :icon="action.icon"
      :label="action.label"
      @click="action.action"      
      class="q-ml-lg"      
      color="primary"
      no-caps
      flat      
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { Action } from '@/models/action'

import DataGridMultifilterDialog from '@/components/grid/data-grid-multifilter-dialog.vue'

@Component({
  components: {
    DataGridMultifilterDialog
  }
})
export default class DataGridTop extends Vue {
  @Prop() actions!: Action[]
}
</script>