<template lang="pug">
q-page.flex.flex-center
  q-table(title="Weather" :data="forecasts" :columns="forecastCols")
</template>

<script lang="ts">
import { Component, Vue} from 'vue-property-decorator';
import { date } from 'quasar'
import { IWeatherForecast } from '../models/IWeatherForecast';
import axios from 'axios';

@Component
export default class PageIndex extends Vue {
  private forecasts: IWeatherForecast[] = [{ summary: 'No data.' } as IWeatherForecast];
  private forecastCols: any[] = [
    { name: 'Summary', label: 'Summary', field: (row: IWeatherForecast) => row.summary },
    { name: 'F',       label: 'F',       field: (row: IWeatherForecast) => row.temperatureF },
    { name: 'C',       label: 'C',       field: (row: IWeatherForecast) => row.temperatureC },
    {
      name: 'Date',
      label: 'Date',
      field: (row: IWeatherForecast) => row.date,
      format: (val: Date) => `${date.formatDate(val, 'YYYY/MM/DD HH:mm:ss')}`
    }
  ];

  public async mounted() {
    try {
      this.forecasts = (await axios.get('api/weatherforecast')).data;
    } catch {
      this.forecasts = [{ summary: 'No data.' } as IWeatherForecast];
    }
  }
}
</script>

