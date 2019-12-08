import { api } from './api'
import { ListResult } from '../../models/data/listResult'
import { ListParam } from '~/src/models/data/listParam'
import { Sorting } from '@/models/data/sorting'
import { ModelDto } from '~/src/models/modelDto'

export async function getData(listParam: ListParam): Promise<ListResult> {
  const response = await api.post(`/${listParam.modelName}/GetAll`, listParam)

  let listResult = new ListResult(response.data.data.data, listParam)

  listResult.listParam.pagination.rowsNumber = response.data.data.totalCount

  return listResult
}

export async function createModel(dto: ModelDto) {
  await api.post(`/${dto.modelName}/Create`, dto)
}

export async function deleteEntry({ modelName, entryId }: { modelName: string; entryId: number }): Promise<any> {
  await api.delete(`/${modelName}/Delete?entryId=${entryId}`)
}
