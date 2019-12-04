<template>
  <q-tr :props="props" style="height: 15px">
    <q-th :key="column.name" v-for="column in props.cols">
      <div class="flex justify-end items-center">
        <q-btn v-if="isRegularColumn(column.name)" icon="fas fa-filter" @click="inDevelopment" size="xs" flat round />
      </div>
    </q-th>
  </q-tr>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

import * as grid from '@/utils/grid'
import { isActionColumn } from '../../utils/grid'

@Component
export default class DataGridFilterRow extends Vue {
  @Prop() props!: any

  isRegularColumn(columnName: string) {
    return !grid.isActionColumn(columnName)
  }

  inDevelopment() {
    this.$q.notify({
      message: 'Данный функционал находится в разработке',
      timeout: 2000,
      color: 'warning'
    })
  }
}
</script>