import { api } from './api'
import { ListResult } from '../../models/data/listResult'
import { ListParam } from '~/src/models/data/listParam'
import { ModelDto } from '~/src/models/modelDto'

export async function getData(listParam: ListParam): Promise<ListResult> {
  let method = listParam.methodName ? listParam.methodName : 'GetAll'

  const response = await api.post(`/${listParam.modelName}/${method}`, listParam)

  let listResult = new ListResult(response.data.data.data, listParam)

  listResult.listParam.pagination.rowsNumber = response.data.data.totalCount

  return listResult
}

export async function createModel(dto: ModelDto) {
  await api.post(`/${dto.modelName}/Create`, dto)
}

export async function updateModel(dto: ModelDto) {
  await api.post(`/${dto.modelName}/Update`, dto)
}

export async function deleteEntry({ modelName, entryId }: { modelName: string; entryId: number }): Promise<any> {
  await api.delete(`/${modelName}/Delete?entryId=${entryId}`)
}
