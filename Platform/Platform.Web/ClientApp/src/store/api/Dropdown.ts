import { api } from './api'
import { ListParam } from '~/src/models/data/listParam'
import { RowData } from '~/src/models/rowData'

export async function getData(listParam: ListParam): Promise<RowData[]> {
  const response = await api.post(`/${listParam.modelName}/GetAll`, listParam)
  return response.data.data.data
}
