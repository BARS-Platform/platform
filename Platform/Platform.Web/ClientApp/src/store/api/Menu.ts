import { api } from './api'

import { MenuItem } from '@/models/menuItem'

export async function getMenuItems(): Promise<MenuItem> {
  const response = await api.get(`/Menu/GetMenu`)
  return response.data as MenuItem
}
