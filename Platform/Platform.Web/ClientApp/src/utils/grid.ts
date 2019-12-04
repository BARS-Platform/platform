import { Property } from '@/models/property'

export function getColumns(properties: Property[]) {
  let columns: { name: string; label: string; field: string; align: string }[] = []

  columns.push({
    name: 'action_edit',
    label: '',
    field: '',
    align: 'right'
  })
  properties
    .filter(x => x.displayIn.grid)
    .forEach(x => {
      let column = {
        name: x.propertyName,
        label: x.label,
        field: x.propertyName,
        align: 'left'
      }

      columns.push(column)
    })

  columns.push({
    name: 'action_delete',
    label: '',
    field: '',
    align: 'right'
  })

  return columns
}
