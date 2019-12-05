<template>
  <q-tr :props="props">
    <q-th v-for="column in props.cols" :key="column.name">
      <div class="flex justify-between items-center">
        {{ column.label }}
      </div>
    </q-th>
  </q-tr>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

import * as grid from '@/utils/grid'

@Component
export default class DataGridHeaderRow extends Vue {
  @Prop() props!: any
}
</script>