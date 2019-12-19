import { api } from './api'
import { ListParam } from '~/src/models/data/listParam'
import { RowData } from '~/src/models/rowData'

export async function getData(listParam: ListParam): Promise<RowData[]> {
  let method = listParam.methodName ? listParam.methodName : 'GetAll'
  const response = await api.post(`/${listParam.modelName}/${method}`, listParam)
  return response.data.data.data
}
