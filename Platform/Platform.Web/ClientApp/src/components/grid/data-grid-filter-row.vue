<template>
  <q-tr :props="props" style="height: 15px">
    <q-th :key="column.name" v-for="column in props.cols">
      <div class="row items-center">
        <q-input class="col-11" v-if="isRegularColumn(column.name)" borderless :value="getValue(column.name)" disable dense />
        <q-btn class="col-1" v-if="isRegularColumn(column.name)" icon="fas fa-filter" size="xs" flat round>
          <q-menu self="top right">
            <div class="no-wrap q-pa-md">
              <div class="row">
                <q-input
                  class="col q-ml-xs"
                  :value="getValue(column.name)"
                  :label="column.label"
                  dense
                  style="width: 270px"
                  @input="setFilter(column.name, $event)"
                />
              </div>
              <div class="flex justify-end items-center q-mt-md bg-grey-2">
                <q-btn icon="done" @click="onRequest" size="sm" flat round />
                <q-btn icon="delete" @click="clearFilter(column.name)" size="sm" flat round />
              </div>
            </div>
          </q-menu>
        </q-btn>
      </div>
    </q-th>
  </q-tr>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

import * as grid from '@/utils/grid'
import { isActionColumn } from '@/utils/grid'
import { Filtration } from '@/models/data/filtration'

@Component
export default class DataGridFilterRow extends Vue {
  @Prop() props!: any
  @Prop() filters!: Filtration[]
  @Prop() onRequest!: Function

  fils: Filtration[] = []

  getValue(columnName: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      return fil.columnValue
    } else {
      return ''
    }
  }

  clearFilter(columnName: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      let index = this.fils.indexOf(fil)
      this.fils.splice(index, 1)
    }
  }

  setFilter(columnName: string, value: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      fil.columnValue = value
    } else {
      this.fils.push({
        columnName: columnName,
        columnValue: value
      })
    }
    this.$emit('update:filters', this.fils)
  }

  isRegularColumn(columnName: string) {
    return !grid.isActionColumn(columnName)
  }

  getRegularColumns(columns: { name: string }[]) {
    return grid.getRegularColumns(columns)
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