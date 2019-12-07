import { Pagination } from '@/models/data/pagination'

export interface IListParam {
  modelName: string
  pagination: Pagination
}

export class ListParam implements IListParam {
  public modelName: string = ''

  public pagination: Pagination = {
    page: 1,
    rowsNumber: 5,
    rowsPerPage: 5
  }

  constructor(modelName?: string, pagination?: Pagination) {
    this.modelName = modelName || this.modelName
    this.pagination = pagination || this.pagination
  }
}
