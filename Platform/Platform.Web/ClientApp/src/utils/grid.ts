import { Property } from '@/models/property'
import { Column } from '@/models/grid/column'

export function getColumns(properties: Property[]) {
  let columns: Column[] = []

  columns.push({
    name: 'action_edit',
    label: '',
    field: '',
    align: 'right',
    type: '',
    sortable: false
  })
  properties
    .filter(x => x.displayIn.grid)
    .forEach(x => {
      let column = {
        name: x.propertyName,
        label: x.label,
        field: x.propertyName,
        align: 'left',
        type: x.type,
        sortable: true
      }

      columns.push(column)
    })

  columns.push({
    name: 'action_delete',
    label: '',
    field: '',
    align: 'right',
    type: '',
    sortable: false
  })

  return columns
}

export function isActionColumn(columnName: string) {
  return columnName.startsWith('action')
}

export function getRegularColumns(columns: { name: string }[]) {
  return columns.filter(x => !isActionColumn(x.name))
}
