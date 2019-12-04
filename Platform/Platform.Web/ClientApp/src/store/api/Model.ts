import { api } from './api'
import { ListResult } from '../../models/data/listResult'
import { ListParam } from '~/src/models/data/listParams'

export async function getData(listParam: ListParam): Promise<ListResult> {
  const response = await api.post(`/${listParam.modelName}/GetAll`, listParam)
  return response.data as ListResult
}
