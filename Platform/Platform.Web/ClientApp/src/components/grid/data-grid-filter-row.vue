<template>
  <q-tr :props="props" style="height: 15px">
    <q-th :key="column.name" v-for="column in props.cols">
      <div class="row items-center">
        <q-input class="col-11" v-if="isRegularColumn(column.name)" borderless :value="getFilter(column.name)" readonly dense />
        <div class="col-1 flex justify-end">
          <data-grid-filter-menu :applyFilter="onFilter" :filters.sync="localFilters" :column="column" />
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

import DataGridFilterMenu from '@/components/grid/data-grid-filter-menu.vue'

@Component({
  components: {
    DataGridFilterMenu
  }
})
export default class DataGridFilterRow extends Vue {
  @Prop() props!: any
  @Prop() filters!: Filtration[]
  @Prop() onFilter!: Function

  localFilters: Filtration[] = []

  @Watch('filters', { immediate: true, deep: true })
  async onFiltersChange(filters: Filtration[]) {
    this.localFilters = filters
  }

  @Watch('localFilters', { immediate: true, deep: true })
  async onLocalFiltersChange(filters: Filtration[]) {
    this.$emit('update:filters', this.localFilters)
  }

  getFilter(columnName: string) {
    let filter = this.localFilters.find(x => x.columnName == columnName)
    return filter ? (filter.columnOperator ? filter.columnOperator + filter.columnValue : filter.columnValue) : ''
  }

  isRegularColumn(columnName: string) {
    return !grid.isActionColumn(columnName)
  }
}
</script>