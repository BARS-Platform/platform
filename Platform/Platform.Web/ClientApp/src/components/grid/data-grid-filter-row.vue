<template>
  <q-tr :props="props" style="height: 15px">
    <q-th :key="column.name" v-for="column in props.cols">
      <div class="row items-center">
        <q-input class="col-11" v-if="isRegularColumn(column.name)" borderless :value="getValue(column.name)" readonly dense />
        <div class="col-1 flex justify-end">
          <q-btn v-if="isRegularColumn(column.name)" icon="fas fa-filter" size="xs" flat round>
            <q-menu self="top right">
              <div class="no-wrap q-pa-md">
                <div class="row">
                  <q-select
                    v-if="column.type === 'integer'"
                    @input="setOperator(column.name, $event)"
                    options-dense
                    class="col-2"
                    dense
                    :value="getOperatorValue(column.name)"
                    :options="options"
                  />
                  <q-input
                    class="col q-ml-xs"
                    :mask="column.type === 'integer' ? '###########################' : ''"
                    :value="getValue(column.name)"
                    :label="column.label"
                    dense
                    style="width: 270px"
                    @input="setFilter(column, $event)"
                  />
                </div>
                <div class="flex justify-end items-center q-mt-md bg-grey-2">
                  <q-btn icon="done" @click="onRequest('filter')" size="sm" flat round />
                  <q-btn icon="delete" @click="clearFilter(column.name)" size="sm" flat round />
                </div>
              </div>
            </q-menu>
          </q-btn>
        </div>
      </div>
    </q-th>
  </q-tr>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from 'vue-property-decorator'

import * as grid from '@/utils/grid'
import { isActionColumn } from '@/utils/grid'
import { Filtration } from '@/models/data/filtration'
import { Column } from '@/models/grid/column'

@Component
export default class DataGridFilterRow extends Vue {
  @Prop() props!: any
  @Prop() filters!: Filtration[]
  @Prop() onRequest!: Function

  options = ['=', '!=', '<', '<=', '>', '>=']

  fils: Filtration[] = this.filters

  @Watch('filters', { immediate: true, deep: true })
  async onFilterChange(newVal: any) {
    this.fils = newVal
  }

  getValue(columnName: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      if (fil.columnOperator) return fil.columnOperator + fil.columnValue
      else return fil.columnValue
    } else {
      return ''
    }
  }

  getOperatorValue(columnName: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      return fil.columnOperator
    } else {
      return '='
    }
  }

  clearFilter(columnName: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      let index = this.fils.indexOf(fil)
      this.fils.splice(index, 1)
    }
  }

  setOperator(columnName: string, value: string) {
    let fil = this.fils.find(x => x.columnName == columnName)
    if (fil) {
      fil.columnOperator = value
    } else {
      this.fils.push({
        columnName: columnName,
        columnOperator: value,
        columnValue: ''
      })
    }
    this.$emit('update:filters', this.fils)
  }

  setFilter(column: Column, value: string) {
    let fil = this.fils.find(x => x.columnName == column.name)
    if (fil) {
      fil.columnValue = value
    } else {
      let fil: Filtration = {
        columnName: column.name,
        columnValue: value
      }
      if (column.type === 'integer') {
        fil.columnOperator = '='
      }
      this.fils.push(fil)
    }
    this.$emit('update:filters', this.fils)
  }

  isRegularColumn(columnName: string) {
    return !grid.isActionColumn(columnName)
  }

  getRegularColumns(columns: { name: string }[]) {
    return grid.getRegularColumns(columns)
  }
}
</script>