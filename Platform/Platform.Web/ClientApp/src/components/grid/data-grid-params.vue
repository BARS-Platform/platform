<template>
  <q-drawer v-model="drawer" show-if-above :mini="!drawer || miniState" :width="250" :breakpoint="500" bordered content-class="bg-grey-2">
    <template v-slot:mini>
      <q-list padding>
        <q-item class="flex justify-end" clickable @click="miniState = !miniState">
          <q-btn dense flat round unelevated icon="chevron_right" />
        </q-item>
        <q-item class="q-mt-xl">
          <div class="text-h6" style="transform: rotate(90deg)">
            <span>Параметры</span>
          </div>
        </q-item>
      </q-list>
    </template>
    <q-scroll-area class="fit">
      <q-list padding>
        <q-item class="flex justify-between bg-grey-4" clickable @click="miniState = !miniState">
          <div class="text-subtitle2 flex items-center">
            <q-icon name="fas fa-filter" color="primary" />
            <span class="q-ml-md">Параметры</span>
          </div>
          <q-btn dense flat round unelevated icon="chevron_left" />
        </q-item>
      </q-list>
    </q-scroll-area>
  </q-drawer>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { Action } from '@/models/action'

@Component({})
export default class DataGridParams extends Vue {
  drawer = false
  miniState = true
}
</script>