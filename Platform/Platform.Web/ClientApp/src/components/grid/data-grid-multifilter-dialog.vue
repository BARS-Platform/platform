<template>
  <div>
    <q-btn no-caps color="primary" icon="fas fa-filter" @click="seamless = true" size="md" />
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
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

@Component
export default class DataGridMultifilterDialog extends Vue {
  seamless = false
}
</script>