import { api } from './api'

import { MenuItem } from '@/models/menuItem'

export async function getMenuItems(): Promise<MenuItem> {
  const response = await api.get(`/Menu/GetMenu`)
  return response.data as MenuItem
}

export async function checkAccess(modelRoute: string): Promise<Boolean> {
  const response = await api.get(`/CheckAccess/CheckAccess?modelRoute=${modelRoute}`)
  return response.data as Boolean
}
