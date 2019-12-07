<template>
  <q-tr :props="props">
    <q-td style="width: 20px">
      <q-btn size="xs" flat round icon="fas fa-pen" @click="inDevelopment" />
    </q-td>
    <q-td :key="column.name" v-for="column in getRegularColumns(props.cols)">
      {{ props.row[column.name] }}
    </q-td>
    <q-td style="width: 20px">
      <q-btn size="xs" flat round icon="fas fa-minus-circle" @click="inDevelopment" />
    </q-td>
  </q-tr>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

import * as grid from '@/utils/grid'
import { isActionColumn } from '../../utils/grid'

@Component
export default class DataGridBody extends Vue {
  @Prop() props!: any

  getRegularColumns(colums: { name: string }[]) {
    return colums.filter(x => !x.name.startsWith('action'))
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