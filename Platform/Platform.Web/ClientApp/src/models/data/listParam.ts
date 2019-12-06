import { Pagination } from '@/models/data/pagination'
import { Filtration } from '@/models/data/filtration'

export interface IListParam {
  modelName: string
  pagination: Pagination
  filters: Filtration[]
}

export class ListParam implements IListParam {
  public modelName: string = ''

  public pagination: Pagination = {
    page: 1,
    rowsNumber: 5,
    rowsPerPage: 5
  }

  public filters: Filtration[] = []

  constructor(modelName?: string, pagination?: Pagination, filters?: Filtration[]) {
    this.modelName = modelName || this.modelName
    this.pagination = pagination || this.pagination
    this.filters = filters || this.filters
  }
}
