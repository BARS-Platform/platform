import { api } from './api'

import { IWeatherForecast } from '@/models/IWeatherForecast'

export async function getWeatherForecast(): Promise<IWeatherForecast[]> {
  const response = await api.get(`/WeatherForecast`)
  return response.data as IWeatherForecast[]
}
