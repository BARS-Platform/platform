import { api } from './api'
import { ListResult } from '../../models/data/listResult'
import { ListParam } from '~/src/models/data/listParams'

export async function getData(listParam: ListParam): Promise<ListResult> {
  const response = await api.post(`/${listParam.modelName}/GetAll`, listParam)

  listParam.pagination.rowsNumber = response.data.totalCount

  let listResult: ListResult = {
    data: response.data.data,
    listParam: listParam
  }

  return listResult
}