import { RowData } from '@/models/rowData'
import { ListParam } from '@/models/data/listParam'
export interface IListResult {
  data: RowData[]
  listParam: ListParam
}

export class ListResult implements IListResult {
  public data: RowData[] = []

  public listParam: ListParam = new ListParam()

  constructor(data?: RowData[], listParam?: ListParam) {
    this.data = data || []
    this.listParam = listParam || new ListParam()
  }
}
