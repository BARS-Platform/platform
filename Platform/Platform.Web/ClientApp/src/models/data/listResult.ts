import { RowData } from '@/models/rowData'
import { Pagination } from '@/models/data/pagination'
export interface IListResult {
  modelName: string
  data: RowData[]
  pagination: Pagination
}

export class ListResult implements IListResult {
  public modelName: string

  public data: RowData[] = []

  public pagination: Pagination = {
    page: 1,
    rowsNumber: 5,
    rowsPerPage: 5
  }

  constructor(modelName?: string, data?: RowData[], pagination?: Pagination) {
    this.modelName = modelName || ''
    this.data = data || []
    this.pagination = pagination || { rowsPerPage: 5, rowsNumber: 5, page: 1 }
  }
}
