<template>
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
            :value="getOperator(column.name)"
            :options="operators"
          />
          <q-input
            class="col q-ml-xs"
            :mask="column.type === 'integer' ? '###########################' : ''"
            :value="getFilter(column.name)"
            :label="column.label"
            dense
            style="width: 270px"
            @input="setFilter(column, $event)"
          />
        </div>
        <div class="flex justify-end items-center q-mt-md bg-grey-2">
          <q-btn v-close-popup icon="done" @click.stop="applyFilter" size="sm" flat round />
          <q-btn icon="delete" @click.stop="clearFilter(column.name)" size="sm" flat round />
        </div>
      </div>
    </q-menu>
  </q-btn>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { Action } from '@/models/action'
import { Column } from '../../models/grid/column'
import { Filtration } from '../../models/data/filtration'
import * as grid from '@/utils/grid'

@Component({})
export default class DataGridFilterMenu extends Vue {
  @Prop() applyFilter!: Function
  @Prop() column!: Column
  @Prop() filters!: Filtration[]

  operators = ['=', '!=', '<', '<=', '>', '>=']

  get LocalFilters() {
    return this.filters
  }

  set LocalFilters(filters: Filtration[]) {
    this.filters = filters
  }

  getFilter(columnName: string) {
    let filter = this.LocalFilters.find(x => x.columnName == columnName)
    return filter ? (filter.columnOperator ? filter.columnOperator + filter.columnValue : filter.columnValue) : ''
  }

  getOperator(columnName: string) {
    let filter = this.LocalFilters.find(x => x.columnName == columnName)
    return filter ? filter.columnOperator : '='
  }

  setOperator(columnName: string, value: string) {
    let filter = this.LocalFilters.find(x => x.columnName == columnName)
    if (filter) {
      filter.columnOperator = value
    } else {
      this.LocalFilters.push({
        columnName: columnName,
        columnOperator: value,
        columnValue: ''
      })
    }
    this.$emit('update:filters', this.LocalFilters)
  }

  setFilter(column: Column, value: string) {
    let filter = this.LocalFilters.find(x => x.columnName == column.name)
    if (filter) {
      filter.columnValue = value
    } else {
      let filter: Filtration = {
        columnName: column.name,
        columnValue: value
      }
      if (column.type === 'integer') {
        filter.columnOperator = '='
      }
      this.LocalFilters.push(filter)
    }
    this.$emit('update:filters', this.LocalFilters)
  }

  clearFilter(columnName: string) {
    let filter = this.LocalFilters.find(x => x.columnName == columnName)
    if (filter) {
      let index = this.LocalFilters.indexOf(filter)
      this.LocalFilters.splice(index, 1)
    }
  }

  isRegularColumn(columnName: string) {
    return !grid.isActionColumn(columnName)
  }
}
</script>