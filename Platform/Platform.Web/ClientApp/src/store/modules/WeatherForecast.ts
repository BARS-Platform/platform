import { IWeatherForecast } from '@/models/IWeatherForecast'
import * as api from '@/store/api/WeatherForecast'
import { Module, VuexModule, MutationAction } from 'vuex-module-decorators'
import Store from '@/store/index'

@Module({
  dynamic: true,
  name: 'weatherForecast',
  namespaced: true,
  store: Store
})
export default class WeatherForecastModule extends VuexModule {
  public forecasts: IWeatherForecast[] = [{ summary: 'No data.' } as IWeatherForecast]

  get Forecasts() {
    return this.forecasts
  }

  @MutationAction
  async getForecasts() {
    const forecasts = await api.getWeatherForecast()
    return { forecasts: forecasts }
  }
}
