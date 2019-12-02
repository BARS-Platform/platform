import { api } from './api'
import { RowData } from '../../models/rowData'

export async function getData(modelName: string): Promise<RowData[]> {
  const response = await api.get(`/${modelName}/GetAll`)
  return response.data as RowData[]
}
