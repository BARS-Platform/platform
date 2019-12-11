<template>
  <q-tr :props="props">
    <q-td style="width: 20px; height: 44px">
      <q-btn size="xs" flat round icon="fas fa-pen" @click="inDevelopment" />
    </q-td>
    <q-td :key="column.name" v-for="column in getRegularColumns(props.cols)" style="height:44px">
      {{ props.row[column.name] }}
    </q-td>
    <q-td style="width: 20px; height: 44px">
      <q-btn size="xs" flat round icon="fas fa-minus-circle">
        <q-menu self="top right">
          <q-card>
            <q-card-section>
              <div class="text-subtitle1">Вы действительно хотите удалить запись?</div>
            </q-card-section>
            <q-separator />
            <q-card-actions align="right">
              <q-btn size="md" style="min-width: 70px" color="negative" v-close-popup @click="onDelete(props.row.id)" outline>Да</q-btn>
              <q-btn style="min-width: 70px" color="positive" v-close-popup outline>Нет</q-btn>
            </q-card-actions>
          </q-card>
        </q-menu>
      </q-btn>
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
  @Prop() onDelete!: Function

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