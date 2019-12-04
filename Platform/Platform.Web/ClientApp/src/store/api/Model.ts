import { api } from './api'
import { ListResult } from '../../models/data/listResult'
import { ListParam } from '~/src/models/data/listParam'

export async function getData(listParam: ListParam): Promise<ListResult> {
  const response = await api.post(`/${listParam.modelName}/GetAll`, listParam)

  let listResult = new ListResult(response.data.data, listParam)

  listResult.listParam.pagination.rowsNumber = response.data.totalCount

  return listResult
}