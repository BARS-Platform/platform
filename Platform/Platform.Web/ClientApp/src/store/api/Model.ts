import { api } from './api'
import { ListResult } from '../../models/data/listResult'

export async function getData(listResult: ListResult): Promise<ListResult> {
  const response = await api.post(`/${listResult.modelName}/GetAll`, listResult)
  return response.data as ListResult
}
