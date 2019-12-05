import { Notify } from 'quasar'
import * as permissionHelper from './helpers/permissionHelper'

export default async (to: any, from: any, next: any) => {
  let check = await permissionHelper.Check(to.params.name)
  if (check) {
    next()
  } else {
    next('/')
    Notify.create({
      message: 'Доступ запрещен',
      color: 'negative',
      timeout: 2000
    })
  }
}
