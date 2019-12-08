import { Pagination } from '@/models/data/pagination'
import { Filtration } from '@/models/data/filtration'
import { Sorting } from '@/models/data/sorting'

export interface IListParam {
  modelName: string
  pagination: Pagination
  filters: Filtration[]
  sorting: Sorting | null
}

export class ListParam implements IListParam {
  public modelName: string = ''

  public pagination: Pagination = {
    page: 1,
    rowsNumber: 5,
    rowsPerPage: 15
  }

  public filters: Filtration[] = []

  public sorting: Sorting | null = null

  constructor(modelName?: string, pagination?: Pagination, filters?: Filtration[]) {
    this.modelName = modelName || this.modelName
    this.pagination = pagination || this.pagination
    this.filters = filters || this.filters
    if (this.pagination.sortBy) {
      this.sorting = {
        columnName: this.pagination.sortBy,
        ascending: !this.pagination.descending
      }
    } else {
      this.sorting = null
    }
  }
}
