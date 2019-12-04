<template>
  <q-tr :props="props">
    <q-th v-for="column in props.cols" :key="column.name" @mouseover="settingsHover = column.name" @mouseleave="settingsHover = null">
      <div class="flex justify-between items-center">
        {{ column.label }}
        <q-btn
          v-if="isRegularColumn(column.name)"
          @click="inDevelopment"
          icon="fas fa-cogs"
          :style="{ opacity: settingsHover == column.name ? '100%' : '0%' }"
          style="transition: all 0.5s ease-out"
          size="xs"
          flat
          round
        />
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

  settingsHover = null

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