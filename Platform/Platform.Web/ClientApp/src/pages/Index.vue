<template>
  <q-page class="flex flex-center">
    Главная
  </q-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { date } from 'quasar'
import { IWeatherForecast } from '../models/IWeatherForecast'

@Component
export default class PageIndex extends Vue {}
</script>

