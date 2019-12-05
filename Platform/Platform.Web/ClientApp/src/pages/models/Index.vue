<template>
  <q-page class="flex" ref="page">
    <q-table
      :columns="Columns"
      :visible-columns="VisibleColumns"
      :data="ListResult.data"
      v-if="Model.modelName && Columns && ListResult"
      :pagination.sync="ListResult.listParam.pagination"
      :rows-per-page-options="[3, 5, 10, 25]"
      :pagination-label="(firstRowIndex, endRowIndex, totalRowsNumber) => `${firstRowIndex}-${endRowIndex} из ${totalRowsNumber}`"
      rows-per-page-label="Элементов на странице:"
      @request="onRequest"
      :table-style="`height: ${tableHeight}px`"
      separator="cell"
      style="width: 100%"
      class="platform-table"
    >
      <template v-slot:top>
        <q-btn no-caps color="primary" icon="fas fa-filter" @click="seamless = true" size="md" />
        <q-btn no-caps color="primary" class="q-ml-lg" flat icon="add_box" @click="inDevelopment" label="Создать новую запись" />
        <q-btn no-caps color="primary" flat icon="autorenew" label="Обновить" @click="inDevelopment" />
        <q-space />

        <q-select
          v-model="VisibleColumns"
          multiple
          borderless
          display-value="Столбцы"
          dense
          options-dense
          emit-value
          map-options
          :options="Columns.filter(x => !x.name.startsWith('action'))"
          option-value="name"
          style="min-width: 150px"
        />
      </template>
      <template v-slot:header="props">
        <q-tr :props="props">
          <q-th :key="column.name" v-for="column in props.cols" :props="props" class="">
            <div class="flex justify-between items-center" @mouseover="over = column.name" @mouseleave="over = null">
              {{ column.label }}
              <div>
                <q-btn
                  size="xs"
                  v-if="!column.name.startsWith('action')"
                  flat
                  round
                  icon="fas fa-cogs"
                  :style="{ opacity: over == column.name ? '100%' : '0%' }"
                  @click="inDevelopment"
                />
              </div>
            </div>
          </q-th>
        </q-tr>
        <q-tr :props="props" style="height: 15px">
          <q-th :key="column.name" v-for="column in props.cols" :props="props" class="">
            <div class="flex justify-end items-center">
              <q-btn size="xs" v-if="!column.name.startsWith('action')" flat round icon="fas fa-filter" @click="inDevelopment" />
            </div>
          </q-th>
        </q-tr>
      </template>
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td style="width: 20px">
            <q-btn size="xs" flat round icon="fas fa-pen" @click="inDevelopment" />
          </q-td>
          <q-td :key="column.name" v-for="column in props.cols.filter(x => !x.name.startsWith('action'))" :props="props">
            {{ props.row[column.name] }}
          </q-td>
          <q-td style="width: 20px">
            <q-btn size="xs" flat round icon="fas fa-minus-circle" @click="inDevelopment" />
          </q-td>
        </q-tr>
      </template>
    </q-table>
    <q-dialog v-model="seamless" seamless position="left" full-height>
      <q-card style="width: 350px">
        <q-card-section class="row items-center bg-grey-4">
          <div class="text-h6 flex items-center">
            <q-icon name="fas fa-filter" color="primary" />
            <span class="q-ml-md">Фильтр</span>
          </div>
          <q-space />
          <q-btn icon="fas fa-times" flat round dense size="sm" v-close-popup />
        </q-card-section>
        <q-card-section class="row q-mt-md">
          Данный функционал находится в разработке
        </q-card-section>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'

import { getModule } from 'vuex-module-decorators'
import ModelModule from '@/store/modules/Model'

import * as access from '@/utils/access'
import * as notify from '@/utils/notify'
import * as grid from '@/utils/grid'
import { Model } from '@/models/model'
import { ListResult } from '@/models/data/listResult'

@Component
export default class ModelIndex extends Vue {
  private modelStore = getModule(ModelModule)
  routeParam = 'name'
  seamless = false
  over = null

  visibleColumns: string[] = []

  get VisibleColumns() {
    if (this.visibleColumns.length === 0) {
      return this.Columns.map(x => x.name)
    }
    return this.visibleColumns
  }

  set VisibleColumns(value: string[]) {
    this.visibleColumns = value
  }

  get Model() {
    return this.modelStore.Model
  }

  get tableHeight() {
    return (this.$refs.page as Vue).$el.clientHeight - 49 - 65
  }

  get ListResult() {
    return this.modelStore.ListResult
  }

  get Columns() {
    return grid.getColumns(this.Model.properties)
  }

  inDevelopment() {
    this.$q.notify({
      message: 'Данный функционал находится в разработке',
      timeout: 2000,
      color: 'warning'
    })
  }

  async onRequest(props: any) {
    let currentParam = this.$router.currentRoute.params[this.routeParam]
    if (this.Model.modelName) {
      this.ListResult.listParam.modelName = currentParam
      this.ListResult.listParam.pagination = props.pagination
      await this.modelStore.getData(this.ListResult.listParam)
    }
  }

  @Watch('$route', { immediate: true, deep: true })
  async onUrlChange(newVal: any) {
    let currentParam = this.$router.currentRoute.params[this.routeParam]

    access.check(currentParam, this.$router)

    await this.modelStore.getCurrentModel(currentParam)

    this.visibleColumns = []

    if (this.Model.modelName) {
      this.ListResult.listParam = {
        modelName: currentParam,
        pagination: {
          page: 1,
          rowsPerPage: 5,
          rowsNumber: 5
        }
      }
      await this.modelStore.getData(this.ListResult.listParam)
    }
  }
}
</script>

<style lang="sass">
.platform-table
	tbody tr:last-child
		border-bottom: 1px solid rgba(0,0,0,0.12)
</style>
