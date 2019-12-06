<template>
  <q-table
    v-if="model.modelName && Columns && listResult"
    :data="listResult.data"
    :columns="Columns"
    @request="onRequest"
    :loading="loading"
    :pagination.sync="listResult.listParam.pagination"
    :rows-per-page-options="[3, 5, 10, 25]"
    :pagination-label="paginationLabel"
    rows-per-page-label="Элементов на странице:"
    class="platform-table"
    style="width: 100%"
    :table-style="`height: ${height}px`"
    separator="cell"
  >
    <template v-slot:top>
      <data-grid-top :actions="Actions" />
    </template>
    <template v-slot:header="props">
      <data-grid-header-row :props="props" />
      <data-grid-filter-row :props="props" :onRequest="onRequest" :filters.sync="listResult.listParam.filters" />
    </template>
    <template v-slot:body="props">
      <data-grid-body :props="props" />
    </template>
  </q-table>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { MenuItem } from '@/models/menuItem'

import { Model } from '@/models/model'
import { ListResult } from '@/models/data/listResult'
import { Action } from '@/models/action'

import * as grid from '@/utils/grid'

import DataGridTop from '@/components/grid/data-grid-top.vue'
import DataGridHeaderRow from '@/components/grid/data-grid-header-row.vue'
import DataGridFilterRow from '@/components/grid/data-grid-filter-row.vue'
import DataGridBody from '@/components/grid/data-grid-body.vue'

@Component({
  components: {
    DataGridTop,
    DataGridHeaderRow,
    DataGridFilterRow,
    DataGridBody
  }
})
export default class DataGrid extends Vue {
  @Prop() model!: Model
  @Prop() height!: number
  @Prop() listResult!: ListResult
  @Prop() onRequest!: Function
  @Prop() loading!: Boolean

  paginationLabel(firstRowIndex: number, endRowIndex: number, totalRowsNumber: number) {
    return `${firstRowIndex}-${endRowIndex} из ${totalRowsNumber}`
  }

  get Actions() {
    let actions: Action[] = [
      {
        icon: 'add_box',
        label: 'Создать новую запись',
        action: this.inDevelopment
      },
      {
        icon: 'autorenew',
        label: 'Обновить',
        action: this.onRequest
      }
    ]
    return actions
  }

  get Columns() {
    return grid.getColumns(this.model.properties)
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

<style lang="sass">
.platform-table
	tbody tr:last-child
		border-bottom: 1px solid rgba(0,0,0,0.12)
</style>