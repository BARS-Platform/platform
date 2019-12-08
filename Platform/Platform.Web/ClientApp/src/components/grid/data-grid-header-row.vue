<template>
  <q-tr :props="props" style="height: 56px">
    <q-th v-for="column in props.cols" :key="column.name" :props="props">
      {{ column.label }}
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