import { RowData } from '@/models/rowData'
import { Pagination } from '@/models/data/pagination'
import { ListParam } from './listParams'
export interface IListResult {
  data: RowData[]
  listParam: ListParam
}

export class ListResult implements IListResult {
  public data: RowData[] = []

  public listParam: ListParam = {
    modelName: '',
    pagination: {
      page: 1,
      rowsNumber: 10,
      rowsPerPage: 10
    }
  }

  constructor(modelName?: string, data?: RowData[], listParam?: ListParam) {
    this.data = data || []
    this.listParam = listParam || {
      modelName: '',
      pagination: {
        page: 1,
        rowsNumber: 10,
        rowsPerPage: 10
      }
    }
  }
}
